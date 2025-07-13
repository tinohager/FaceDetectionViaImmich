using FaceDetectionViaImmich;

Console.WriteLine("Recognize similar faces");

var httpClient = new HttpClient()
{
    BaseAddress = new Uri("http://localhost:3003")
};

var immichMachineLearningClient = new ImmichMachineLearningClient(httpClient);
var response = await immichMachineLearningClient.PredictAsync(@"C:\Users\tinoh\Pictures\Test\GesichterFindSame.jpg");



Console.WriteLine();

