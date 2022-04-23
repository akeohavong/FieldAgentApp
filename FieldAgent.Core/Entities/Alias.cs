using System.ComponentModel.DataAnnotations;

namespace FieldAgent.Core.Entities
{
    public class Alias
    {
        [Key]
        public int AliasID { get; set; }
        [Required]
        public int AgentID { get; set; }
        [Required, MaxLength(50)]
        public string AliasName { get; set; }
        [Required]
        public Guid InterpolID { get; set; }
        [Required]
        public string Persona { get; set; }


        public Agent Agent { get; set; }
    }
}
