using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Anti_Dox.Database
{
    public class Logs
    {
        [Key]
        public ulong Id { get; set; }
        public ulong ServerId { get; set; }
        public string ServerName { get; set; }
        public Nullable<System.DateTime> TimeOfAction { get; set; }
        public ulong ActionById { get; set; }
        public int DoxType { get; set; }
        public ulong MessageId { get; set; }
        public int Punishment { get; set; }
        public Nullable<System.DateTime> PunishmentTime { get; set; }
        public ulong ServerCaseId { get; set; }

    }
}
