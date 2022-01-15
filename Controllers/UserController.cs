using MALON_GLOBAL_WEBAPP.Models;
using MALON_GLOBAL_WEBAPP.Models.DTO.RequestDto;
using MALON_GLOBAL_WEBAPP.Models.DTO.ResponseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Threading.Tasks;

namespace MALON_GLOBAL_WEBAPP.Controllers
{
    public class UserController : Controller
    {
       
       
        public ActionResult<IEnumerable> Index()  //page that shows after login in
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        //[Authorize]
        public ActionResult<IEnumerable> Index(UserAccount userAccount)
        {
            return View();
        }


        //[Authorize]
        [HttpPost]
        public ActionResult Login(UserLoginRequestDto userLoginRequest)
        {
            return View();
        }

       


    }
}
