namespace Assignment.Controllers.DTOs
{
    public class CreateOrderDTO
    {
        public int OrderStatus { get; set; }
        public int OrderType { get; set; }
        public bool IsActive { get; set; }
    }
}
