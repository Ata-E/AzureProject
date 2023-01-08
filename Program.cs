using System;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace ConsoleApp1
{
    class Program
    {
        static string key = "c0acb2943db0476cbba2eb441e475ddf";
        static string endpoint = "https://demo-cv3434.cognitiveservices.azure.com/";

        static void Main(string[] args)
        {
            List<string> imagePaths = new List<string>
            {
                @"C:\Users\User\Desktop\Photos\Interesting.png",
                @"C:\Users\User\Desktop\Photos\Saray.jpg",
                @"C:\Users\User\Desktop\Photos\Uzay1.jpg"
            };

            // client object
            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key)) { Endpoint= endpoint };
            
            foreach(var imagePath in imagePaths) 
            {
                AnalyzeImage(client, imagePath).Wait();
            }

        }

        private static async Task AnalyzeImage(ComputerVisionClient client, string imagePath)
        {
            var features = new List<VisualFeatureTypes?>()
            { VisualFeatureTypes.Description,
              VisualFeatureTypes.Tags,
              VisualFeatureTypes.Categories,
            };

            using (Stream stream = File.OpenRead(imagePath))
            {
                var results = await client.AnalyzeImageInStreamAsync(stream, visualFeatures: features);

                Console.WriteLine("\nDescription");
                foreach (var caption in results.Description.Captions)
                {
                    Console.WriteLine($"{caption.Text} and confidence {caption.Confidence}");
                }

                Console.WriteLine("\nTags:");
                foreach (var tag in results.Tags)
                {
                    Console.WriteLine($"{tag.Name}");
                }
                
                Console.WriteLine("\nCategories:");
                foreach (var category in results.Categories)
                {
                    Console.WriteLine($"{category.Name} confidence {category.Score}");
                }
                
            }
        }
    }

}

