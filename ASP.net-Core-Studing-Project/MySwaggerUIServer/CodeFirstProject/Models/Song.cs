using System;
using System.Collections.Generic;
using System.Text;

namespace CodeFirstProject.Models
{
    class Song
    {
        public int SongId { get; set; }
        public string Title { get; set; }
        public float Duration { get; set; }
        public DateTime ProductionDate { get; set; }
        public int UserId { get; set; }

    }
}
