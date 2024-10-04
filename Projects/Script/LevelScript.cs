using Godot;
using System;

public partial class LevelScript : Node
{

    public override void _Ready() { } // Appelé lorsque le nœud entre dans scene tree pour la 1e fois.
    public override void _Process(double delta) { } // Appelé à chaque frame. 'delta' est le temps écoulé depuis la frame précédente.

    // Variables

    public LevelScript()
    {


    }
    // Le Level Manager n’a qu’une seule fonction : LoadLevel.
    public void _LoadLevel(string level)
    {
        /* 
        Cette fonction prend sous la forme d’une chaine de caractères la scène à charger et la charge.
        ----------------------------------------------------------------------------------------------
        Delete the existing scene.
            1. Unloads memory.
            2. Processing stops.
        */

        // Delete actual scene
        get_node("res://SceneDemo.tscn").free();

        // Charge new scene
        Node simultaneousScene = ResourceLoader.Load<PackedScene>(level).Instantiate(); // C'est PackedScene ou LevelScript?
        GetTree().Root.AddChild(simultaneousScene);
    }
}