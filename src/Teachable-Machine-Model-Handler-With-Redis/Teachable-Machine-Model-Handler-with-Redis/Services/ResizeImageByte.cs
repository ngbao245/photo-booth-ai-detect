using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Teachable_Machine_Model_Handler_with_Redis.Services
{
    public class ResizeImageByte
    {
        public byte[] ResizeImage(byte[] imageBytes, long targetSize)
        {
            // Đọc mảng byte vào bitmap
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                using (Image image = Image.FromStream(ms))
                {
                    // Tính toán chất lượng JPEG tối ưu để đạt được kích thước mục tiêu
                    long quality = 100;
                    using (var memoryStream = new MemoryStream())
                    {
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                        // Lưu hình ảnh với chất lượng cụ thể và kiểm tra kích thước
                        image.Save(memoryStream, GetEncoder(ImageFormat.Jpeg), encoderParameters);
                        while (memoryStream.Length > targetSize)
                        {
                            quality -= 5; // Giảm chất lượng 5 đơn vị mỗi lần cho đến khi đạt được kích thước mục tiêu
                            memoryStream.Position = 0;
                            memoryStream.SetLength(0);
                            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                            image.Save(memoryStream, GetEncoder(ImageFormat.Jpeg), encoderParameters);
                        }

                        // Trả về mảng byte của hình ảnh đã được giảm kích thước
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}