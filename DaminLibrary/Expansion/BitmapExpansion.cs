using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class BitmapExpansion
    {
        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
        public static bool Save(this Bitmap bitmap, int maxWidth, int maxHeight, int quality, string SavePath)
        {
            bool result = false;
            try
            {
                int OriginalWidth = bitmap.Width;
                int OriginalHeight = bitmap.Height;

                float ratioX = (float)maxWidth / (float)OriginalWidth;
                float ratioY = (float)maxHeight / (float)OriginalHeight;

                float ratio = System.Math.Max(ratioX, ratioY);

                int newWidth = (int)(OriginalWidth * ratio);
                int newHeight = (int)(OriginalHeight * ratio);

                Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

                using (Graphics graphics = Graphics.FromImage(newImage))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.DrawImage(bitmap, 0, 0, newWidth, newHeight);
                }
                ImageCodecInfo imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);
                System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters encoderParameters = new EncoderParameters(1);
                EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
                encoderParameters.Param[0] = encoderParameter;
                newImage.Save(SavePath, imageCodecInfo, encoderParameters);
                newImage.Dispose();
                result = true;
            }
            catch (Exception exception) { exception.WriteLine(); }
            return result;
        }
    }
}
