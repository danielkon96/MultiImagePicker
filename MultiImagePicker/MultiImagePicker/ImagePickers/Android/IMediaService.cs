using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiImagePicker.Services
{
    /// <summary>
    /// Code implementation found in Android Project -> MediaService.cs
    /// </summary>
	public interface IMediaService
	{
		  void OpenGallery();
		  void ClearFileDirectory();
	}
}
