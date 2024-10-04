using Godot;

namespace DemoMoteursDeJeu.Script;

[GlobalClass]
public partial class CustomMainLoop : SceneTree
{


	public static CustomMainLoop customMainLoop;
	

	public override void _Initialize()
	{
		GD.Print("Initialized:");
		//GetSubsystem<SaveManager>().LoadGame(null);
	}

	public override bool _Process(double delta)
	{
		// Return true to end the main loop.
		return Input.GetMouseButtonMask() != 0 || Input.IsKeyPressed(Key.Escape);
	}

	public static CustomMainLoop GetCustomMainLoop()
	{
		if (customMainLoop == null)
		{
			customMainLoop = new CustomMainLoop();
		}

		return customMainLoop;
	}


	public override void _Finalize()
	{
		GD.Print("Finalized:");
	}
	public T GetSubsystem<T>() where T : ISubSystem
	{
		return (T)typeof(T).GetMethod("GetSubSystem").Invoke(null,null);
	}
}

public interface ISubSystem
{

	public static ISubSystem GetSubsystem()
	{
		return null;
	}

}