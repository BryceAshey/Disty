using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disty.Common.Contract.Account;
using Disty.Common.Data;
using log4net;

namespace Disty.Service
{
    // public class AccountService : IAccountService
    //{
    //    private ILog _log;
    //    private IDataClient<DistyUser> _dataClient;

    //    public AccountService(ILog log, IDataClient<DistyUser> dataClient)
    //    {
    //        _log = log;
    //        _dataClient = dataClient;
    //    }

    //    public void Login()
    //    {
    //        Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
    //    }

    //    public void Logout()
    //    {
    //        Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
    //    }

    //    public void Register(DistyUser model)
    //    {
    //        var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

    //        IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            
    //    }
    //}
}
