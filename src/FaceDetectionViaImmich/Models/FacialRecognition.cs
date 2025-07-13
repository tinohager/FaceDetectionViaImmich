namespace FaceDetectionViaImmich.Models
{
    public class FacialRecognition
    {
        public Boundingbox boundingBox { get; set; }

        public string embedding { get; set; }

        public float score { get; set; }
    }
}
