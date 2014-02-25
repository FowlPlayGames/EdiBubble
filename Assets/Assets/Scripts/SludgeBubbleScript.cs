/*************************************************
SludgeBubbleScript - Jill Gray (Feb. 24, 2014)
	- Controls AI SludgeBubble movement
*************************************************/

using UnityEngine;
using System.Collections;

public class SludgeBubbleScript : EnemyScript {

	// PUBLIC VARIABLES
	public float fAcceleration;													// Sludge bubble acceleration

	// CONSTANT VARIABLES
	public const float fMaxSpeed = 5.0f;										// Sludge bubble maximum speed
	public const float fConstAccel = 0.05f;										// Sludge bubble constant acceleration -- either negative or positive


	void Awake()
	{
		bIsAlive 		= true;													// Set the enemy to be alive
		Random.seed 	= System.DateTime.Now.Millisecond;						// Seed random number;
		fAcceleration 	= -0.05f;												// Set the sludge bubble's acceleration
	}

	void Start () 
	{
		CreateRandomAcceleration( ref fAcceleration );							// Set the acceleration
		CreateRandomVelocity( ref v3Velocity );									// Set the random velocity
	}
	

	void Update () 
	{
		MoveEnemy();															// Move the sludge bubble	
	}

	public override void MoveEnemy()
	{
		transform.position += ( v3Velocity * Time.deltaTime );					// Update the position
		v3Velocity += new Vector3( fAcceleration, fAcceleration, 0.0f );		// Update the velocity

		// CHECK TO SEE IF THE THE SLUDGE BUBBLE IS SLOW ENOUGH TO CHANGE DIRECTION
		if( v3Velocity.magnitude < 1.0f )
		{	
			CreateRandomAcceleration( ref fAcceleration );						// Set new random acceleration
			CreateRandomVelocity( ref v3Velocity );								// Set new random velocity
		}

		// SEE IF THE MAX SPEED HAS BEEN SURPASSED
		else if( v3Velocity.magnitude >= fMaxSpeed )
		{
			fAcceleration *= -1.0f;												// Decelerate
		}
	}

	void CreateRandomVelocity( ref Vector3 velocity )
	{
		float velX, velY;														// Temp variables
		velX = Random.Range( -1.0f, 1.0f );										// Set x to random value
		velY = Random.Range( -1.0f, 1.0f );										// Set y to random value
		velocity = new Vector3( velX, velY, 0.0f );								// Set the new velocity
		velocity.Normalize();													// Normalize the velocity
	}

	void CreateRandomAcceleration( ref float acceleration )
	{
		float temp = Random.Range( -1.0f, 1.0f );								// Get a negative or positive value -- 0 will count as positive
		if( temp < 0.0f )
			acceleration = -fConstAccel;										// Set the acceleration to be negative
		else
			acceleration = fConstAccel;											// Set the acceleration to be positive
	}

	void OnCollisionEnter2D( Collision2D other )
	{
		// IF A LEFT OR RIGHT BARRIER IS HIT
		if( other.gameObject.tag == "RightRocks" || other.gameObject.tag == "LeftRocks" )
		{
			float tempX = -v3Velocity.x;										// Negate the x component of velocity
			v3Velocity = new Vector3( tempX, v3Velocity.y, 0.0f );				// Set the new velocity
		}
		// IF A TOP OR BOTTOM BARRIER IS HIT
		else if( other.gameObject.tag == "TopRocks" || other.gameObject.tag == "BottomRocks" )
		{
			float tempY = -v3Velocity.y;										// Negate the y component of velocity
			v3Velocity = new Vector3( v3Velocity.x, tempY, 0.0f );				// Set the new velocity
		}
	}
}
