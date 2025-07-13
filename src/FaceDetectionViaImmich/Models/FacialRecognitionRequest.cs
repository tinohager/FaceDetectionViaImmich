using System.Text.Json.Serialization;

namespace FaceDetectionViaImmich.Models
{
    public class FacialRecognitionRequest
    {
        [JsonPropertyName("facial-recognition")]
        public FacialRecognitionConfig FacialRecognitionConfig { get; set; }
    }

    public class FacialRecognitionConfig
    {
        [JsonPropertyName("recognition")]
        public Recognition Recognition { get; set; }

        [JsonPropertyName("detection")]
        public Detection Detection { get; set; }
    }

    public class Recognition
    {
        [JsonPropertyName("modelName")]
        public string ModelName { get; set; }
    }

    public class Detection
    {
        [JsonPropertyName("modelName")]
        public string ModelName { get; set; }

        [JsonPropertyName("options")]
        public Options Options { get; set; }
    }

    public class Options
    {
        [JsonPropertyName("minScore")]
        public double MinScore { get; set; }
    }
}
