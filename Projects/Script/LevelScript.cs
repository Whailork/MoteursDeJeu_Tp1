using Godot;
using System;

/*
LevelScript levelScript = GetNode<LevelScript>("res://Script/LevelScript.cs");
levelScript._LoadLevel("res://Scenes/Level_2.tscn");
*/

public partial class LevelScript : Node
{

	public override void _Ready() { } // Appelé lorsque le nœud entre dans scene tree pour la 1e fois.
	public override void _Process(double delta) { } // Appelé à chaque frame. 'delta' est le temps écoulé depuis la frame précédente.

	// Le Level Manager n’a qu’une seule fonction : LoadLevel.
	public void _LoadLevel(string levelPath)
	{
		var scene = (PackedScene)ResourceLoader.Load(levelPath);
		
		if (scene != null)
		{
			// Delete current scene
			if (GetTree().CurrentScene != null) { GetTree().CurrentScene.QueueFree(); }
			
			// Change to new scene
			GetTree().ChangeSceneToPacked(scene);
			
			GD.PrintErr(levelPath + "n'est pas null");
		}
		else { GD.PrintErr("Échec avec : " + levelPath); }
	
		/* 
		Cette fonction prend sous la forme d’une chaine de caractères la scène à charger et la charge.
		----------------------------------------------------------------------------------------------
		Delete the existing scene.
			1. Unloads memory.
			2. Processing stops.
		*/
	}
}
