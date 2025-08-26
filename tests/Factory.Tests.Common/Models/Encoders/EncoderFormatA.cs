
namespace Factory.Tests.Common.Models.Encoders
{
    public class EncoderFormatA : IEncoder
    {
        public string GetMsg()
        {
            return $"Inside {nameof(EncoderFormatA)}";
        }
        public string Encode()
        {
            return $"Encoding to format {nameof(EncoderFormatA)}";
        }
    }
}