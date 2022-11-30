using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class LoginModel : IEntity
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
