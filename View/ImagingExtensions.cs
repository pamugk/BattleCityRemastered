using System;
using System.Drawing;
using Imaging = System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;
using static System.Windows.Interop.Imaging;

namespace View
{
    static class ImagingExtensions
    {
        public static Bitmap ToBitmap(this BitmapSource bitmapsource)
        {
            var width = bitmapsource.PixelWidth;
            var height = bitmapsource.PixelHeight;
            var stride = width * ((bitmapsource.Format.BitsPerPixel + 7) / 8);
            var memoryBlockPointer = Marshal.AllocHGlobal(height * stride);
            bitmapsource.CopyPixels(new Int32Rect(0, 0, width, height), memoryBlockPointer, height * stride, stride);
            var bitmap = new Bitmap(width, height, stride, Imaging.PixelFormat.Format32bppArgb, memoryBlockPointer);
            return bitmap;
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DeleteObject([In] IntPtr hObject);

        public static BitmapSource ToImageSource(this Bitmap bitmap)
        {
            var handle = bitmap.GetHbitmap();
            try
            {
                return CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(handle);
            }
        }
    }
}
