﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_site.Domain.Entities
{
    public class Role:IdentityRole
    {
        public string Description { get; set; }
    }
}
