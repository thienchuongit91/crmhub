using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api_intergration.Models
{
    [Table("DataCollection")]
    public class DataCollection
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column("data")]
        [Required]
        public string data { get; set; }

    }
    public class DataCollectionDbContext : DbContext
    {
        public DataCollectionDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<DataCollection> dataCollections { get; set; }
    }
}