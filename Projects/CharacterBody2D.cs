using DemoMoteursDeJeu.Script;
using Godot;

namespace DemoMoteursDeJeu;

public partial class CharacterBody2D : Godot.CharacterBody2D
{
	public const float Speed = 100.0f;
	public const float JumpVelocity = -400.0f;
	public AnimatedSprite2D animatedSprite2D;
	
	public override void _Ready()
	{
		CustomMainLoop.customMainLoop.GetSubsystem<SaveManager>()
		CustomMainLoop.customMainLoop.GetSubsystem<SaveManager>().LoadGame(this);
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
		//pour les anim de walk
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
		
		//les animations pour le play dead
		if (Input.IsActionJustPressed("PlayDead"))
		{
			if (animatedSprite2D.Animation == "Walk_Right" || animatedSprite2D.Animation == "Idle_Right")
			{
				animatedSprite2D.Animation = "Die_Right";
			}

			if (animatedSprite2D.Animation == "Walk_Left" || animatedSprite2D.Animation == "Idle_Left")
			{
				animatedSprite2D.Animation = "Die_Left";
			}

			if (animatedSprite2D.Animation == "Walk_Down" || animatedSprite2D.Animation == "Idle_Down")
			{
				animatedSprite2D.Animation = "Die_Down";
			}

			if (animatedSprite2D.Animation == "Walk_Top" || animatedSprite2D.Animation == "Idle_Top")
			{
				animatedSprite2D.Animation = "Die_Top";
			}
		}

		if (Input.IsActionJustPressed("Attack"))
		{
			if (animatedSprite2D.Animation == "Walk_Right" || animatedSprite2D.Animation == "Idle_Right")
			{
				animatedSprite2D.Animation = "Attack_Right";
			}

			if (animatedSprite2D.Animation == "Walk_Left" || animatedSprite2D.Animation == "Idle_Left")
			{
				animatedSprite2D.Animation = "Attack_Left";
			}

			if (animatedSprite2D.Animation == "Walk_Down" || animatedSprite2D.Animation == "Idle_Down")
			{
				animatedSprite2D.Animation = "Attack_Down";
			}

			if (animatedSprite2D.Animation == "Walk_Top" || animatedSprite2D.Animation == "Idle_Top")
			{
				animatedSprite2D.Animation = "Attack_Top";
			}
		}
		
		// pour les animations de idle
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
			else
			{
				if (velocity.X < 0)
				{
					animatedSprite2D.Animation = "Walk_Left";
				}
				else
				{
					animatedSprite2D.Animation = "Idle_Down";
				}
			}

		}
		if (Input.IsActionJustReleased("MoveUp"))
		{
			if (velocity.X > 0)
			{
				animatedSprite2D.Animation = "Walk_Right";
			}
			else
			{
				if (velocity.X < 0)
				{
					animatedSprite2D.Animation = "Walk_Left";
				}
				else
				{
					animatedSprite2D.Animation = "Idle_Top";
				}
			}

		}

		velocity = velocity.Normalized() * Speed;
		return velocity;
	}

	private void AnimatedSprite2DOnAnimationFinished()
	{
		if (animatedSprite2D.Animation == "Attack_Right")
		{
			animatedSprite2D.Animation = "Idle_Right";
			animatedSprite2D.Play();
		}
		if (animatedSprite2D.Animation == "Attack_Left")
		{
			animatedSprite2D.Animation = "Idle_Left";
			animatedSprite2D.Play();
		}
		if (animatedSprite2D.Animation == "Attack_Down")
		{
			animatedSprite2D.Animation = "Idle_Down";
			animatedSprite2D.Play();
		}
		if (animatedSprite2D.Animation == "Attack_Top")
		{
			animatedSprite2D.Animation = "Idle_Top";
			animatedSprite2D.Play();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = getInput();
		Velocity = velocity;
		MoveAndSlide();
	}
}