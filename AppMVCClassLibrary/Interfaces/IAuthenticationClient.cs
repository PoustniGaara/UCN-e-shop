using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.DTOs;

namespace WebApiClient.Interfaces
{
    public interface IAuthenticationClient
    {
        public Task<String> LoginAsync(LoginModelDto loginModel);
    }
}
