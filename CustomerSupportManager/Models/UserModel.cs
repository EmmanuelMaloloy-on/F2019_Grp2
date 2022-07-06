using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerSupportManager.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public UserModel()
        {
            Id = "";
            Email = "";
            Name = "";
            Role = "";
        }

        public UserModel(string id, string email, string name, string role)
        {
            Id = id;
            Email = email;
            Name = name;
            Role = role;
        }
    }
}