using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disty.Common.Contract.Account
{
    public interface IDistyUser
    {
        string Id { get; set; }

        string Email { get; set; }

        string Password { get; set; }

        string PasswordHash { get; set; }

        string PasswordSalt { get; set; }
    }

    [Serializable]
    public class DistyUser : DistyEntity, IDistyUser
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}
