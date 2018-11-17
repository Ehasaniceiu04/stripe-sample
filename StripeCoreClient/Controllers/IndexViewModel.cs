namespace StripeCoreClient.Controllers
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
        }

        public string StripePublishableKey { get; set; }
    }
    public class ChargeViewModel
    {
        public string StripeToken { get; set; }
        public string StripeEmail { get; set; }
    }
    public class StripeSettings
    {
        public string PublishedKey { get; set; }
        public string SecretKey { get; set; }
    }
}