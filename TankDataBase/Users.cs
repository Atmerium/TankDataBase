using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankDataBase
{
    public class Users
    {
        public Users(string username, string password, string email, string role)
        {
            Username = username;
            Password = password;
            Email = email;
            Role = role;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public override string ToString()
        {
            return $"{Username},{Password},{Email},{Role}";
        }

        public static Users FromString(string userData)
        {
            var parts = userData.Split(",");
            if (parts.Length != 4)
            {
                throw new FormatException("A felhasználói adatok formátuma nem megfelelő");
            }
            return new Users(parts[0], parts[1], parts[2], parts[3]);
        }
    }
}
