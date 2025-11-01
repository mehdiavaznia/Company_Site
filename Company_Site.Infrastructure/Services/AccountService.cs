using Company_site.Domain.Entities;
using Company_Site.Application.DTOs;
using Company_Site.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_Site.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginDto> GetAccountAsync(string returnUrl = "/")
        {
            var result =new LoginDto
            {
                ReturnUrl = returnUrl,
            };
            return result;
        }

        public async Task<ResultDto> LoginAccountAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                return null;
            }

            //_signInManager.SignOutAsync();
            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.IsPersistent, true);

            if (result.Succeeded == true)
            {
                return new ResultDto(true, "با موفقیت وارد اکانت شدید");
            }
            if (result.RequiresTwoFactor == true)
            {
                //
            }
            if (result.IsLockedOut)
            {
                //
            }

            return new ResultDto(false, "مشکلی پیش آمده است");
            
        }

        public async Task<ResultDto> LogOutAccountAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return new ResultDto(true, "خروج از حساب با موفقیت انجام شد");
            }
            catch (Exception ex) 
            {
                return new ResultDto(false, $"خطا در خروج از حساب {ex.Message}");
            }
           
        }

        public async Task<ResultDto> RegisterAccountAsync(RegisterDto dto)
        {
            User user = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email,
            };


            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) 
            {
                return new ResultDto(false, "مشکلی در ثبت نام پیش آمده است");
            }
            return new ResultDto(true, "اکانت با موفقیت ساخته شد");
        }
    }
}
