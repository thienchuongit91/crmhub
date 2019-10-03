using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHBFC_Job.Models
{
    [Table("CheckListResult")]
    public class CheckListResult
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Column("DataId")]
        public int DataId { get; set; }
        [Column("Called_Date")]
        public DateTime Called_Date { get; set; }
        [Column("Result")]
        public string Result { get; set; }
    }
    public class CheckListResultDbContext : DbContext
    {
        public CheckListResultDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<CheckListResult> CheckListResultDb { get; set; }
    }
}
