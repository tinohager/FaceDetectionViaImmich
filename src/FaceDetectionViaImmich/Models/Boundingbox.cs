using System.Text.Json.Serialization;

namespace FaceDetectionViaImmich.Models
{
    public class BoundingBox
    {
        [JsonPropertyName("x1")]
        public float X1 { get; set; }

        [JsonPropertyName("y1")]
        public float Y1 { get; set; }

        [JsonPropertyName("x2")]
        public float X2 { get; set; }

        [JsonPropertyName("y2")]
        public float Y2 { get; set; }
    }
}
