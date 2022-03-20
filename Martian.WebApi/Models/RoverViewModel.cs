using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Martian.WebApi.Models
{
    public class RoverViewModel
    {
        private string _name;

        [Required]
        public string PlateauName { get; set; }

        [Description("If empty, the value will be assigned automatically.")]
        public string Name { get => _name; set => _name = string.IsNullOrEmpty(value) ? Guid.NewGuid().ToString() : value; }
        [Required]
        public string RoverPlace { get; set; }
        [Required]
        public string RoverCommands { get; set; }
    }
}
