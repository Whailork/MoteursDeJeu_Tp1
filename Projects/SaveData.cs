using Godot;
using System;

[Serializable]
public partial class SaveData : Node
{
    public Vector2 Position { get; set; }  // Player's position in the world

    // Constructor (optional for initialization)
    public SaveData(Vector2 position)
    {
        Position = position;
    }

    // Default constructor (needed for deserialization)
    public SaveData()
    {
    }
}
