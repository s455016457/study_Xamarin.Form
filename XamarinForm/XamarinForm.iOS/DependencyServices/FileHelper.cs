using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using XamarinForm.DependencyServices;
using XamarinForm.iOS.DependencyServices;

[assembly:Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace XamarinForm.iOS.DependencyServices
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            String docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}
