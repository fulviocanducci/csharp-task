using System.ComponentModel.DataAnnotations;

namespace Model.Domain.Entities
{
    public sealed class Task : BaseEntity
    {
        [Required()]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required()]
        public bool IsDone { get; set; }
    }
}
