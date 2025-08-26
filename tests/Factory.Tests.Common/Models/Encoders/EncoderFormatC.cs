
namespace Factory.Tests.Common.Models.Encoders
{
    public class EncoderFormatC : IEncoder
    {
        public string GetMsg()
        {
            return $"Inside {nameof(EncoderFormatC)}";

        }
        
        public string Encode()
        {
            return $"Encoding to format {nameof(EncoderFormatC)}";
        }
    }
}
