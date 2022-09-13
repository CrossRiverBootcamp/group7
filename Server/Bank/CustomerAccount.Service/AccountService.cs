

using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;
using NSB.Event;
using NServiceBus;
using System.Net.Mail;

namespace CustomerAccount.Service;

public class AccountService : IAccountService
{

    IMapper _IMapper;
    IAccountStorage _AccountStorage;
    IEmailVerificationStorage _EmailVerificationStorage;
    IOperationHistoryStorage _OperationHistoryStorage;
    IAuthorizationFuncs _AuthorizationFuncs;

    public AccountService(IAccountStorage AccountStorage, IOperationHistoryStorage OperationHistoryStorage, IMapper Mapper, IAuthorizationFuncs authorizationFuncs, IEmailVerificationStorage emailVerificationStorage)
    {
        _AccountStorage = AccountStorage;
        _OperationHistoryStorage = OperationHistoryStorage;
        _EmailVerificationStorage = emailVerificationStorage;
        _IMapper = Mapper;
        _AuthorizationFuncs = authorizationFuncs;
    }
    public async Task<bool> createNewAccount(CustomerModel customer)
    {
        if (await _AccountStorage.emailExist(customer.Email) == false)
        {
            EmailVerificationModel emailVerificationModel = sendEmail(customer.Email);
            if (emailVerificationModel != null)
            {
                EmailVerification emailVerification = _IMapper.Map<EmailVerificationModel, EmailVerification>(emailVerificationModel);
                return await _EmailVerificationStorage.addEmailVarifiction(emailVerification);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public async Task<AccountCustomerInfoModel> getAccountCustomerInfo(int accountID)
    {
        Account account = await _AccountStorage.getAccountCustomerInfo(accountID);
        AccountCustomerInfoModel accountInfo = _IMapper.Map<Account, AccountCustomerInfoModel>(account);
        return accountInfo;
    }

    public async Task<bool> updateBalance(UpdateBalanceModel updateBalance, IMessageHandlerContext context)
    {

        if (await _AccountStorage.accountExist(updateBalance.FromAccountId) == false)
        {
            AccountUpdated accountUpdated = new AccountUpdated()
            {
                TransactionID = updateBalance.TransactionId,
                Status = 2,
                FailureReason = "From Account is not exsist"
            };
            await context.Publish(accountUpdated);
            //publishEvent(updateBalance.TransactionId, 2, "From Account is not exsist", context);
            return false;
        }

        if (await _AccountStorage.accountExist(updateBalance.ToAccountId) == false)
        {
            publishEvent(updateBalance.TransactionId, 2, "TO Account is not exsist", context);
            return false;
        }

        if (await _AccountStorage.balanceCheacking(updateBalance.Amount, updateBalance.FromAccountId) == false)
        {
            publishEvent(updateBalance.TransactionId, 2, "The balnce is not enough", context);
            return false;
        }
        //????????????????????? לבדוק מצב שהוא לא הצליח לעדכן את החשבון
        else
        {
            await _AccountStorage.updateBalance(updateBalance.Amount, updateBalance.FromAccountId, updateBalance.ToAccountId);
            bool reslt = await addOperationHistory(updateBalance);
            AccountUpdated accountUpdated = new AccountUpdated()
            {
                TransactionID = updateBalance.TransactionId,
                Status = 1
            };

            await context.Publish(accountUpdated);
            return true;
        }
    }

    public async void publishEvent(int transactionID, int status, string failureReason, IMessageHandlerContext context)
    {
        AccountUpdated accountUpdated = new AccountUpdated()
        {
            TransactionID = transactionID,
            Status = status,
            FailureReason = failureReason
        };
        await context.Publish(accountUpdated);
    }

    public async Task<bool> addOperationHistory(UpdateBalanceModel updateBalance)
    {
        OperationHistory operationFrom = new OperationHistory();
        operationFrom.AccountId = updateBalance.FromAccountId;
        operationFrom.TransactionID = updateBalance.TransactionId;
        operationFrom.IsDebit = true;
        operationFrom.TransactionAmount = updateBalance.Amount;
        operationFrom.OperationTime = DateTime.Now;
        //לשנותתת
        operationFrom.Balance = 1;

        OperationHistory operationTo = new OperationHistory();
        operationTo.AccountId = updateBalance.ToAccountId;
        operationTo.TransactionID = updateBalance.TransactionId;
        operationTo.IsDebit = false;
        operationTo.TransactionAmount = updateBalance.Amount;
        operationTo.OperationTime = DateTime.Now;
        //לשנות!
        operationTo.Balance = 1;

        return await _OperationHistoryStorage.addOperationHistory(operationFrom, operationTo);
    }
    public EmailVerificationModel sendEmail(string email)
    {
        //generate a code
        var code = new Random(Guid.NewGuid().GetHashCode()).Next(0, 9999).ToString("D4");
        //send a mail
        MailAddress from = new MailAddress("crossriver@outlook.co.il");
        MailAddress to = new MailAddress(email);
        MailMessage message = new MailMessage(from, to);
        message.Subject = "Confirm your email address";
        message.Body = $"Your confirmation code is below — enter it in your open browser window and sign in :) \n {code}";
        SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");
        SmtpServer.Port = 587;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Credentials = new System.Net.NetworkCredential("crossriver@outlook.co.il", "Zipi&Shira");
        SmtpServer.EnableSsl = true;
        try
        {
            SmtpServer.Send(message);
            EmailVerificationModel emailVerificationModel = new EmailVerificationModel()
            {
                Email = email,
                VerificationCode = code,
                ExpirationTime = DateTime.Now
            };
            return emailVerificationModel;
        }
        catch (SmtpException ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }
}


//if (await _AccountStorage.accountExist(updateBalance.FromAccountId) == false)
//        {
//            AccountUpdated accountUpdated = new AccountUpdated()
//            {
//                TransactionID = updateBalance.TransactionId,
//                Status = 2,
//                FailureReason = "From Account is not exsist"
//            };
//await context.Publish(accountUpdated);
//            return false;
//        }

//        if (await _AccountStorage.accountExist(updateBalance.ToAccountId) == false)
//{
//    AccountUpdated accountUpdated = new AccountUpdated()
//    {
//        TransactionID = updateBalance.TransactionId,
//        Status = 2,
//        FailureReason = "To Account is not exsist"
//    };
//    await context.Publish(accountUpdated);
//    return false;
//}

//if (await _AccountStorage.balanceCheacking(updateBalance.Amount, updateBalance.FromAccountId) == false)
//{
//    AccountUpdated accountUpdated = new AccountUpdated()
//    {
//        TransactionID = updateBalance.TransactionId,
//        Status = 2,
//        FailureReason = "The balnce is not enough"
//    };
//    await context.Publish(accountUpdated);
//    return false;
//}
//????????????????????? לבדוק מצב שהוא לא הצליח לעדכן את החשבון
//else
//{
//    await _AccountStorage.updateBalance(updateBalance.Amount, updateBalance.FromAccountId, updateBalance.ToAccountId);
//bool reslt = await addOperationHistory(updateBalance);
//AccountUpdated accountUpdated = new AccountUpdated()
//{
//    TransactionID = updateBalance.TransactionId,
//    Status = 1
//};

//await context.Publish(accountUpdated);
//return true;
//}