

using System.ComponentModel.DataAnnotations;

namespace FieldAgent.Core.Entities
{
    public class AgencyAgent
    {
        [Required]
        public int AgencyID { get; set; }
        [Required]
        public int AgentID { get; set; }
        [Required]
        public int SecurityClearanceID { get; set; }
        [Required]
        public Guid BadgeID { get; set; }
        [Required]
        public DateTime ActivationDate { get; set; }
        [Required]
        public DateTime DeactivationDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
  
        public Agency Agency { get; set; }
       
        public Agent Agent { get; set; }
       
        public SecurityClearance SecurityClearance { get; set; }
    }
}
