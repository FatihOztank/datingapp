using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; // WAY to serve data to frontend
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
            //async dediğimiz şey requestler bir thread ilgileniyor demek
            //always make db calls async
            return await _context.Users.ToListAsync();
            //await taskın işinin bitmesini bekliyoe demek
        }

        //api/users/3 returns user of id 3
        [Authorize]
        [HttpGet("{id}")]
        public async Task< ActionResult<AppUser>> GetUser(int id){
            return await _context.Users.FindAsync(id);
            
        }
    }
}