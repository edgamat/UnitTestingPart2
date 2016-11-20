using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crossroads.Domain
{
    public class AgentTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TransactionTypeId { get; set; }

        [Required]
        public int TransactionStatusId { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        [Required]
        public DateTime? RegisteredDate { get; set; }

        public virtual TransactionType TransactionType { get; set; }

        public virtual TransactionStatus TransactionStatus { get; set; }
    }
}
