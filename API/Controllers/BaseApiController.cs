using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /*  JSON WEB TOKEN (JWT)(RFC 7519)
    APIler session mantığında çalışan şeyler değildir. 
    Request atarsın sana cevap gelir eğer login edip birşeyler
    yapmak istiyorsan session başlatıp takılmak yerine 
    login olan usere token verirsin user o token ile login
    olması gereken requestleri atar.
    */
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}