using System;
using System.IO;
using Android.Graphics;
using MultiImagePicker.Droid.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(ImageHelpers))]
namespace MultiImagePicker.Droid.Helpers
{
    public class ImageHelpers : ICompressImages
    {
        //collectionName is the name of the folder in your Android Pictures directory.
        public static readonly string collectionName = "TmpPictures";

        public string SaveFile(byte[] imageByte, string fileName)
        {
            var fileDir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), collectionName);
            if (!fileDir.Exists())
            {
                fileDir.Mkdirs();
            }

            var file = new Java.IO.File(fileDir, fileName);
            System.IO.File.WriteAllBytes(file.Path, imageByte);

            return file.Path;
        }

        public string CompressImage(string path)
        {
            byte[] imageBytes;

            //Get the bitmap.
            var originalImage = BitmapFactory.DecodeFile(path);

            //Set imageSize and imageCompression parameters.
            var imageSize = .86;
            var imageCompression = 67;

            //Resize it and then compress it to Jpeg.
            var width = (originalImage.Width * imageSize);
            var height = (originalImage.Height * imageSize);
            var scaledImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, true);

            using (MemoryStream ms = new MemoryStream())
            {
                scaledImage.Compress(Bitmap.CompressFormat.Jpeg, imageCompression, ms);
                imageBytes = ms.ToArray();
            }

            originalImage.Recycle();
            originalImage.Dispose();
            GC.Collect();

            return SaveFile(imageBytes, Guid.NewGuid().ToString());
        }
    }
}
