namespace BankAccountProject
{
    // FileHandler saves session results as readable text files.
    public static class FileHandler
    {
        // A session is plain text so it can be opened without this application.
        // Saves session entries and returns the path of the created file.
        // entries contains the actions and results recorded during the session.
        public static string SaveSession(IEnumerable<string> entries)
        {
            string directory = Path.Combine(AppContext.BaseDirectory, "sessions");
            Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, $"session-{DateTime.Now:yyyyMMdd-HHmmss}.txt");
            File.WriteAllLines(filePath, entries);
            return filePath;
        }
    }
}
