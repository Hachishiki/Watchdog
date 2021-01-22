using System;
using System.Collections.Generic;
using System.IO;

namespace WatchDog
{
    class Watcher
    {
        private Dictionary<string, string> hashedDictionary = new Dictionary<string, string>();

        public void Watch(string path)
        {
            Console.WriteLine("WatchDog: Starting watch...");
            if (Directory.Exists(path))
            {
                while (true)
                {
                    this.LoopFiles(path);
                }
            }
            else
            {
                Console.WriteLine("The directory does not exist. Please enter a different path.");
            }
        }

        private void LoopFiles(string path)
        {
            string[] filePaths = Directory.GetFiles(path);
            for (int i = 0; i < filePaths.Length; i++)
            {
                try
                {
                    string fileContects = File.ReadAllText(filePaths[i]);
                    string hashedFile = HashFile(fileContects);
                    if (!hashedDictionary.ContainsKey(filePaths[i]))
                    {
                        hashedDictionary.Add(filePaths[i], hashedFile);
                    }
                    this.CompareFiles(filePaths[i], hashedFile);
                }
                catch (Exception e) { }
            }
        }

        private void CompareFiles(string currentFilePath, string currentFileHash)
        {
            if (hashedDictionary[currentFilePath] != currentFileHash)
            {
                Notify(currentFilePath);
                hashedDictionary[currentFilePath] = currentFileHash;
            }
        }

        internal static string HashFile(string text)
        {
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        private void Notify(string currentFilePath)
        {
            string dateTime = DateTime.Now.ToString();
            Console.WriteLine($"The following file: {currentFilePath} has changed.\n" +
                $"The time was: [{dateTime}]");
        }
    }
}
