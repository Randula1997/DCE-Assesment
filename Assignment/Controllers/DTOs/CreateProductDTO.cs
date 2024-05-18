namespace Assignment.Controllers.DTOs
{
    public class CreateProductDTO
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
