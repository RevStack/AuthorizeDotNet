using RevStack.Payment.Model;

namespace RevStack.AuthorizeDotNet.Model
{
    public class Void : IVoid
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
    }
}
