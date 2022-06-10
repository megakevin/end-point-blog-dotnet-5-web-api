using System;
using Microsoft.AspNetCore.Identity;
using VehicleQuotes.Models;

namespace VehicleQuotes.Services
{
    public class ApiKeyService
    {
        private readonly VehicleQuotesContext _context;

        public ApiKeyService(VehicleQuotesContext context)
        {
            _context = context;
        }

        public UserApiKey CreateApiKey(IdentityUser user)
        {
            var newApiKey = new UserApiKey
            {
                User = user,
                Value = GenerateApiKeyValue()
            };

            _context.UserApiKeys.Add(newApiKey);

            _context.SaveChanges();

            return newApiKey;
        }

        private string GenerateApiKeyValue() =>
            $"{Guid.NewGuid().ToString()}-{Guid.NewGuid().ToString()}";
    }
}