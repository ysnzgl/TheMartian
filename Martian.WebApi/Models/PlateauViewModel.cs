using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Martian.WebApi.Models
{
    public class PlateauViewModel
    {

        private string _name;

        [Description("If empty, the value will be assigned automatically.")]
        public string Name { get => _name; set => _name = string.IsNullOrEmpty(value) ? Guid.NewGuid().ToString() : value; }

        [Required]
        public string Size { get; set; }
    }
}
