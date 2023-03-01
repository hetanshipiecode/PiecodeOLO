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

        public static void DeleteFile(IFormFile file,string path)
        {
            string folder= Path.GetDirectoryName(path); 

            if(!File.Exists(folder))
              Directory.Delete(folder);

           using (Stream fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }




        }
    }
}
