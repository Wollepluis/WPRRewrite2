using Microsoft.AspNetCore.Identity;
using WPRRewrite2.DTOs;

namespace WPRRewrite2.Interfaces;

public interface IAccount
{
    int AccountId { get; set; }
    string Email { get; set; }
    string Wachtwoord { get; set; }
    string AccountType { get; set; }

    void UpdateAccount(AccountDto nieuweGegevens);
}