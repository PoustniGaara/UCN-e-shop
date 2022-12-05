﻿using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class LoginModelDto : IEntity
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}