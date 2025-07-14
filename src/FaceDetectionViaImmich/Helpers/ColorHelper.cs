using System.Drawing;

namespace FaceDetectionViaImmich.Helpers
{
    public static class ColorHelper
    {
        public static Color[] GenerateColors(int count)
        {
            var random = new Random();

            var colors = new List<Color>();
            for (int i = 0; i < count; i++)
            {
                colors.Add(Color.FromArgb(255, random.Next(256), random.Next(256), random.Next(256)));
            }

            return [.. colors];
        }
    }
}
