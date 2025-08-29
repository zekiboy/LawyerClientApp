using System;

namespace ClientApp.Models
{
    public class Case
    {
        public int Id { get; set; }
        public string CaseNumber { get; set; }   // Dava No
        public string Court { get; set; }        // Mahkeme
        public string CaseSubject { get; set; }  // Dava Konusu
        public string CaseStatus { get; set; }   // Açık / Kapalı / Ertelendi

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}