﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TaxCalculator.Models
{
    public class MyDbContext:DbContext
    {
        public MyDbContext()
        {
        }
    }
}