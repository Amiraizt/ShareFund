﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBContext
{
    public class ExceptionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string? Message { get; set; }
        public DateTime DateTime { get; set; }
        public string? InnerException { get; set; }
        public string StackTrace { get; set; }
    }
}
