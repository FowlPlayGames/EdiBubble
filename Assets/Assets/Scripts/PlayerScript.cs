/*************************************************
PlayerScript - Jill Gray (Feb. 11, 2014)
	- Controls player movement
*************************************************/

using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public static PlayerScript playerScript;							// Creates an instance of the script.
	CircleCollider2D circleCollider;

	// CONSTANT VARS
	private const float fYTiltCalibration 		= 0.3f;					// Calibration -- Might want to use first so many frames to capture this per user
	private const float fMinSize 				= 1.0f;					// Minimum size player bubble can be
	private const float fMaxSize 				= 5.0f;					// Maximum size player bubble can be
	private const float fSizeIncrement 			= 0.5f;					// How much to grow player bubble by
	private const float fOriginalReboundTime 	= 0.25f;				// Default rebound time
	private const float fGrowRate 				= 0.05f;				// How fast does the player grow / shrink?
	private const float fMinTilt 				= -0.03f;				// Minimum tilt
	private const float fMaxTilt 				= 0.03f;				// Maximum tilt

	// PUBLIC VARS
	private float fSpeed;												// Speed of the player bubble
	public float fCurrentSize;											// Current size of the player bubble
	public float fMaxGrowSize;											// Maximum size player bubble can grow to
	public float fReboundTimer;											// Timer to turn off rebounding
	public AudioClip acEatBubble;										// Sound to play when a bubble is eaten

	// PRIVATE VARS
	private Vector3 v3Tilt;												// Vector to hold tilt values
	private bool bRebounding;											// Did the player recently collide with rocks?
	private Vector3 v3ReboundVec;										// Rebounding velocity

	private bool bUsingKeyboard = true;

	void Start () 
	{
		v3Tilt 			= Vector3.zero;									// Zero out v3Tilt vector
		fCurrentSize 	= 1.0f;											// Player will start at size 1.0f
		fMaxGrowSize 	= 1.0f; 										// Player currently cannot grow past 1.0f
		fReboundTimer	= fOriginalReboundTime;							// Set the rebound timer
		bRebounding 	= false;										// Did the player recently collide with rocks?
		fSpeed			= 100.0f;										// Set the speed of the player bubble
	}

	void Update()
	{
		circleCollider = gameObject.GetComponent<CircleCollider2D>();
		circleCollider.radius = 0.04f * transform.localScale.x;
	}

	void FixedUpdate () 
	{
		// TAKE THIS IF STATEMENT OUT ONCE TESTING WITH KEYBOARD IS COMPLETE
		if( !bUsingKeyboard )
		{
			// IF NOT ALREADY REBOUNDING FROM A COLLISION WITH A WALL
			if( !bRebounding )												
			{
				v3Tilt = Input.acceleration;									// Get the tilt vector
				v3Tilt.z = 0.0f;												// Zero out z component

				v3Tilt.x = ClipTilt( v3Tilt.x );								// Check if tilt x value is too large or small
				v3Tilt.x *= fSpeed * Time.deltaTime;							// Calculate position change in the x direction

				v3Tilt.y += fYTiltCalibration;									// Calculate tilt in the y direction
				v3Tilt.y = ClipTilt( v3Tilt.y );								// Check if tilt y value is too large or small
				v3Tilt.y *= fSpeed * Time.deltaTime;							// Calculate position change in the y direction

				transform.position += v3Tilt;									// Update the overall position
			}
			// ELSE ALREADY REBOUNDING FROM A COLLISION WITH A WALL -- STILL RECOVERING FROM HITTING A WALL
			else
			{
				transform.position += v3ReboundVec;								// Update the position by the rebound vector
				fReboundTimer -= Time.deltaTime;								// Decrement the rebound timer
			}

			// IF THE REBOUND TIMER HAS EXPIRED
			if( fReboundTimer <= 0.0f )										
			{
				fReboundTimer = fOriginalReboundTime;							// Reset the rebound timer for the next collision with a wall
				bRebounding = false;											// Set fRebounding to false so that position can be updated via input
			}
		}


		// FOR TESTING WITH THE KEYBOARD
		else
		{
			if( Input.GetKey( KeyCode.UpArrow ) )
			{
				Debug.Log( "pressed up" );
				transform.position += new Vector3( 0.0f, 2.0f * Time.deltaTime, 0.0f );
			}
			if( Input.GetKey( KeyCode.DownArrow ) )
			{
				transform.position += new Vector3( 0.0f, -2.0f * Time.deltaTime, 0.0f );
			}
			if( Input.GetKey( KeyCode.LeftArrow ) )
			{
				transform.position += new Vector3( -2.0f * Time.deltaTime, 0.0f, 0.0f );
			}
			if( Input.GetKey( KeyCode.RightArrow ) )
			{
				transform.position += new Vector3( 2.0f * Time.deltaTime, 0.0f, 0.0f );
			}
		}
	}

	void OnCollisionEnter2D( Collision2D other )
	{
		// TODO: Make sure this bubble you are eating is smaller than you
		// IF YOU ATE A SMALLER BUBBLE
		if( other.gameObject.tag == "GrowBubble" && transform.localScale.x >= other.transform.localScale.x )
		{
			Debug.Log( "You ate a grow bubble!" );

			audio.PlayOneShot( acEatBubble );								// Play eat bubble sound

			Destroy( other.gameObject );									// Destroy the grow bubble	

			// IF THERE IS ROOM TO GROW
			if( fMaxGrowSize < fMaxSize )							
			{
				fMaxGrowSize += fSizeIncrement;								// Increment the max grow size
			}
		}
		// ELSE IF YOU COLLIDED WITH ROCKS ON THE LEFT OR THE RIGHT
		else if( other.gameObject.tag == "LeftRocks" || other.gameObject.tag == "RightRocks" )
		{
			Debug.Log( "Hit the left or right rocks" );
			Vector3 tempTilt = Input.acceleration;							// Get the tilt vector
			tempTilt.z = 0.0f;												// Zero out z component

			tempTilt.x = ClipTilt( tempTilt.x );							// Check to see if temptilt in x is too large or small
			tempTilt.x *= fSpeed * Time.deltaTime;							// Calculate position change in the x direction

			tempTilt.y += fYTiltCalibration;								// Calculate tilt in the y direction
			tempTilt.y = ClipTilt( tempTilt.y );							// Check to see if temptilt in y is too large or small
			tempTilt.y *= fSpeed * Time.deltaTime;							// Calculate position change in the y direction

			tempTilt.x *= -1.0f;											// Reverse the x direction
			v3ReboundVec = tempTilt;										// Set the rebound vector
			Rebound( fReboundTimer );										// Let the bubble rebound off the wall
		}
		// ELSE IF YOU COLLIDED WITH ROCKS ON THE TOP OR THE BOTTOM
		else if( other.gameObject.tag == "TopRocks" || other.gameObject.tag == "BottomRocks" )
		{
			Debug.Log( "Hit the top or bottom rocks" );
			Vector3 tempTilt = Input.acceleration;							// Get the tilt vector
			tempTilt.z = 0.0f;												// Zero out z component

			tempTilt.x *= fSpeed * Time.deltaTime;							// Calculate the tilt in the x direction
			tempTilt.x = ClipTilt( tempTilt.x );							// Check to see if temptilt in x is too large or small

			tempTilt.y = ( tempTilt.y + fYTiltCalibration ) *				// Calculate the tilt in the y direction
				fSpeed * Time.deltaTime;
			tempTilt.y = ClipTilt( tempTilt.y );							// Check to see if the temptilt in y is too large or small
		
			tempTilt.y *= -1.0f;											// Reverse the y direction						
			v3ReboundVec = tempTilt;										// Set the rebound vector
			Rebound( fReboundTimer );										// Let the bubble rebound off the wall
		}
	}

	public void Rebound( float timer )
	{
		bRebounding = true;													// Set rebounding to true -- will freeze controls
		fReboundTimer = timer;												// Set the rebound timer
	}

	public void GrowShrinkBubble()
	{
		transform.localScale = new Vector3( fCurrentSize, 					// Set the scale of the player bubble based on its current size
		                                     fCurrentSize, 0.0f );
	}

	public float ClipTilt( float fTiltVal )
	{
		// IF TILT VALUE IS LESS THAN MINIMUM TILT
		if( fTiltVal < fMinTilt )
		{
			return fMinTilt;												// Return mininum tilt
		}
		// ELSE IF TILT VALUE IS GREATER THAN MAXIMUM TILT
		else if ( fTiltVal > fMaxTilt )
		{
			return fMaxTilt;												// Return maximum tilt
		}
		// ELSE TILT IS OK
		else
		{
			return fTiltVal;												// Return current tilt
		}
	}

	public void IncreaseCurrentSize( float increase )
	{
		fCurrentSize += increase;											// Increase current size
	}

	public void DecreaseCurrentSize( float decrease )
	{
		fCurrentSize -= decrease;											// Decrease current size
	}

	public float GetGrowRate()
	{
		return fGrowRate;													// Return the grow rate
	}

	public float GetCurrentSize()
	{
		return fCurrentSize;												// Return the current size
	}

	public float GetMaxGrowSize()
	{
		return fMaxGrowSize;												// Return the max grow size
	}

	public float GetMinSize()
	{
		return fMinSize;													// Return the min size
	}
}
