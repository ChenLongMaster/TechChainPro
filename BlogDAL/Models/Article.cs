﻿using System;

namespace BlogDAL.Models
{
    public class Article : ModelBase
    {
        public string Name { get; set; }
        public string Abstract { get; set; }
        public string DisplayContent { get; set; }
        public string RepresentImageUrl { get; set; }
        public int Category { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
