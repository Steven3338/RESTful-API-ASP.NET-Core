using Microsoft.AspNetCore.Mvc;
using PeopleInformation.Domain;

namespace CustomerDataWebApplicationExample.Services
{
    public interface IJwtGenerator
    {
        string GenerateJwt(User user);
    }
}