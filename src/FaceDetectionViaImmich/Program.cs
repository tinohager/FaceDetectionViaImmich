using FaceDetectionViaImmich;
using FaceDetectionViaImmich.Helpers;
using FaceDetectionViaImmich.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.Json;

Console.WriteLine("Recognize similar faces");

var httpClient = new HttpClient()
{
    BaseAddress = new Uri("http://localhost:3003")
};

var imagePath = @"Images/Faces.jpg";

var immichMachineLearningClient = new ImmichMachineLearningClient(httpClient);
var predictResponse = await immichMachineLearningClient.PredictAsync(imagePath);

var colors = ColorHelper.GenerateColors(predictResponse.FacialRecognitions.Length);
var boundingBoxes = new List<BoundingBoxWithPerson>();
var minimumSimilarityForMatch = 0.5;
var detectedPersons = new HashSet<float>();

var faceIndex = 0;
foreach (var facialRecognition in predictResponse.FacialRecognitions)
{
    var vectors1 = JsonSerializer.Deserialize<float[]>(facialRecognition.Embedding);
    var uniqueId1 = vectors1.Sum();

    if (detectedPersons.Contains(uniqueId1))
    {
        continue;
    }

    var color = colors[faceIndex];

    faceIndex++;

    boundingBoxes.Add(new BoundingBoxWithPerson
    {
        PersonInfo = $"{faceIndex}",
        Color = color,
        X1 = facialRecognition.BoundingBox.X1,
        X2 = facialRecognition.BoundingBox.X2,
        Y1 = facialRecognition.BoundingBox.Y1,
        Y2 = facialRecognition.BoundingBox.Y2,
    });

    foreach (var facialRecognition1 in predictResponse.FacialRecognitions)
    {
        var vectors2 = JsonSerializer.Deserialize<float[]>(facialRecognition1.Embedding);
        var uniqueId2 = vectors2.Sum();

        if (vectors1.SequenceEqual(vectors2))
        {
            continue;
        }

        var similarity = VectorHelper.CosineSimilarity(vectors1, vectors2);
        if (similarity > minimumSimilarityForMatch)
        {
            boundingBoxes.Add(new BoundingBoxWithPerson
            {
                PersonInfo = $"{faceIndex}",
                Color = color,
                X1 = facialRecognition1.BoundingBox.X1,
                X2 = facialRecognition1.BoundingBox.X2,
                Y1 = facialRecognition1.BoundingBox.Y1,
                Y2 = facialRecognition1.BoundingBox.Y2,
            });

            detectedPersons.Add(uniqueId2);

            Console.BackgroundColor = ConsoleColor.Green;
        }

        Console.WriteLine($"{similarity:0.000}");
        Console.ResetColor();
    }

    Console.WriteLine("---------------------------------------------");
}


using var image = new Bitmap(imagePath);
using var graphics = Graphics.FromImage(image);

for (int i = 0; i < boundingBoxes.Count; i++)
{
    var boundingBox = boundingBoxes[i];

    var width = boundingBox.X2 - boundingBox.X1;
    var height = boundingBox.Y2 - boundingBox.Y1;

    using var pen = new Pen(boundingBox.Color, 3);
    graphics.DrawRectangle(pen, new RectangleF(boundingBox.X1, boundingBox.Y1, width, height));

    var font = new Font("Arial", 40, FontStyle.Bold);
    var brush = Brushes.White; // oder z. B. new SolidBrush(Color.Yellow);
    var position = new PointF(boundingBox.X1, boundingBox.Y1 + 20); // etwas über der Box

    graphics.DrawString(boundingBox.PersonInfo, font, brush, position);
}

image.Save("ImageWithBoundingBoxes.jpg", ImageFormat.Jpeg);


Console.WriteLine();


