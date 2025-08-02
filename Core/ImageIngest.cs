using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using System.Runtime.InteropServices;

namespace ASCII_ART_GENERATOR.Service
{
    public static class ImageIngest
    {
        public static Image<Rgba32> LoadImage(string path)
        {
            try
            {
                var image = Image.Load<Rgba32>(path);
                return image;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (NotSupportedException ex)
            {
                throw new NotSupportedException(ex.Message);
            }
            catch (InvalidImageContentException ex)
            {
                throw new InvalidImageContentException(ex.Message);
            }
            catch (UnknownImageFormatException ex)
            {
                throw new UnknownImageFormatException(ex.Message);
            }
        }
    }
}
