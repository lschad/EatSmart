using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchadLucas.EatSmart.Data.Types.Entities
{
    public abstract class EntityBase
    {
        [Key]
        [Required]
        [Column("Id", Order = 1)]
        public Guid Id { get; set; }

        [Required]
        [Column("Created", Order = 2)]
        public DateTime Created { get; set; }

        [Required]
        [Column("Modified", Order = 3)]
        public DateTime Modified { get; set; }

        protected EntityBase()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }
    }
}