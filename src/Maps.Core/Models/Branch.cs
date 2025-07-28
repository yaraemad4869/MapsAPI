using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maps.src.Maps.Core.Models
{
    public class Branch
    {
        public Branch()
        {
            SetClosingDate();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public TimeOnly OpeningDate { get; set; } = TimeOnly.MinValue;
        public TimeSpan Duration { get; set; } = TimeSpan.Zero;
        public TimeOnly ClosingDate { get; set; }
        private void SetClosingDate()
        {
            ClosingDate = OpeningDate.Add(Duration);
        }
    }
}
