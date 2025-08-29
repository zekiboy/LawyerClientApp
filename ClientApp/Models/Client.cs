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
        public string ContactInfo { get; set; } // İsteğe bağlı (telefon, mail vs.)

        // Navigation property
        public List<Case>? Cases { get; set; }
    }
}