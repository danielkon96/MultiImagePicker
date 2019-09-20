using MultiImagePicker;
using System;
using Xamarin.Forms;

namespace MultiImagePicker
{
    /*
     how to use in Xamarin project:

        var result = await GMMultiImagePicker.Current.PickMultiImage();
    */
    public class GMMultiImagePicker
    {
        private static readonly Lazy<IGMMultiImagePicker> Implementation = new Lazy<IGMMultiImagePicker>(CreateModalView,
            System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Current settings to use
        /// </summary>
        public static IGMMultiImagePicker Current
        {
            get
            {
                var ret = Implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }
        private static IGMMultiImagePicker CreateModalView()
        {
        #if PORTABLE
            return null;
        #else
            return DependencyService.Get<IGMMultiImagePicker>();
        #endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return
                new NotImplementedException(
                    "This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}