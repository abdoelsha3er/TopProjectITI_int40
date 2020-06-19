using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }               // for max size
        public string[] AcceptedFileTypes { get; set; }  // for types extesions .jpg  .png  .
        public bool IsSupported(string fileName)        // supported or no  .txt  .html  .omg
        {
            return AcceptedFileTypes.Any(s => s == Path.GetExtension(fileName).ToLower());
        }
    }
}
