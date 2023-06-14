namespace SubscriptionApi.Entities
{
    public class SubscriptionEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
