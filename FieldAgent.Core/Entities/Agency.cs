

using System.ComponentModel.DataAnnotations;

namespace FieldAgent.Core.Entities
{
    public class Agency
    {
        [Key]
        public int AgencyID { get; set; }
        [Required]
        [MaxLength(25)]
        public string ShortName { get; set; }
        [Required]
        [MaxLength(255)]
        public string LongName { get; set; }
        public List<Mission> Missions { get; set; }
        public List<Location> Locations { get; set; }

        public List<AgencyAgent> AgencyAgents { get; set; }
    }
}
