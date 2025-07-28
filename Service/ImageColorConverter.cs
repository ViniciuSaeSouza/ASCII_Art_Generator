using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ASCII_ART_GENERATOR.Service;

public static class ImageColorConverter
{
    public static Image<Rgba32> ConvertToGray(Image<Rgba32> image)
    {
        //TODO: Improve logic for iterating pixels (serch for methods that using row spans) to be more efficient
        for (int y = 0; y < image.Height; y++)
        {
            for (int x = 0; x < image.Width; x++)
            {
                // Getting each color channel from the image for the weighted sum formula below.
                byte redByteValue = image[x, y].R;
                byte greenByteValue = image[x, y].G;
                byte blueByteValue = image[x, y].B;
                //alphaByteValue = image[x, y].A; // Not using the alpha value for now as it doesn't seem to be necessary


                // GrayScale weighted sum formula:
                //     Ylinear   = (0.2126 * Rlinear)      + (0.7152 * Glinear)        + (0.0722 * Blinear)
                double sumResult = (0.2126 * redByteValue) + (0.7152 * greenByteValue) + (0.0722 * blueByteValue);

                byte lightValue = (byte)sumResult;

                /* 
                    - After getting the lightValue in bytes from the sumResult, we need to replace the current pixel with a new pixel
                    (<TPixel>) with the light values
                    - In this case we can either pass the constructor values as the whole byte value or normalize to a 0, 1 float range by
                      dividing the value by the current range max (in bytes goes from 0 to 255).
                    - Ex: 
                */
                //var yNormalized = yLinear / 255f;
                image[x, y] = new Rgba32(lightValue, lightValue, lightValue);
            }
        }
        return image;
    }

}
