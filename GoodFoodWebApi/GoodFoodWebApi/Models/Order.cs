using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Order
    {
        [Key]
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
        [JsonPropertyName("isPickUpOnPoint")]
        public bool IsPickUpOnPoint { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("customerEmail")]
        public string CustomerEmail { get; set; }
        [JsonPropertyName("customerPhoneNumber")]
        public string CustomerPhoneNumber { get; set; }
        [JsonPropertyName("orderTotalPrice")]
        public int OrderTotalPrice { get; set; }
    }
}