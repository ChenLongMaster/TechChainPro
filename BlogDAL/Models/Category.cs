﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Introduction { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}