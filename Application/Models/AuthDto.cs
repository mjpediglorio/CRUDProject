﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class AuthDto : ResultBaseDto
    {
        public string Token { get; set; }
        public int Id { get; set; }
    }
}
