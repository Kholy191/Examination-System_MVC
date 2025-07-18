﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        #region Navigational Props
        public Instructor Instructor { get; set; }
        public Student Student { get; set; }
        #endregion
    }
}
