using System.ComponentModel.DataAnnotations;

namespace RESTDogs.Models
{
    public class Dog 
    
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public double Weight { get; set; }



        public override string ToString()
        {
            return $"Dog: {Name},Weight: {Weight}";
        }
    }
}
