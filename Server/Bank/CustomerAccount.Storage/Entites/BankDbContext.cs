﻿
using CustomerAccount.Storage.Entites;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Storage.Entites;

public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
    {

    }
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }

}
