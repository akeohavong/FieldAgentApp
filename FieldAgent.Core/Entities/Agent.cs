

using System.ComponentModel.DataAnnotations;

namespace FieldAgent.Core.Entities
{
    public class Agent
    {
        [Key]
        public int AgentID { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public decimal Height { get; set; }

        public List<AgencyAgent> AgencyAgents { get; set; }
        public List<Mission> Missions { get; set; }
        public MissionAgent MissionAgent { get; set; }
        public List<Alias> Aliases { get; set; }


    }
}
