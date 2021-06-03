namespace FIAP.Blog.Gabriel.Batista.Models.Requests {
    public class BillingAddressRequest {
        public string[] addressLine { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string dependentLocality { get; set; }
        public string organization { get; set; }
        public string phone { get; set; }
        public string postalCode { get; set; }
        public string recipient { get; set; }
        public string region { get; set; }
        public string sortingCode { get; set; }
    }
}