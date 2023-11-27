using Core.Localization.Abstract;
using Core.Localization.Provider;
using Core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Localization.Extantions
{
    public static class MiddlewareExtansions
    {
        public static IServiceCollection AddLocalizationService(this IServiceCollection services)
        {
            
            string AssemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString();
            var directories = Directory.GetDirectories(AssemblyPath);
            var localizeDirectiory = directories.Select(d => Path.Combine(d)).Where(x => x.Contains("Localization")).FirstOrDefault();
            var dictionaryList = new List<Dictionary>();
            if (!string.IsNullOrEmpty(localizeDirectiory))
            {
                string[] files = Directory.GetFiles(Path.Combine(localizeDirectiory));
                foreach (var file in files)
                {
                    var jsonObject = FileUtilities.ReadJsonFileAsync<Dictionary>(Path.Combine(file)).GetAwaiter().GetResult();
                    dictionaryList.Add(jsonObject);
                }
            }
            
            services.AddSingleton(typeof(IDictionaryContext),new DictionaryContext(dictionaryList));
            services.AddSingleton<ILocalizationProvider, LocalizationProvider>();
            return services;
        }
    }
}
