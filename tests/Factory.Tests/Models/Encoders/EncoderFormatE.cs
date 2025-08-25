
namespace Factory.Tests.Models.Encoders
{
    public class EncoderFormatE : IEncoder
    {
        public string GetMsg()
        {
            return $"Inside {nameof(EncoderFormatE)}";

        }
        
        public string Encode()
        {
            return $"Encoding to format {nameof(EncoderFormatE)}";
        }
    }
}