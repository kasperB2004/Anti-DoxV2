using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Anti_Dox.Database
{
    public class Status
    {
        [Key]
        public string StatusMessage { get; set; }

    }
}