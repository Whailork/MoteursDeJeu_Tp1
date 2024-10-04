extends CharacterBody2D


const SPEED = 100.0
const JUMP_VELOCITY = -400.0

func get_input():
	velocity = Vector2()
	if Input.is_action_pressed("MoveRight"):
		velocity.x+=1

	if Input.is_action_pressed("MoveLeft"):
		velocity.x-=1

	if Input.is_action_pressed("MoveDown"):
		velocity.y += 1

	if Input.is_action_pressed("MoveUp"):
		velocity.y -= 1

	
	#pour les animations walk
	if Input.is_action_just_pressed("MoveRight"):
		$AnimatedSprite2D.animation = "Walk_Right"
		$AnimatedSprite2D.play()
	if Input.is_action_just_pressed("MoveLeft"):
		$AnimatedSprite2D.animation = "Walk_Left"
		$AnimatedSprite2D.play()
	if Input.is_action_just_pressed("MoveDown"):
		$AnimatedSprite2D.animation = "Walk_Down"
		$AnimatedSprite2D.play()
	if Input.is_action_just_pressed("MoveUp"):
		$AnimatedSprite2D.animation = "Walk_Top"
		$AnimatedSprite2D.play()
		
	# les animations pour le play dead
	if Input.is_action_just_pressed("PlayDead"):
		if $AnimatedSprite2D.animation == "Walk_Right" || $AnimatedSprite2D.animation == "Idle_Right":
			$AnimatedSprite2D.animation = "Die_Right"
					
		if $AnimatedSprite2D.animation == "Walk_Left" || $AnimatedSprite2D.animation == "Idle_Left":
			$AnimatedSprite2D.animation = "Die_Left"

		if $AnimatedSprite2D.animation == "Walk_Down" || $AnimatedSprite2D.animation == "Idle_Down":
			$AnimatedSprite2D.animation = "Die_Down"

		if $AnimatedSprite2D.animation == "Walk_Top" || $AnimatedSprite2D.animation == "Idle_Top":
			$AnimatedSprite2D.animation = "Die_Top"
	
	if Input.is_action_just_pressed("Attack"):
		if $AnimatedSprite2D.animation == "Walk_Right" || $AnimatedSprite2D.animation == "Idle_Right":
			$AnimatedSprite2D.animation = "Attack_Right"
					
		if $AnimatedSprite2D.animation == "Walk_Left" || $AnimatedSprite2D.animation == "Idle_Left":
			$AnimatedSprite2D.animation = "Attack_Left"

		if $AnimatedSprite2D.animation == "Walk_Down" || $AnimatedSprite2D.animation == "Idle_Down":
			$AnimatedSprite2D.animation = "Attack_Down"

		if $AnimatedSprite2D.animation == "Walk_Top" || $AnimatedSprite2D.animation == "Idle_Top":
			$AnimatedSprite2D.animation = "Attack_Top"

		
	## pour les animation de idle
	if Input.is_action_just_released("MoveRight"):
		$AnimatedSprite2D.animation = "Idle_Right"
		
	if Input.is_action_just_released("MoveLeft"):
		$AnimatedSprite2D.animation = "Idle_Left"
		
	if Input.is_action_just_released("MoveDown"):
		if velocity.x > 0:
			$AnimatedSprite2D.animation = "Walk_Right"
		else:
			if velocity.x < 0:
				$AnimatedSprite2D.animation = "Walk_Left"
			else:
				$AnimatedSprite2D.animation = "Idle_Down"
				
	if Input.is_action_just_released("MoveUp"):
		if velocity.x > 0:
			$AnimatedSprite2D.animation = "Walk_Right"
		else:
			if velocity.x < 0:
				$AnimatedSprite2D.animation = "Walk_Left"
			else:
				$AnimatedSprite2D.animation = "Idle_Top"
		
	velocity = velocity.normalized() * SPEED
	

func _physics_process(delta):
	get_input()
	move_and_slide()


func _on_animated_sprite_2d_animation_finished() -> void:
	if $AnimatedSprite2D.animation == "Attack_Right":
		$AnimatedSprite2D.animation = "Idle_Right"
		$AnimatedSprite2D.play()
	if $AnimatedSprite2D.animation == "Attack_Left":
		$AnimatedSprite2D.animation = "Idle_Left"
		$AnimatedSprite2D.play()
		
	if $AnimatedSprite2D.animation == "Attack_Down":
		$AnimatedSprite2D.animation = "Idle_Down"
		$AnimatedSprite2D.play()
		
	if $AnimatedSprite2D.animation == "Attack_Top":
		$AnimatedSprite2D.animation = "Idle_Top"
		$AnimatedSprite2D.play()
