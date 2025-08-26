

namespace Factory.Tests.Common.Models.Encoders
{
    public class EncoderFormatB : IEncoder
    {
        public string GetMsg()
        {
            return $"Inside {nameof(EncoderFormatB)}";

        }
        
        public string Encode()
        {
            return $"Encoding to format {nameof(EncoderFormatB)}";
        }
    }
}
