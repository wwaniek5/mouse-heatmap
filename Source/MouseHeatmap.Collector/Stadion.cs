using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseHeatmap.Collector
{
    public class Stadion
    {
        [Key]
        [Column(Order = 1)]
        [Index("IX_Stadion_Main", Order = 2, IsUnique = true)] // Test for combined, named index
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [Index("IX_Stadion_Main", Order = 1, IsUnique = true)] // Test for combined, named index
        public string Street { get; set; }

        [Key]
        [Column(Order = 3)]
        public string City { get; set; }

        [Column(Order = 4)]
        [Index("ReservedKeyWordTest", IsUnique = true)]
        public int Order { get; set; }
    }
}
