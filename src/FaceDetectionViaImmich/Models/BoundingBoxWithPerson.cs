using System.Drawing;

namespace FaceDetectionViaImmich.Models
{
    public class BoundingBoxWithPerson : BoundingBox
    {
        public required string PersonInfo { get; set; }

        public Color Color { get; set; }
    }
}
