using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Anti_Dox.Database
{
    public class BannedGuild
    {
        [Key]
        public ulong Id { get; set; }
        public ulong ServerId { get; set; }
        public string ServerName { get; set; }
        public Nullable<System.DateTime> TimeOfAction { get; set; }
        public ulong ActionById { get; set; }
        public string Reason { get; set; }
        
    }
}
