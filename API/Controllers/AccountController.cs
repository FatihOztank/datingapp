using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }
        /* baseapicontrollerdeki [apicontroller] sağolsun c# otomatik olarak
        registere attığımız parametreyi nereden çoracağını biliyor
        ama apicontroller olmasa registere verdiğimiz parametrelerde 
        ([FromBody] parameters) şeklinde tanımlayarak bu işi halledebilirdik*/

        [HttpPost("register")] // action result means http status code 200,302,404 vsvs
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){

            if (await UserExists(registerDto.Username)) return BadRequest(
                "Username is taken!"
            ); 

            using var hmac = new HMACSHA512(); // using statementi RAII gibi birşeyi sağlıyor sanırsam
            // DTO = Data Transfer Object

            var user = new AppUser{
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user); // tracks the object, doesnt added to db yet
            await _context.SaveChangesAsync();

            return new UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>>Login(LoginDto loginDto){
            // dbye req. attığımız yerlerde await kullanıyoruz
            var user = await _context.Users.SingleOrDefaultAsync(x =>
            x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Invalid Username!");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(
                loginDto.Password
            ));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid Password");
                
            }
            return new UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

        }

        private async Task<bool> UserExists(string username){
            return await _context.Users.AnyAsync(x => 
            x.UserName == username.ToLower());
        }
    }
}