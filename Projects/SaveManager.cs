using System.IO;
using System.Text.Json;
using DemoMoteursDeJeu.Script;
using Godot;

namespace DemoMoteursDeJeu;

public partial class SaveManager : ISubSystem
{
    private string saveFilePath;
    public static SaveManager saveManager;

    public static ISubSystem GetSubSystem()
    {
        if (saveManager == null)
        {
            saveManager = new SaveManager();
        }

        return saveManager;
    }

    
    private SaveManager()
    {
        // Set the file path for the save file
        saveFilePath = Path.Combine(OS.GetUserDataDir(), "save_game.json");
    }

    // Method to save the player's position
    public void SaveGame(CharacterBody2D player)
    {
        // Create SaveData with player's position
        SaveData saveData = new SaveData
        {
            Position = player.GlobalPosition // Save only the position
        };

        // Serialize the SaveData to JSON and write it to a file
        string json = JsonSerializer.Serialize(saveData);
        File.WriteAllText(saveFilePath, json);

        GD.Print("Game saved to " + saveFilePath);
    }

    // Method to load the player's position
    public bool LoadGame(CharacterBody2D player)
    {
        // Check if the save file exists
        if (!File.Exists(saveFilePath))
        {
            GD.Print("Save file not found.");
            return false;
        }

        // Read the file and deserialize it into SaveData
        string json = File.ReadAllText(saveFilePath);
        SaveData saveData = JsonSerializer.Deserialize<SaveData>(json);

        // Load the saved position into the player
        player.GlobalPosition = saveData.Position;

        GD.Print("Game loaded from " + saveFilePath);
        return true;
    }
}