using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBContext
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public string JobTitleAR { get; set; }
        public string JobTitleEN { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string SkypeLink { get; set; }
        public string TwitterLink { get; set; }
        public string Image { get; set; }
    }
}
