namespace DishoutOLO.Helpers
{
    public class Utility
    {
        public static string GetBaseUrl(HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}";
        }

        public static void SaveFile(IFormFile file, string path)
        {
            string folder = Path.GetDirectoryName(path);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            using (Stream fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
        }

        public static void DeleteFile(string path)
        {
            if(File.Exists(path))
                File.Delete(path);
        }
    }
}
