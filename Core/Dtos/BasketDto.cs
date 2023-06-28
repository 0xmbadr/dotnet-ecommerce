namespace Core.Dtos
{
    public class BasketDto
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
    }
}
