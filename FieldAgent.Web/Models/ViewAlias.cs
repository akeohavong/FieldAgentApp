using System;
using System.ComponentModel.DataAnnotations;

namespace FieldAgent.Web.Models
{
    public class ViewAlias
    {
        [Required(ErrorMessage = "Alias ID is required")]
        public int aliasId { get; set; }
        [Required(ErrorMessage = "Agent ID is required")]
        public int agentId { get; set; }
        [Required(ErrorMessage = "Alias name is required")]
        [StringLength(50, ErrorMessage = "Alias name cannot exceed 50 characters")]

        public string aliasName { get; set; }
        public Guid interpolId { get; set; }
        
        public string persona { get; set; }

    }
}
