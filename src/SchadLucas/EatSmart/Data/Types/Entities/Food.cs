using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchadLucas.EatSmart.Data.Types.Entities
{
    [Table("Food")]
    public class Food : EntityBase
    {
        public float Carbohydrates { get; set; }
        public float Fat { get; set; }
        public float Fiber { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public string Points => 99.ToString();
        public float Protein { get; set; }

        public Food()
        {
            Carbohydrates = 0;
            Fat = 0;
            Fiber = 0;
            Protein = 0;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}