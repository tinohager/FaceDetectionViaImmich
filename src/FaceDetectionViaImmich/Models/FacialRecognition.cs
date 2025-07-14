using System.Text.Json.Serialization;

namespace FaceDetectionViaImmich.Models
{
    public class FacialRecognition
    {
        [JsonPropertyName("boundingBox")]
        public BoundingBox BoundingBox { get; set; }

        [JsonPropertyName("embedding")]
        public string Embedding { get; set; }

        [JsonPropertyName("score")]
        public float Score { get; set; }
    }
}
