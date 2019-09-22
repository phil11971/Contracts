using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Models
{
    public class Stage
    {
        [Key]
        public int StageId { get; set; }

        [Required, MaxLength(50)]
        public string StageName { get; set; }

        [Required]
        public int MinCntDays { get; set; }

        [Required, MaxLength(100)]
        public string Comment { get; set; }

        public List<StageContract> StageContracts { get; set; }

        public Stage()
        {
            StageContracts = new List<StageContract>();
        }
    }
}
