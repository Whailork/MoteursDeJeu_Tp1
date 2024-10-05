using DemoMoteursDeJeu.Script;
using Godot;

namespace DemoMoteursDeJeu
{
	public partial class CharacterBody2D : Godot.CharacterBody2D
	{
		public const float Speed = 100.0f;
		public AnimatedSprite2D animatedSprite2D;

		public override void _Ready()
		{
			// Load the game state when the character is ready
			CustomMainLoop.GetCustomMainLoop().GetSubsystem<SaveManager>().LoadGame( GetNode<CharacterBody2D>("."));
			animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
			animatedSprite2D.AnimationFinished += AnimatedSprite2DOnAnimationFinished;
		}

		private Vector2 getInput()
		{
			Vector2 velocity = new Vector2();
			if (Input.IsActionPressed("MoveRight"))
			{
				velocity.X += 1; 
			}

			if (Input.IsActionPressed("MoveLeft"))
			{
				velocity.X -= 1;
			}

			if (Input.IsActionPressed("MoveDown"))
			{
				velocity.Y += 1;
			}

			if (Input.IsActionPressed("MoveUp"))
			{
				velocity.Y -= 1;
			}

			// Animation logic based on movement input
			if (Input.IsActionJustPressed("MoveRight"))
			{
				animatedSprite2D.Animation = "Walk_Right";
				animatedSprite2D.Play();
			}

			if (Input.IsActionJustPressed("MoveLeft"))
			{
				animatedSprite2D.Animation = "Walk_Left";
				animatedSprite2D.Play();
			}

			if (Input.IsActionJustPressed("MoveDown"))
			{
				animatedSprite2D.Animation = "Walk_Down";
				animatedSprite2D.Play();
			}

			if (Input.IsActionJustPressed("MoveUp"))
			{
				animatedSprite2D.Animation = "Walk_Top";
				animatedSprite2D.Play();
			}

			// PlayDead animation
			if (Input.IsActionJustPressed("PlayDead"))
			{
				switch (animatedSprite2D.Animation)
				{
					case "Walk_Right":
					case "Idle_Right":
						animatedSprite2D.Animation = "Die_Right";
						break;
					case "Walk_Left":
					case "Idle_Left":
						animatedSprite2D.Animation = "Die_Left";
						break;
					case "Walk_Down":
					case "Idle_Down":
						animatedSprite2D.Animation = "Die_Down";
						break;
					case "Walk_Top":
					case "Idle_Top":
						animatedSprite2D.Animation = "Die_Top";
						break;
				}
			}

			// Attack animation
			if (Input.IsActionJustPressed("Attack"))
			{
				switch (animatedSprite2D.Animation)
				{
					case "Walk_Right":
					case "Idle_Right":
						animatedSprite2D.Animation = "Attack_Right";
						break;
					case "Walk_Left":
					case "Idle_Left":
						animatedSprite2D.Animation = "Attack_Left";
						break;
					case "Walk_Down":
					case "Idle_Down":
						animatedSprite2D.Animation = "Attack_Down";
						break;
					case "Walk_Top":
					case "Idle_Top":
						animatedSprite2D.Animation = "Attack_Top";
						break;
				}
			}

			// Idle animation
			if (Input.IsActionJustReleased("MoveRight"))
			{
				animatedSprite2D.Animation = "Idle_Right";
			}

			if (Input.IsActionJustReleased("MoveLeft"))
			{
				animatedSprite2D.Animation = "Idle_Left";
			}

			if (Input.IsActionJustReleased("MoveDown"))
			{
				if (velocity.X > 0)
				{
					animatedSprite2D.Animation = "Walk_Right";
				}
				else if (velocity.X < 0)
				{
					animatedSprite2D.Animation = "Walk_Left";
				}
				else
				{
					animatedSprite2D.Animation = "Idle_Down";
				}
			}

			if (Input.IsActionJustReleased("MoveUp"))
			{
				if (velocity.X > 0)
				{
					animatedSprite2D.Animation = "Walk_Right";
				}
				else if (velocity.X < 0)
				{
					animatedSprite2D.Animation = "Walk_Left";
				}
				else
				{
					animatedSprite2D.Animation = "Idle_Top";
				}
			}

			// Normalize velocity and apply movement speed
			velocity = velocity.Normalized() * Speed;
			return velocity;
		}

		private void AnimatedSprite2DOnAnimationFinished()
		{
			// Automatically revert to idle animation after attack
			switch (animatedSprite2D.Animation)
			{
				case "Attack_Right":
					animatedSprite2D.Animation = "Idle_Right";
					animatedSprite2D.Play();
					break;
				case "Attack_Left":
					animatedSprite2D.Animation = "Idle_Left";
					animatedSprite2D.Play();
					break;
				case "Attack_Down":
					animatedSprite2D.Animation = "Idle_Down";
					animatedSprite2D.Play();
					break;
				case "Attack_Top":
					animatedSprite2D.Animation = "Idle_Top";
					animatedSprite2D.Play();
					break;
			}
		}

		public override void _PhysicsProcess(double delta)
		{
			// Handle movement and animations
			Vector2 velocity = getInput();
			Velocity = velocity;
			MoveAndSlide();

			// Check for save/load key presses
			if (Input.IsActionJustReleased("Save")) // Key 9 to Save
			{
				CustomMainLoop.GetCustomMainLoop().GetSubsystem<SaveManager>().SaveGame(GetNode<CharacterBody2D>("."));
			}

			if (Input.IsActionJustReleased("Load")) // Key 0 to Load
			{
				CustomMainLoop.GetCustomMainLoop().GetSubsystem<SaveManager>().LoadGame(GetNode<CharacterBody2D>("."));
			}
		}
	}
}
