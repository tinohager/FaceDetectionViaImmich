using System.Text.Json.Serialization;

namespace FaceDetectionViaImmich.Models
{
    public class PredictResponse
    {
        [JsonPropertyName("facial-recognition")]
        public FacialRecognition[] FacialRecognitions { get; set; }

        [JsonPropertyName("imageHeight")]
        public int ImageHeight { get; set; }

        [JsonPropertyName("imageWidth")]
        public int ImageWidth { get; set; }
    }
}
