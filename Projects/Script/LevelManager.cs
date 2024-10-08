using Godot;
using System;

using Godot;
using System;
using System.Collections.Generic;
using DemoMoteursDeJeu.Script;

// Pour l'appeler
// GetSubsystem<LevelManager>().LoadLevel("res://path_to_your_scene.tscn");

public partial class LevelManager : ISubSystem
{
	private static LevelManager levelManager;

	// Retourne l'instance du LevelManager
	public static ISubSystem GetSubSystem()
	{

		if (levelManager == null) {
			levelManager = new LevelManager();
		}

		return levelManager;
	}

	// Le Level Manager n’a qu’une seule fonction : LoadLevel.
	public void LoadLevel(string levelPath)
	{
		PackedScene scene = ResourceLoader.Load<PackedScene>(levelPath);

		if (scene != null)
		{
			// Add scene à l'arbre
			CustomMainLoop.GetCustomMainLoop().ChangeSceneToPacked(scene); // CustomMainLoop.GetCustomMainLoop().RootNode = sceneInstance;


			GD.Print(levelPath + " loaded");
		}
		else { GD.PrintErr("Échec avec : " + levelPath); }
	}
}
