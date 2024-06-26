using System.IO;
using System.Linq;
using System.Reflection;

namespace ARLiteNET.Lib.Tests.Data
{
    public static class PathUtils
    {
        public static string TryGetRootPath()
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (directory != null && !directory.GetFiles("*.csproj").Any())
            {
                directory = directory.Parent;
            }

            return directory.FullName;
        }

        public static string TryGetRootPath(Assembly assembly)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(assembly.Location));

            while (directory != null && !directory.GetFiles("*.csproj").Any())
            {
                directory = directory.Parent;
            }

            return directory.FullName;
        }
    }
}
