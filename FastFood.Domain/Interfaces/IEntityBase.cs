namespace FastFood.Domain.Interfaces
{
    public interface IEntityBase
    {
        long Id { get; set; }
        DateTime CreatedAt { get; set; }

        DateTime? UpdatedAt { get; set; }
    }
}
