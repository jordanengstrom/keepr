using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace keepr.Models
{
    public class Keep
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Img { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int Views { get; set; }
    }
}