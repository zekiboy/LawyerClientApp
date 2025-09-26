using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 

namespace ClientApp.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, StringLength(20)]
        public string ClientCode { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? District { get; set; }

        [StringLength(10)]
        public string? PostalCode { get; set; }

        [StringLength(50)]
        public string? TaxId { get; set; } // Vergi No veya T.C. No

        [StringLength(100)]
        public string? CompanyName { get; set; } // Opsiyonel

        // Navigation property
        public List<Case>? Cases { get; set; }
    }
}