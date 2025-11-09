public class UpdateOrderStatusDto
{
    public Guid OrderId { get; set; }
    public string NewStatus { get; set; }
}