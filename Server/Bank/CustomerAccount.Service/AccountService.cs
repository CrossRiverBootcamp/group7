

using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.Storage;
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
    ISendEmail _sendEmail;

    public AccountService(IAccountStorage AccountStorage, IOperationHistoryStorage OperationHistoryStorage, IMapper Mapper, IAuthorizationFuncs authorizationFuncs, IEmailVerificationStorage emailVerificationStorage , ISendEmail sendEmail)
    {
        _AccountStorage = AccountStorage;
        _OperationHistoryStorage = OperationHistoryStorage;
        _EmailVerificationStorage = emailVerificationStorage;
        _IMapper = Mapper;
        _AuthorizationFuncs = authorizationFuncs;
        _sendEmail=sendEmail;
    }

    public async Task<bool> createNewAccount(CustomerModel customer)
    {
        bool userIsVerify = await _EmailVerificationStorage.verifyUser(customer.VerificationCode,customer.Email);
        if (userIsVerify)
        {
            Customer newCustomer = _IMapper.Map<CustomerModel, Customer>(customer);
            /* var salt = _AuthorizationFuncs.GenerateSalt(8);
             newCustomer.Password = _AuthorizationFuncs.HashPassword(newCustomer.Password, salt, 1000, 8);*/
            AccountModel accunt = new AccountModel() { Balance = 1000, OpenDate = DateTime.Now };
            Account newAccont = _IMapper.Map<AccountModel, Account>(accunt);
            return await _AccountStorage.createNewAccount(newAccont, newCustomer);
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

    public async Task<AccountUpdated> updateBalance(UpdateBalanceModel updateBalance)
    {

        if (await _AccountStorage.accountExist(updateBalance.FromAccountId) == false)
        {
           return await CreateEvent(updateBalance.TransactionId, 2, "From Account is not exsist");
           
        }
        var email = (await _AccountStorage.getAccountCustomerInfo(updateBalance.FromAccountId)).Customer.Email;

        if (await _AccountStorage.accountExist(updateBalance.ToAccountId) == false)
        {
            string subject = $"Transaction to {updateBalance.ToAccountId} ";
            string body = "We are sorry You tried to make a transaction to an account that does not exist ): ";
            _sendEmail.sendEmail(email, subject, body);
            return await CreateEvent(updateBalance.TransactionId, 2, "TO Account is not exsist");
          
        }

        if (await _AccountStorage.balanceCheacking(updateBalance.Amount, updateBalance.FromAccountId) == false)
        {
            string subject = $"Transaction to {updateBalance.ToAccountId} ";
            string body = "We are sorry You tried to make a transaction, Your account does not have sufficient balance  ): ";
            _sendEmail.sendEmail(email, subject, body);
            return await CreateEvent(updateBalance.TransactionId, 2, "The balnce is not enough");

        }
        else
        {
            BalanceObject balance = await _AccountStorage.updateBalance(updateBalance.Amount, updateBalance.FromAccountId, updateBalance.ToAccountId);
            await addOperationHistory(updateBalance, balance);
            string subject = $"Transaction to {updateBalance.ToAccountId} ";
            string body = $"{updateBalance.Amount} were transferred to {updateBalance.ToAccountId} successfully :)";
            _sendEmail.sendEmail(email, subject, body);
            return await CreateEvent(updateBalance.TransactionId, 1, null);
           
           
        }
    }

    public async Task<AccountUpdated>CreateEvent(int transactionID, int status, string failureReason)
    {
        AccountUpdated accountUpdated = new AccountUpdated()
        {
            TransactionID = transactionID,
            Status = status,
            FailureReason = failureReason
        };

        return accountUpdated;
    }

    public async Task<bool> addOperationHistory(UpdateBalanceModel updateBalance, BalanceObject balance)
    {
        OperationHistory operationFrom = new OperationHistory();
        operationFrom.AccountId = updateBalance.FromAccountId;
        operationFrom.TransactionID = updateBalance.TransactionId;
        operationFrom.IsDebit = true;
        operationFrom.TransactionAmount = updateBalance.Amount;
        operationFrom.OperationTime = DateTime.Now;
        operationFrom.Balance = balance.fromBalance;

        OperationHistory operationTo = new OperationHistory();
        operationTo.AccountId = updateBalance.ToAccountId;
        operationTo.TransactionID = updateBalance.TransactionId;
        operationTo.IsDebit = false;
        operationTo.TransactionAmount = updateBalance.Amount;
        operationTo.OperationTime = DateTime.Now;
        operationTo.Balance = balance.toBalance;

        return await _OperationHistoryStorage.addOperationHistory(operationFrom, operationTo);
    }

}
