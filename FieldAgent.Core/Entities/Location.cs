
using System.ComponentModel.DataAnnotations;

namespace FieldAgent.Core.Entities
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }

        
        [Required]
        [MaxLength(50)]
        public string LocationName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street1 { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street2 { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(15)]
        public string PostalCode { get; set; }
        [Required]
        [MaxLength(5)]
        public string CountryCode { get; set; }
      
        [Required]
        public int AgencyID { get; set; }

        public Agency Agency { get; set; }
    }
}
