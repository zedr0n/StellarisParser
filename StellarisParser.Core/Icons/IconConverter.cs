using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;


namespace StellarisParser.Core.Icons
{
    public class IconConverter
    {
        public Bitmap ConvertIcon(string path)
        {
            if (!File.Exists(path))
                return null;

            Bitmap bitmap;
            using (var image = Pfim.Pfim.FromFile(path))
            {
                PixelFormat format;

                // Convert from Pfim's backend agnostic image format into GDI+'s image format
                switch (image.Format)
                {
                    case Pfim.ImageFormat.Rgba32:
                        format = PixelFormat.Format32bppArgb;
                        break;
                    case Pfim.ImageFormat.Rgb24:
                        format = PixelFormat.Format24bppRgb;
                        break;
                    default:
                        // see the sample for more details
                        throw new NotImplementedException(); 
                }

                // Pin pfim's data array so that it doesn't get reaped by GC, unnecessary
                // in this snippet but useful technique if the data was going to be used in
                // control like a picture box
                var handle = GCHandle.Alloc(image.Data, GCHandleType.Pinned);
                try
                {
                    var data = Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0);
                    bitmap = new Bitmap(image.Width, image.Height, image.Stride, format, data);
                    // bitmap.Save(Path.ChangeExtension(path, ".png"), ImageFormat.Png);
                }
                finally
                {
                    handle.Free();
                }
            }

            return bitmap;
        }
    }
}