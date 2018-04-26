using System.ComponentModel.DataAnnotations;

namespace WebSovelluksetFinal.Models
{
    public class HouseType
    {
        public int ID { get; set; }
        [Required]
        public string Type { get; set; }
    }
}