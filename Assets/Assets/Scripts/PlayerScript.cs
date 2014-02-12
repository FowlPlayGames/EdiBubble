using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	// CONSTANT VARS
	private const float fYTiltCalibration = 0.4f;						// Calibration -- Might want to use first so many frames to capture this per user
	private const float fMinSize = 1.0f;								// Minimum size player bubble can be
	private const float fMaxSize = 10.0f;								// Maximum size player bubble can be
	private const float fSizeIncrement = 1.0f;							// How much to grow player bubble by
	private const float fOriginalReboundTime = 2.0f;

	// PUBLIC VARS
	public float fSpeed;												// Speed of the player bubble
	public float fCurrentSize;											// Current size of the player bubble
	public float fMaxGrowSize;											// Maximum size player bubble can grow to
	public float fReboundTimer;											// Timer to turn off rebounding

	// PRIVATE VARS
	private Vector3 v3Tilt;												// Vector to hold tilt values
	private bool bRebounding;											// Did the player recently collide with rocks?
	private Vector3 v3ReboundVec;



	void Start () 
	{
		v3Tilt 			= Vector3.zero;									// Zero out v3Tilt vector
		fCurrentSize 	= 1.0f;											// Player will start at size 1.0f
		fMaxGrowSize 	= 1.0f; 										// Player currently cannot grow past 1.0f
		fReboundTimer	= fOriginalReboundTime;							// Set the rebound timer
		bRebounding 	= false;										// Did the player recently collide with rocks?
	}

	void Update () 
	{
		if( !bRebounding )
		{
			v3Tilt.x = Input.acceleration.x * fSpeed * Time.deltaTime;	// Calculate tilt value in x direction
			v3Tilt.y = ( Input.acceleration.y + fYTiltCalibration ) * 	// Calculate tilt value in y direction and account for calibration
				fSpeed * Time.deltaTime;

			transform.position += v3Tilt;								// Update the overall position
		}
		else
		{
			transform.position += ( v3ReboundVec * fSpeed * Time.deltaTime );
			fReboundTimer -= Time.deltaTime;
		}

		if( fReboundTimer <= 0.0f )
		{
			fReboundTimer = fOriginalReboundTime;
			bRebounding = false;
		}

		// TODO -- CLIP THE BOUNDS OF THE BUBBLE
	}

	void OnCollisionEnter2D( Collision2D other )
	{
		// TODO -- NEED TO WRITE THIS SHIT
		if( other.gameObject.tag == "TestCube" )
		{
			Debug.Log( "You hit shit!" );

			other.transform.position = 
				new Vector3( Random.Range( -4.0f, 4.0f ), 
							 Random.Range( -4.0f, 4.0f ), 0.0f );		// for testing -- move square to a random position



			if( fMaxGrowSize < fMaxSize )								// If there is room to grow
			{
				fMaxGrowSize += fSizeIncrement;							// Increment the max grow size
				transform.localScale += new Vector3( 1.0f, 1.0f, 0.0f );// This will need to be moved to the growbubble function
			}
		}
		else if( other.gameObject.tag == "Rocks" )
		{
			Debug.Log( "Hit a rock" );

			Vector3 temp = Vector3.zero;
			temp.x = Input.acceleration.x;								// Calculate tilt value in x direction
			temp.y = ( Input.acceleration.y + fYTiltCalibration );		// Calculate tilt value in y direction and account for calibration

			v3ReboundVec = Rebound( fReboundTimer, temp );

			if( transform.position.x <= other.gameObject.transform.position.x )
			{
				transform.position += new Vector3( 0.01f, 0.0f, 0.0f );
			}
		}
	}

	public Vector3 Rebound( float timer, Vector3 tempTilt )
	{
		bRebounding = true;
		fReboundTimer = timer;

		return tempTilt * -1.0f;
	}

	public void GrowBubble( float currSize, float desiredSize )
	{
		// TODO -- NEED TO WRITE THIS SHIT TOO
	}
}
