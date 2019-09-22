using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracts.Models
{
    public class StageContract
    {
        public int ContractId { get; set; }
        public Contract Contract { get; set; }

        public int StageId { get; set; }
        public Stage Stage { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PlanCompletionDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ProjCompletionDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? FactCompletionDate { get; set; }

        [Required, MaxLength(100)]
        public string Comment { get; set; }
    }
}
