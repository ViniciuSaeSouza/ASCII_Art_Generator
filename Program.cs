using ASCII_ART_GENERATOR.Service;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

public class Program
{
    static void Main(string[] args)
    {
        var basePath = "C:\\Users\\Vinic\\Documents\\programacao\\desafios\\ASCII_Art_Generator\\images\\";

        var inputFileName = "vegeta.png";
        var inputFilePath = basePath + "source\\" + inputFileName;

        string outputPath = "C:\\Users\\Vinic\\Documents\\programacao\\desafios\\ASCII_Art_Generator\\images\\output\\";
        string fileName = inputFileName.Split(".")[0];

        using (var image = ImageIngest.LoadImage(inputFilePath))
        using (var imageGrayScale = ImageConverter.ConvertToGrayScale(image))
        {
            ImageConverter.ConvertGrayScaleToAscii(imageGrayScale, outputPath, fileName);
        }
        

    }
}