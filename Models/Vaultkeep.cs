using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace keepr.Models
{
    public class Vaultkeep
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int VaultId { get; set; }
        [Required]
        public int KeepId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}