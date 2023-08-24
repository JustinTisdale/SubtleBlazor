using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.Text;

namespace SubtleBlazor
{
    public static class EmbeddedResource
    {
        /// <summary>
        /// Retrieve an embedded resource file as a string
        /// </summary>
        /// <param name="fileRelativePath">Relative path within the folder structure of the executing assembly.</param>
        /// <returns>String contents of the file</returns>
        public static async Task<string> FetchAsStringAsync(string fileRelativePath)
        {
            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            var resourceStream = embeddedProvider.GetFileInfo(fileRelativePath).CreateReadStream();

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                var query = await reader.ReadToEndAsync();
                return query;
            }
        }
    }
}