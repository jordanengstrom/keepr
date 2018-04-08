using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace keepr.Models
{
    public class Keep
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}