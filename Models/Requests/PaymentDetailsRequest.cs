namespace FIAP.Blog.Gabriel.Batista.Models.Requests {
    public class PaymentDetailsRequest {
        public BillingAddressRequest billingAddress { get; set; }
        public string cardNumber { get; set; }
        public string cardSecurityCode { get; set; }
        public string cardholderName { get; set; }
        public string expiryMonth { get; set; }
        public string expiryYear { get; set; }
    }
}