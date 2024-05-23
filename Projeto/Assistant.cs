namespace Projeto
{
    public class Assistant
    {
        public static void WriteLogFile(string mensagem, bool condition)
        {
            string path = Directory.GetCurrentDirectory() + @"\logs";

        
            if (!Directory.Exists(path))
            {
                
                Directory.CreateDirectory(path);
            }

        
            StreamWriter sw = new StreamWriter(path + @"\" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".txt", true);

            try
            {
          
                if (!condition)
                {
           
                    sw.WriteLine($"info - {DateTime.Now:dd-MM-yyyy HH:mm:ss} --> {mensagem}");
                }
                else
                {
               
                    sw.WriteLine($"error - {DateTime.Now:dd-MM-yyyy HH:mm:ss} --> {mensagem}");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            
                sw.Flush();
                sw.Close();
            }
        }
    }
}
