using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SharedTrip.Models;
using SharedTrip.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(
            Request request,
            IUserService _userService)
             : base(request)
        {
            userService = _userService;
        }

        public Response Login()
            => View();
        public Response Register()
            => View();


        [HttpPost]
        public Response Login(LoginViewModel model)
        {
            Request.Session.Clear();

            (string userId, bool IsCorrect) = userService.IsLoginCorrect(model);

            if (IsCorrect)
            {
                SignIn(userId);

                CookieCollection cookies = new CookieCollection();
                cookies.Add(Session.SessionCookieName,
                    Request.Session.Id);

                return Redirect("/");
            }

            return View(new List<ErrorViewModel>() { new ErrorViewModel("Login incorrect") }, "/Error");
        }
        [HttpPost]
        public Response Register(RegisterViewModel model)
        {
            var (isValid, errors) = userService.ValidateModel(model);

            if (!isValid)
            {
                return View(errors, "/Error");
            }

            try
            {
                userService.RegisterUser(model);
            }
            catch (ArgumentException aex)
            {
                return View(new List<ErrorViewModel> { new ErrorViewModel(aex.Message) }, "/Error");
            }
            catch (Exception)
            {
                return View(new List<ErrorViewModel>() { new ErrorViewModel("Unexpected Error") }, "/Error");
            }

            return Redirect("/Users/Login");
        }
        
        [Authorize]
        public Response Logout()
        {
            SignOut();

            return Redirect("/");
        } 
    }
}
