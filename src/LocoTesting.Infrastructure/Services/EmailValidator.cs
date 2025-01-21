using System.Net;
using DnsClient;
using Microsoft.Extensions.Configuration;

namespace LocoTesting.Infrastructure.Services;

public interface IEmailValidator
{
    Task<(bool isValid, string message)> ValidateEmailAsync(string email);
}

public class EmailValidator : IEmailValidator
{
    private readonly LookupClient _dnsClient;

    public EmailValidator()
    {
        _dnsClient = new LookupClient();
    }
    
    public async Task<(bool isValid, string message)> ValidateEmailAsync(string email)
    {
        try
        {
            string domain = email.Split('@')[1];
            
            var mxRecords = await _dnsClient.QueryAsync(domain, QueryType.MX);

            if (mxRecords.Answers.Count == 0)
            {
                return (false, "Домен не имеет почтовых серверов (MX-записей)");
            }

            return (true, "Email адрес корректен");
        }
        catch (Exception ex)
        {
            return (false, $"Ошибка при проверке email: {ex.Message}");
        }
    }
}