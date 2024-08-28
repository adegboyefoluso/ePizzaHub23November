namespace ePizzaHub.UI.Helper
{
    public class Transactional
    {
        public string definitionKey { get; set; }
        public TranRecipient recipient { get; set; }

    }


    public class TranRecipient
    {
        public string contactKey { get; set; }
        public string to { get; set; }
        public AttributesClass attributes { get; set; }
    }

    public class AttributesClass
    {
        public string Subscriberkey { get; set; }
        public string EmailAddress { get; set; }
        public string MessageKey { get; set; }
    }
}
