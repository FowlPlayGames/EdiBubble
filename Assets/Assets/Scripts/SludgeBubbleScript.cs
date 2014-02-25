using UnityEngine;
using System.Collections;

public class SludgeBubbleScript : EnemyScript {

	// PUBLIC VARIABLES
	public float fAcceleration;
	public float fStopTimer;


	// PRIVATE VARIABLES
	bool bMoving;

	// CONSTANT VARIABLES
	public const float fMaxSpeed = 5.0f;

	// Use this for initialization
	void Awake()
	{
		bIsAlive = true;								// Set the enemy to be alive
		v3Velocity = new Vector3( 0.0f, 0.0f, 0.0f );	// Set the enemy velocity
		bMoving = true;
		fStopTimer = 2.0f;
	}

	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		MoveEnemy();
	}

	public override void MoveEnemy()
	{
		// Possible movement description -- move a little, stop for a little, rinse and repeat
		// Maybe have this enemy move in random direction each time it starts from a stop

		// xf=xi+vi+.5at2
		if( bMoving )
		{
			transform.position += ( v3Velocity * Time.deltaTime );						// Update the position

			float tempX = v3Velocity.x + fAcceleration;

			Debug.Log( "Temp: " + tempX.ToString() );

			if( tempX < Mathf.Abs( v3Velocity.x ) )
			{
				fAcceleration *= -1.0f;
				bMoving = false;
			}

			v3Velocity = new Vector3( tempX , 0.0f, 0.0f );

			// SEE IF THE MAX SPEED HAS BEEN SURPASSED
			if( Mathf.Abs( v3Velocity.x ) >= fMaxSpeed )
			{
				Debug.Log( "Decelerating" );
				fAcceleration *= -1.0f;													// Decelerate
			}
		}
		else
		{
			fStopTimer -= Time.deltaTime;
			if( fStopTimer <= 0.0f )
			{
				bMoving = true;
			}
		}
	}
}
