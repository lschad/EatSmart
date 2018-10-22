using System.ComponentModel.DataAnnotations;

namespace SchadLucas.EatSmart.Data.Types.Entities
{
    public enum Sex
    {
        Male,
        Female
    }

    public class Person
    {
        public int Height { get; set; }

        [Key]
        public string Name { get; set; }

        public Sex Sex { get; set; }
        public float Weight { get; set; }
    }
}