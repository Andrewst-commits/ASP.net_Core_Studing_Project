﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CodeFirstProject.Models
{
    class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
