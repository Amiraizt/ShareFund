using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBContext
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] 
        public string Image { get; set; }
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }
        public string SummaryAR { get; set; }
        public string SummaryEN { get; set; }
        public string SecondImage { get; set; }
        public string ContentAR { get; set; }
        public string ContentEN { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public NewsCategory Category { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime Date { get; set; }

    }
}
