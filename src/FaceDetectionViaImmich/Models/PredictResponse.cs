using System.Text.Json.Serialization;

namespace FaceDetectionViaImmich.Models
{
    public class PredictResponse
    {
        [JsonPropertyName("facial-recognition")]
        public FacialRecognition[] FacialRecognitions { get; set; }

        public int imageHeight { get; set; }

        public int imageWidth { get; set; }
    }
}
