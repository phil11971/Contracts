using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracts.Models
{
    public class Contract
    {
        [Key]
        public int ContractId { get; set; }

        [Required, MaxLength(50)]
        public string ContractName { get; set; }

        [Required]
        public decimal PlanCost { get; set; }

        [Required, Column(TypeName = "datetime2")]
        public DateTime? PlanDeliveryDate { get; set; }

        [Required]
        public decimal FactCost { get; set; }

        [Required, Column(TypeName = "datetime2")]
        public DateTime? FactDeliveryDate { get; set; }

        public List<StageContract> StageContracts { get; set; }

        public Contract()
        {
            StageContracts = new List<StageContract>();
        }
    }
}
