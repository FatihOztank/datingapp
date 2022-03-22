using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; } // Id koy entity bunun primary key olduğunu çakar
        public string UserName { get; set; } //bu da convention Username yazsaydık iki saat refactor edecektik
        
    }
}