using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiImagePicker
{
    /// <summary>
    /// Code implementation found in iOS Project -> MultiMediaChooserPickerImplementation.cs
    /// </summary>
    public interface IGMMultiImagePicker
    {
        Task<List<string>> PickMultiImage();
        Task<List<string>> PickMultiImage(bool needsHighQuality);
        void ClearFileDirectory();
    }
}
