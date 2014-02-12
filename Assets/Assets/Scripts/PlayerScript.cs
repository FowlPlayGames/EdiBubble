using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	// CONSTANT VARS
	private const float fYTiltCalibration = 0.4f;						// Calibration -- Might want to use first so many frames to capture this per user
	private const float fMinSize = 1.0f;								// Minimum size player bubble can be
	private const float fMaxSize = 5.0f;								// Maximum size player bubble can be
	private const float fSizeIncrement = 1.0f;							// How much to grow player bubble by
	private const float fOriginalReboundTime = 0.25f;					// Default rebound time
	public float fGrowRate = 0.1f;											// How fast does the player grow / shrink?

	// PUBLIC VARS
	public float fSpeed;												// Speed of the player bubble
	public float fCurrentSize;											// Current size of the player bubble
	public float fMaxGrowSize;											// Maximum size player bubble can grow to
	public float fReboundTimer;											// Timer to turn off rebounding
	public AudioClip acEatBubble;										// Sound to play when a bubble is eaten


	// PRIVATE VARS
	private Vector3 v3Tilt;												// Vector to hold tilt values
	private bool bRebounding;											// Did the player recently collide with rocks?
	private Vector3 v3ReboundVec;										// Rebounding velocity
	
	void Start () 
	{
		v3Tilt 			= Vector3.zero;									// Zero out v3Tilt vector
		fCurrentSize 	= 1.0f;											// Player will start at size 1.0f
		fMaxGrowSize 	= 1.0f; 										// Player currently cannot grow past 1.0f
		fReboundTimer	= fOriginalReboundTime;							// Set the rebound timer
		bRebounding 	= false;										// Did the player recently collide with rocks?
	}

	void FixedUpdate () 
	{
		if( !bRebounding )												// If not already rebounding from a collision with wall
		{
			v3Tilt.x = Input.acceleration.x * fSpeed * Time.deltaTime;	// Calculate tilt value in x direction
			v3Tilt.y = ( Input.acceleration.y + fYTiltCalibration ) * 	// Calculate tilt value in y direction and account for calibration
				fSpeed * Time.deltaTime;

			transform.position += v3Tilt;								// Update the overall position
		}
		else
		{
			transform.position += ( v3ReboundVec * fSpeed * 			// Update the position by the rebound vector
			                         Time.deltaTime );
			fReboundTimer -= Time.deltaTime;							// Decrement the rebound timer
		}

		if( fReboundTimer <= 0.0f )										// Check to see if the rebound timer has expired
		{
			fReboundTimer = fOriginalReboundTime;						// Reset the rebound timer for the next collision with a wall
			bRebounding = false;										// Set fRebounding to false so that position can be updated via input
		}

		if( Input.GetKey( KeyCode.KeypadPlus ) && fCurrentSize < fMaxGrowSize )
		{
			Debug.Log ("Pressed that shitty key" );
			fCurrentSize += fGrowRate;
			GrowBubble();
		}
	}

	void OnCollisionEnter2D( Collision2D other )
	{
		// TODO -- NEED TO WRITE THIS SHIT
		// IF YOU ATE A SMALLER BUBBLE
		if( other.gameObject.tag == "TestCube" )
		{
			Debug.Log( "You hit shit!" );

			audio.PlayOneShot( acEatBubble );

			other.transform.position = 
				new Vector3( Random.Range( -4.0f, 4.0f ), 
							 Random.Range( -4.0f, 4.0f ), 0.0f );		// for testing -- move square to a random position

			if( fMaxGrowSize < fMaxSize )								// If there is room to grow
			{
				fMaxGrowSize += fSizeIncrement;							// Increment the max grow size
			}
		}
		// IF YOU COLLIDED WITH ROCKS ON THE LEFT OR THE RIGHT
		else if( other.gameObject.tag == "LeftRocks" || other.gameObject.tag == "RightRocks" )
		{
			Debug.Log( "Hit the left or right rocks" );
			Vector3 temp = Vector3.zero;
			temp.x = Input.acceleration.x;								// Calculate tilt value in x direction
			temp.y = Input.acceleration.y + fYTiltCalibration;			// Calculate tilt value in y direction and account for calibration

			temp.x *= -1.0f;
			v3ReboundVec = temp;
			Rebound( fReboundTimer );
		}
		else if( other.gameObject.tag == "TopRocks" || other.gameObject.tag == "BottomRocks" )
		{
			Debug.Log( "Hit the top or bottom rocks" );
			Vector3 temp = Vector3.zero;
			temp.x = Input.acceleration.x;
			temp.y = Input.acceleration.y + fYTiltCalibration;

			temp.y *= -1.0f;
			v3ReboundVec = temp;
			Rebound( fReboundTimer );
		}
	}

	public void Rebound( float timer )
	{
		bRebounding = true;
		fReboundTimer = timer;
	}

	public void GrowBubble()
	{
		// TODO -- NEED TO WRITE THIS SHIT TOO
		transform.localScale = new Vector3( fCurrentSize, 				// This will need to be moved to the growbubble function
		                                     fCurrentSize, 0.0f );
	}
}
