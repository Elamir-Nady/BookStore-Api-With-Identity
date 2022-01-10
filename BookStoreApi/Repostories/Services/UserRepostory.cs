using BookStoreApi.Data;
using BookStoreApi.DTOS;
using BookStoreApi.Repostories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApi.Repostories.Services
{
    public class UserRepostory: IUserRepostory
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        public UserRepostory(UserManager<User> userManager,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<string> SignIn(LoginDto model)
        {
            string Token;
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                Token = "Email or Password is incorrect!";
                return Token;
            }

            else
            {
                var SiginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
                var UserCliems = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,model.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Aduience"],
                    expires: DateTime.Now.AddDays(14),
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(SiginKey, SecurityAlgorithms.HmacSha256Signature),
                    claims: UserCliems
                    );
                Token = new JwtSecurityTokenHandler().WriteToken(token)
;
            }
            return Token;
        }

        public async Task<string> SignUp(SignUpDto signUpDto)
        {
            User Temp = signUpDto.ToModel();
          var result=  await userManager.CreateAsync(Temp, signUpDto.Password);
            if (!result.Succeeded)
                return null;
            var SiginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var UserCliems =new List<Claim>()
            {
                new Claim(ClaimTypes.Name,signUpDto.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Aduience"],
                expires: DateTime.Now.AddDays(14),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(SiginKey, SecurityAlgorithms.HmacSha256Signature),
                claims: UserCliems

                );
           return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
