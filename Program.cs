using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

public class Program
{
    static void Main(string[] args)
    {
        string imgName = "lenna.png";
        string fileName = "lenna-gray.jpg";
        byte yLinear;
        byte rLinear;
        byte gLinear;
        byte bLinear;
        byte aLinear;

        using FileStream fileStream = new FileStream(fileName, FileMode.Create);
        using StreamWriter streamWriter = new StreamWriter(fileStream);

        using (Image<Rgba32> img = Image.Load<Rgba32>(imgName))
        {
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    // GrayScale weighted sum formula:
                    // Ylinear=0.2126Rlinear+0.7152Glinear+0.0722Blinear

                    rLinear = img[x, y].R;
                    gLinear = img[x, y].G;
                    bLinear = img[x, y].B;
                    aLinear = img[x, y].A;
                    var sumResult = (0.2126 * rLinear) + (0.7152 * gLinear) + (0.0722 * bLinear);
                    yLinear = (byte)sumResult;
                    
                    img[x, y] = new Rgba32(yLinear, yLinear, yLinear);
                    //streamWriter.Write(img[x, y].R);
                    //Console.Write(img[x,y]);
                }
                //Console.WriteLine("");
            }
            img.SaveAsPng(fileStream);
            
        }
    }
}