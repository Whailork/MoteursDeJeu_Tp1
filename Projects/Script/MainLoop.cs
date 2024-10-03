using Godot;
using System;
using System.Collections.Generic;

public partial class MainLoop : SceneTree
{


	public static MainLoop mainLoop;
	

	public override void _Initialize()
	{
		GD.Print("Initialized:");
		mainLoop = new MainLoop();
	}

	public override bool _Process(double delta)
	{
		// Return true to end the main loop.
		return Input.GetMouseButtonMask() != 0 || Input.IsKeyPressed(Key.Escape);
	}


	public void GetSubsystem<T>()
	{
		
	}
}

public class SubSystem<T>
{
	
}