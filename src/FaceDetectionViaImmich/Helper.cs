using System.Globalization;

namespace FaceDetectionViaImmich;

public static class Helper
{
    public static double CosineSimilarity(
        float[] vectorA,
        float[] vectorB)
    {
        if (vectorA.Length != vectorB.Length)
        {
            throw new ArgumentException("Vectors must be of same length");
        }

        double dotProduct = 0;
        double magnitudeA = 0;
        double magnitudeB = 0;

        for (var i = 0; i < vectorA.Length; i++)
        {
            dotProduct += vectorA[i] * vectorB[i];
            magnitudeA += Math.Pow(vectorA[i], 2);
            magnitudeB += Math.Pow(vectorB[i], 2);
        }

        return dotProduct / (Math.Sqrt(magnitudeA) * Math.Sqrt(magnitudeB));
    }

    public static float[] ParseTextData(string x)
    {
        return x.TrimStart('[').TrimEnd(']').Split(',').Select(o => float.TryParse(o, NumberStyles.Float, CultureInfo.InvariantCulture, out var vector) ? vector : 0).ToArray();
    }
}
