using Company_Site.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_Site.Application.Interfaces
{
    public interface IAccountService
    {
        Task<ResultDto> LoginAccountAsync(LoginDto dto);
        Task<LoginDto> GetAccountAsync(string returnUrl = "/");
        Task<ResultDto> RegisterAccountAsync(RegisterDto dto);
        Task<ResultDto> LogOutAccountAsync();
    }
}
