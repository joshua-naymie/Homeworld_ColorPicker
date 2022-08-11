using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Homeworld_ColorPicker
{
    /// <summary>
    /// Class of miscellaneous utility methods that needed throughout the application
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Checks whether a path exists within the file system.
        /// Can be a directory or a file.
        /// </summary>
        /// <param name="path">The path to check</param>
        /// <returns>True if the path exists, false if not</returns>
        public static bool PathExists(string path)
        {
            return Directory.Exists(path) || File.Exists(path);
        }

        /// <summary>
        /// Deletes all files and subdirectories in a directory.
        /// </summary>
        /// <param name="directory">The path to directory to clear</param>
        public static void ClearDirectory(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            directory.Create();
            directory.EnumerateFiles().ToList().ForEach(file => file.Delete());
            directory.EnumerateDirectories().ToList().ForEach(d => d.Delete(true));
        }

        public static Size GetLabelSize(Label label)
        {
            Size s = TextRenderer.MeasureText(label.Text, label.Font);
            s.Width += 2;
            return s;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            Rectangle dimensions = new Rectangle(0, 0, width, height);
            Bitmap resizedImage = new Bitmap(width, height);

            resizedImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (ImageAttributes attributes = new ImageAttributes())
                {
                    attributes.SetWrapMode(WrapMode.TileFlipXY);
                    g.DrawImage(image, dimensions, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            return resizedImage;
        }

        public static void ResizeBoxImage(ref Controls.BadgeBox box)
        {
           box.Image = ResizeImage(box.Image, box.Width, box.Height);
        }
    }
}