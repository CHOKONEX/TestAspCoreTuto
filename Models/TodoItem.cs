using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestAspCoreTuto.Models
{
    public class TodoItem
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool IsComplete { get; set; }
    }
}
