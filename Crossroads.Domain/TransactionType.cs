using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crossroads.Domain
{
    public class TransactionType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(3)]
        public string Abbreviation { get; set; }

        [Required]
        public bool BackOutType { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
