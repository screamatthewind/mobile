using System;
using System.IO;
using Xamarin.Forms;
using Todo2.Droid;

[assembly: Dependency(typeof(FileHelper))]
namespace Todo2.Droid
{
	public class FileHelper : IFileHelper
	{
		public string GetLocalFilePath(string filename)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			return Path.Combine(path, filename);
		}
	}
}
