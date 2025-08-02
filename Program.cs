using ASCII_ART_GENERATOR.Service;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

public class Program
{
    static void Main(string[] args)
    {
        var basePath = "C:\\Users\\Vinic\\Documents\\programacao\\desafios\\ASCII_Art_Generator\\images\\";

        var inputFileName = "lenna.png";
        var inputFilePath = basePath + "source\\" + inputFileName;

        var outPutFileName = "vegeta-B&W.png";
        var outPutFilePath = basePath + "output\\" + outPutFileName;

        using (FileStream outputFileStream = new FileStream(outPutFilePath, FileMode.Create))
        using (var image = ImageIngest.LoadImage(inputFilePath))
        using (var imageGrayScale = ImageConverter.ConvertToGrayScale(image))
        {

            ImageConverter.ConvertGrayScaleToAscii(imageGrayScale);


        }
        

    }
}