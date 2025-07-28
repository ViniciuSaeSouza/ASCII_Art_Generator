using ASCII_ART_GENERATOR.Service;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

public class Program
{
    static void Main(string[] args)
    {
        var basePath = "C:\\Users\\Vinic\\Documents\\programacao\\desafios\\ASCII_Art_Generator\\images\\";
        
        var inputFileName = "k1.jpg";
        var inputFilePath = basePath + "source\\" + inputFileName;

        var outPutFileName = "k1-gray.png";
        var outPutFilePath = basePath + "output\\" + outPutFileName;

        using (FileStream fileStream = new FileStream(outPutFilePath, FileMode.Create))
        using (var image = ImageIngest.LoadImage(inputFilePath))
        using (var imageGray = ImageColorConverter.ConvertToGray(image))
        {
            imageGray.SaveAsPng(fileStream);
        }
    }
}