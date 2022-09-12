using FastFood.Domain.Interfaces;

namespace FastFood.Domain.Base
{
    public class EntityBase : IEntityBase
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
