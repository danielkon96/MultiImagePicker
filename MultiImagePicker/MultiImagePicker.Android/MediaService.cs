using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Widget;
using MultiImagePicker.Droid;
using MultiImagePicker.Services;
using Xamarin.Forms;
using MultiImagePicker.Droid.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(MediaService))]
namespace MultiImagePicker.Droid
{
	public class MediaService : Java.Lang.Object, IMediaService
	{
        public static int OPENGALLERYCODE = 100;
        public void OpenGallery()
		{
			try
			{
                var imageIntent = new Intent(Intent.ActionPick);
                imageIntent.SetType("image/*");
                imageIntent.PutExtra(Intent.ExtraAllowMultiple, true);
                imageIntent.SetAction(Intent.ActionGetContent);
                ((Activity)Forms.Context).StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), OPENGALLERYCODE);
                Toast.MakeText(Xamarin.Forms.Forms.Context, "Tap and hold to select multiple photos.", ToastLength.Short).Show();
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				Toast.MakeText(Xamarin.Forms.Forms.Context, "Error. Can not continue, try again.", ToastLength.Long).Show();
            }
        }

        /// <summary>
        ///     Call this when you want to delete our temporary images.
        ///     Recommendation: Call this after successfully uploading images to Azure Blob Storage.
        /// </summary>
		void IMediaService.ClearFileDirectory()
		{
            var directory = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), ImageHelpers.collectionName).Path.ToString();

            if (Directory.Exists(directory))
            {
                var list = Directory.GetFiles(directory, "*");
                if (list.Length > 0)
                {
                    for (int i = 0; i < list.Length; i++)
                    {
                        File.Delete(list[i]);
                    }
                }
            }
		}

        /*
        Example of how to call ClearFileDirectory():

            if (Device.RuntimePlatform == Device.Android)
            {
                DependencyService.Get<IMediaService>().ClearFileDirectory();
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                GMMultiImagePicker.Current.ClearFileDirectory();
            }

        */
	}
}
 