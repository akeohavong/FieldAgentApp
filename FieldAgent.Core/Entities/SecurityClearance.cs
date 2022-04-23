

using System.ComponentModel.DataAnnotations;

namespace FieldAgent.Core.Entities
{
    public class SecurityClearance
    {
        [Key]
        public int SecurityClearanceID { get; set; }
        [Required, MaxLength(50)]
        public string SecurityClearanceName { get; set; }

        public AgencyAgent AgencyAgent { get; set; }
    }
}
