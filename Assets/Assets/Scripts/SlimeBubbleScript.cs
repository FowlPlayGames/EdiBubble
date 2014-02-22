using UnityEngine;
using System.Collections;

public class SlimeBubbleScript : EnemyScript {

	// PUBLIC VARIABLES
	public float fAmplitude 		= 0.0f;									// Current amplitude

	// CONSTANT VARIABLES
	float fAmplitudeRate 			= 5.0f;									// How fast do we cycle through 0 to 360 degrees?


	// Use this for initialization
	void Awake()
	{}

	void Start () 
	{
		v3Velocity = new Vector3( 2.0f, 2.0f, 0.0f );						// Set the velocity
	}
	
	// Update is called once per frame
	void Update () 
	{
		MoveEnemy();														// Move the enemy
	}

	void OnCollisionEnter2D( Collision2D other )
	{
		if( other.gameObject.tag == "RightRocks" )
		{
			Debug.Log( "Slime bubble hit the right rocks" );
			Vector3 temp = v3Velocity;
			temp.x *= -1.0f;
			v3Velocity = temp;
		}
		else if( other.gameObject.tag == "LeftRocks" )
		{
			Debug.Log( "Slime bubble hit the left rocks" );
			Vector3 temp = v3Velocity;
			temp.x *= -1.0f;
			v3Velocity = temp;
		}
	}

	public override void MoveEnemy()
	{
		Vector3 temp = new Vector3( v3Velocity.x, Mathf.Sin( fAmplitude ) *	// Calculate the change in position
		                            v3Velocity.y, 0.0f );
		transform.position += ( temp * Time.deltaTime );					// Update the position
		
		fAmplitude += Time.deltaTime * fAmplitudeRate;						// Update the current amplitude

		if( fAmplitude >= Mathf.PI * 2.0f )									// If the amplitude is greater than or equal to 360.0f
		{
			fAmplitude -= Mathf.PI * 2.0f;									// Clip the amplitude
		}
	}
}
