

namespace Factory.Tests.Models.Encoders
{
    public class EncoderFormatD : IEncoder
    {
        public string GetMsg()
        {
            return $"Inside {nameof(EncoderFormatD)}";

        }
        
        public string Encode()
        {
            return $"Encoding to format {nameof(EncoderFormatD)}";
        }
    }
}
