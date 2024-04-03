using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ArkanoidClone
{
    public static class HighScoreManager
    {
        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\highScores.json");
        private const int MAXIMUM_ENTRIES = 10;

        public static List<HighScore> LoadHighScores()
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                List<HighScore> highScores = JsonSerializer.Deserialize<List<HighScore>>(jsonString);
                return highScores ?? new List<HighScore>();
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred while loading high scores: {e.Message}");
                return new List<HighScore>();
            }
        }

        public static void SaveHighScores()
        {
            throw new System.NotImplementedException();
        }

        public static void AddHighScore(HighScore newHighScore)
        {
            throw new System.NotImplementedException();
        }

        public static void DisplayHighScores()
        {
            throw new System.NotImplementedException();
        }
    }
}