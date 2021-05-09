using System;
using System.Collections.Generic;

#nullable disable

namespace DbProject
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public string Head { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Recipient { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
