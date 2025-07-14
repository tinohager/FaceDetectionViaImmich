using FaceDetectionViaImmich.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FaceDetectionViaImmich
{
    public class ImmichMachineLearningClient
    {
        private readonly HttpClient _httpClient;

        public ImmichMachineLearningClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task PingAsync()
        {
            var response = await this._httpClient.GetAsync("/ping");
            response.EnsureSuccessStatusCode();

            var pong = await response.Content.ReadAsStringAsync();
            Console.WriteLine(pong);
        }

        public async Task Predict1Async(string imagePath)
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);


            //Models
            //https://github.com/immich-app/immich/blob/09cbc5d3f4369119d8124e0459affdb9acacc51c/machine-learning/immich_ml/models/constants.py#L6

            var request = new
            {
                clip = new
                {
                    visual = new
                    {
                        modelName = "RN101__yfcc15m"
                    },
                    //textual = new
                    //{
                    //    modelName = "ViT-B-32::openai"
                    //}
                    //detection = new
                    //{
                    //    modelName = "mnet_cov2",
                    //    options = new
                    //    {
                    //        minScore = 0.6
                    //    }
                    //}
                }
            };

            var requestJson = JsonSerializer.Serialize(request);

            using var form = new MultipartFormDataContent();

            var jsonContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            form.Add(jsonContent, "entries");

            var imageContent = new ByteArrayContent(imageBytes);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg"); // or image/png
            form.Add(imageContent, "image", "image.jpg");

            var responsePredict1 = await this._httpClient.PostAsync("/predict", form);
            responsePredict1.EnsureSuccessStatusCode();

            var message1 = await responsePredict1.Content.ReadAsStringAsync();
            Console.WriteLine(responsePredict1.StatusCode);
            Console.WriteLine(message1);
        }

        public async Task<PredictResponse> PredictAsync(string imagePath)
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);

            var request = new FacialRecognitionRequest
            {
                FacialRecognitionConfig = new FacialRecognitionConfig
                {
                    Recognition = new Recognition
                    {
                        ModelName = "buffalo_l"
                    },
                    Detection = new Detection
                    {
                        ModelName = "buffalo_l",
                        Options = new Options
                        {
                            MinScore = 0.75
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(request);

            using var form = new MultipartFormDataContent();

            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
            form.Add(jsonContent, "entries");

            var imageContent = new ByteArrayContent(imageBytes);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg"); // oder image/png
            form.Add(imageContent, "image", "image.jpg");

            var responsePredict = await this._httpClient.PostAsync("/predict", form);
            responsePredict.EnsureSuccessStatusCode();
            var message = await responsePredict.Content.ReadAsStringAsync();
            var predictResponse = JsonSerializer.Deserialize<PredictResponse>(message);

            return predictResponse;
        }
    }
}


