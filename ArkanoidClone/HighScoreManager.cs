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

        private static void SaveHighScores(List<HighScore> highScoresToSave)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(highScoresToSave, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving high scores: {ex.Message}");
            }
        }

        public static void AddHighScore(HighScore newHighScore)
        {
            var highScores = LoadHighScores();
            
            highScores.Add(newHighScore);

            highScores = highScores.OrderBy(highScore => highScore.Score).ToList();

            if (highScores.Count > MAXIMUM_ENTRIES)
                highScores = highScores.Take(MAXIMUM_ENTRIES).ToList();

            SaveHighScores(highScores);
        }
    }
}