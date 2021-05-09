using System;
using System.Collections.Generic;

#nullable disable

namespace DbProject
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
