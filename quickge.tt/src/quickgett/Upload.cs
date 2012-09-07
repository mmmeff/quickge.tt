using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Gett.Sharing;

namespace quickge.tt.src.quickgett
{
    public class Upload
    {
        public string path     { get; set; }
        public string filename { get; set; }
        public FileStream file { get; set; }

        public Upload(String path)
        {
            this.path = path;
            file = File.Open(path, FileMode.Open);
            filename = (file.Name.Split('\\')[file.Name.Split('\\').Length - 1]);
            
            file.Close();
        }
        
    }
}
