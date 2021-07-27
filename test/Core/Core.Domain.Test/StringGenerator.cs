using System.Text;

namespace Core.Domain.Test
{
    public static class StringGenerator
    {
        public static string GenerateStringWithLength(int length)
        {
            var text = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                text.Append('a');
            }

            return text.ToString();
        }
    }
}
