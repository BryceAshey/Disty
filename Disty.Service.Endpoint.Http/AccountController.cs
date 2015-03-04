using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Disty.Common.Contract.Account;

namespace Disty.Service.Endpoint.Http
{
    //[Authorize]
    //[RoutePrefix("api/Account")]
    //public class AccountController : ApiController
    //{
    //    private const string LocalLoginProvider = "Local";
    //    private ApplicationUserManager _userManager;

    //    public AccountController()
    //    {
    //    }

    //    // POST api/Account/Logout
    //    [Route("Logout")]
    //    public IHttpActionResult Logout()
    //    {
    //        Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
    //        return Ok();
    //    }

    //    // POST api/Account/Register
    //    [AllowAnonymous]
    //    [Route("Register")]
    //    public async Task<IHttpActionResult> Register(DistyUser model)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

    //        IdentityResult result = await UserManager.CreateAsync(user, model.Password);

    //        if (!result.Succeeded)
    //        {
    //            return GetErrorResult(result);
    //        }

    //        return Ok();
    //    }
    //}
}
