using System;
using System.ComponentModel.DataAnnotations;

namespace OdenaPWC.Domain
{
    public class ServiceAlert
    {
        
        public int Id { get; set; }
        
        [StringLength(10)]
        [Required]
        public string Line { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
    }
}
