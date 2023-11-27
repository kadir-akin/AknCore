using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public class FileUtilities
    {
        public static async Task<T> ReadJsonFileAsync<T>(string filePath)
        {
            
            using FileStream stream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<T>(stream,new JsonSerializerOptions()
            {
                 
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            });
        }
    }
}
