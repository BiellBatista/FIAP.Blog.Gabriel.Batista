namespace FIAP.Blog.Gabriel.Batista.Models.Requests {
    public class PayRequest {
        public int requestId { get; set; }
        public int methodName { get; set; }
        public PaymentDetailsRequest details { get; set; }

        public string shippingAddress { get; set; }
        public string shippingOption { get; set; }
        public string payerName { get; set; }
        public string payerEmail { get; set; }
        public string payerPhone { get; set; }
    }
}