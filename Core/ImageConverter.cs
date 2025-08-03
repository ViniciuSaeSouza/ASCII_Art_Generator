﻿using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;
using System.Text;

namespace ASCII_ART_GENERATOR.Service;

public static class ImageConverter
{

    public static Image<Rgba32> ConvertToGrayScale(Image<Rgba32> image)
    {
        // Diagnostic's
        var stopWatchPixelRow = new Stopwatch();
        //var stopWatchPixelByPixel = new Stopwatch();

        #region First version
        //stopWatchPixelByPixel.Start();
        //for (int y = 0; y < image.Height; y++)
        //{
        //    for (int x = 0; x < image.Width; x++)
        //    {
        //        redByteValue = image[x, y].R;
        //        greenByteValue = image[x, y].G;
        //        blueByteValue = image[x, y].B;
        //        //alphaByteValue = image[x, y].A; // Not using the alpha value for now as it doesn't seem to be necessary


        //        // GrayScale weighted sum formula:
        //        //     Ylinear   = (0.2126 * Rlinear)      + (0.7152 * Glinear)        + (0.0722 * Blinear)
        //        double sumResult = (0.2126 * redByteValue) + (0.7152 * greenByteValue) + (0.0722 * blueByteValue);
        //        double sumResultClamped = Math.Clamp(sumResult, 0, 255); // Guarantees that the sum result light value won't surpass the 255 limit

        //        byte lightValueClamped = (byte)sumResultClamped;


        //        // - After getting the lightValueClamped in bytes from the sumResult, we need to replace the current pixel with a new pixel
        //        //   (<TPixel>) with the light values
        //        // - In this case we can either pass the constructor values as the whole byte value or normalize to a 0, 1 float range by
        //        //   dividing the value by the current range max (in bytes goes from 0 to 255).
        //        // - Ex: 
        //        //   var yNormalized = yLinear / 255f;


        //        image[x, y] = new Rgba32(lightValueClamped, lightValueClamped, lightValueClamped);
        //    }
        //}
        //stopWatchPixelByPixel.Stop();
        //Console.WriteLine($"Tempo clock pixel por pixel: {stopWatchPixelByPixel.ElapsedMilliseconds}");
        #endregion

        #region Second Version
        //DONE: Improve logic for iterating pixels (serch for methods that using row spans) to be more efficient
        stopWatchPixelRow.Start();
        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                Span<Rgba32> pixelRow = accessor.GetRowSpan(y);

                for (int x = 0; x < pixelRow.Length; x++)
                {
                    ref Rgba32 currentPixel = ref pixelRow[x];

                    // GrayScale weighted sum formula:
                    //     Ylinear  = (0.2126 * Rlinear)      + (0.7152 * Glinear)        + (0.0722 * Blinear)
                    //double sumValue = (0.2126 * currentPixel.R) + (0.7152 * currentPixel.G) + (0.0722 * currentPixel.B);

                    // More readable
                    byte redByteValue = currentPixel.R;
                    byte greenByteValue = currentPixel.G;
                    byte blueByteValue = currentPixel.B;
                    byte alphaByteValue = currentPixel.A;
                    double sumValue = (0.2126 * redByteValue) + (0.7152 * greenByteValue) + (0.0722 * blueByteValue);

                    var sumValueClamped = Math.Clamp(sumValue, 0, 255); // Guarantees that the sum result light value won't surpass the 255 limit

                    byte lightValueByte = (byte)sumValueClamped;

                    currentPixel = new Rgba32(lightValueByte, lightValueByte, lightValueByte, alphaByteValue);
                }
            }

        });
        stopWatchPixelRow.Stop();
        Console.WriteLine($"Tempo clock pixel row: {stopWatchPixelRow.ElapsedMilliseconds}");
        #endregion


        return image;
    }

    public static void ConvertToBlackAndWhite(Image<Rgba32> image)
    {
        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < image.Height; y++)
            {
                var currentRow = accessor.GetRowSpan(y);
                for (int x = 0; x < currentRow.Length; x++)
                {
                    ref var pixel = ref currentRow[x];

                    var lightSumValue = (0.2126 * pixel.R) + (0.7152 * pixel.G) + (0.0722 * pixel.B);

                    float lightFloatValue = (float)(lightSumValue >= 128 ? 1 : 0);
                    pixel = new Rgba32(lightFloatValue, lightFloatValue, lightFloatValue, pixel.A);
                }
            }
        });
    }

    public static void ConvertGrayScaleToAscii(Image<Rgba32> grayImage, string path, string fileName)
    {
        // ASCII Character going from darkest do lightest
        // Different variants
        //var charsString = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\|()1{}[]?-_+~<>i!lI;:,"^`'. ";
        //var charsString = "\" .:-=+*#%@\""; 

        var charsString = "@B%8*!\"^`'."; // My favorite variance so far
        var charList = charsString.ToCharArray();
        int resizeWidth;
        int resizeHeight;




        if (grayImage.Width > 300)
        {
            resizeWidth = (grayImage.Width / 10);
            resizeHeight = (grayImage.Height / 10);
            grayImage.Mutate(x => x.Resize(resizeWidth, resizeHeight));
        }



        void ShowResultOnTerminal()
        {
            int fator = 2;
            resizeWidth = grayImage.Width / fator;
            resizeHeight = grayImage.Height / (fator * 2);
        }


        resizeWidth = grayImage.Width;
        resizeHeight = grayImage.Height / 2;

        grayImage.Mutate(x => x.Resize(resizeWidth, resizeHeight));
        char[] resultadoASCII = AindaNaoSei(grayImage);

        FlushToTxtFile(resultadoASCII, path, fileName);

    }

    private static void FlushToTxtFile(char[] resultadoASCII, string path, string fileName)
    {
        try
        {
            using (var fileStream = new FileStream($"{path}{fileName}.txt", FileMode.Create))
            {
                var encoder = new UTF8Encoding();
                var bytes = encoder.GetBytes(resultadoASCII, 0, resultadoASCII.Length - 1);
                fileStream.Write(bytes);
                fileStream.Flush();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Falha ao salvar resultado ASCII no arquivo txt: " + ex.Message);
        }
    }

    public static char[] AindaNaoSei(Image<Rgba32> image)
    {
        var charsString = "@B%8*!\"^`'."; // My favorite variance so far
        var charList = charsString.ToCharArray();
        var charRow = new char[image.Width];
        image.ProcessPixelRows(accessor =>
        {
            var charRow = new List<char>();

            for (int y = 0; y < accessor.Height; y++)
            {

                var currentRow = accessor.GetRowSpan(y);
                foreach (ref var pixel in currentRow)
                {
                    //TODO: bring grayscale transform logic to here to convert colored images directly to ascii
                    var index = (int)((pixel.R / 255.0) * (charList.Length - 1));
                    charRow.Add(charList[index]);
                }
                charRow.Add('\n');
            }
        });
        return charRow;
    }

}
