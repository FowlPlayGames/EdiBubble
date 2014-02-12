/*************************************************
PlayerScript - Jill Gray (Feb. 11, 2014)
	- Controls player movement
	- Controls player growth both up and down
	- Modifies player data members on collision
*************************************************/

using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	// CONSTANT VARS
	private const float fYTiltCalibration = 0.6f;						// Calibration -- Might want to use first so many frames to capture this per user
	private const float fMinSize = 1.0f;								// Minimum size player bubble can be
	private const float fMaxSize = 5.0f;								// Maximum size player bubble can be
	private const float fSizeIncrement = 0.5f;							// How much to grow player bubble by
	private const float fOriginalReboundTime = 0.25f;					// Default rebound time
	public float fGrowRate = 0.05f;										// How fast does the player grow / shrink?

	// PUBLIC VARS
	public float fSpeed = 5.0f;											// Speed of the player bubble
	public float fCurrentSize;											// Current size of the player bubble
	public float fMaxGrowSize;											// Maximum size player bubble can grow to
	public float fReboundTimer;											// Timer to turn off rebounding
	public AudioClip acEatBubble;										// Sound to play when a bubble is eaten


	// PRIVATE VARS
	private Vector3 v3Tilt;												// Vector to hold tilt values
	private bool bRebounding;											// Did the player recently collide with rocks?
	public Vector3 v3ReboundVec;										// Rebounding velocity
	
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
		// IF NOT ALREADY REBOUNDING FROM A COLLISION WITH A WALL
		if( !bRebounding )												
		{
			v3Tilt.x = Input.acceleration.x * fSpeed * Time.deltaTime;	// Calculate tilt value in the x direction

			float fYInput = Input.acceleration.y;						// Get the current y tilt

			// IF THE CURRENT TILT IS GREATER THAN OR EQUAL TO THE CALIBRATION
			if( fYInput >= -fYTiltCalibration )							
			{
				v3Tilt.y = ( ( fYInput + fYTiltCalibration ) * fSpeed * // Calculate tilt value in the y direction
				            Time.deltaTime ) * fYTiltCalibration;
			}
			// ELSE THE CURRENT TILT IS LESS THAN THE CALIBRATION
			else
			{
				v3Tilt.y = ( ( fYInput + fYTiltCalibration ) * fSpeed * // Calculate tilt value in the y direction
				            Time.deltaTime ) * 4.0f * fYTiltCalibration;
			}		

			transform.position += v3Tilt;								// Update the overall position
		}
		// ELSE ALREADY REBOUNDING FROM A COLLISION WITH A WALL
		else
		{
			transform.position += ( v3ReboundVec * fSpeed * 			// Update the position by the rebound vector
			                         Time.deltaTime );
			fReboundTimer -= Time.deltaTime;							// Decrement the rebound timer
		}

		// IF THE REBOUND TIMER HAS EXPIRED
		if( fReboundTimer <= 0.0f )										
		{
			fReboundTimer = fOriginalReboundTime;						// Reset the rebound timer for the next collision with a wall
			bRebounding = false;										// Set fRebounding to false so that position can be updated via input
		}

		// IF THE KEYPAD PLUS KEY IS PRESSED AND PLAYER'S CURRENT SIZE IS LESS THAN THE MAX SIZE THEY CAN CURRENTLY GROW
		if( Input.GetKey( KeyCode.KeypadPlus ) && fCurrentSize < fMaxGrowSize )
		{
			Debug.Log ("Pressed the grow key" );
			fCurrentSize += fGrowRate;									// Increase the current size by the grow rate
			GrowShrinkBubble();											// Apply the change in size
		}
		// ELSE IF THE KEYPAD MINUS KEY IS PRESSED AND PLAYER'S CURRENT SIZE IS GREATER THAN THE MIN SIZE THEY CAN SHRINK TO
		else if( Input.GetKey( KeyCode.KeypadMinus ) && fCurrentSize > fMinSize )
		{
			Debug.Log( "Pressed the shrink key" );
			fCurrentSize -= fGrowRate;									// Decrease the current size by the grow rate
			GrowShrinkBubble();											// Apply the change in size
		}
	}

	void OnCollisionEnter2D( Collision2D other )
	{
		// TODO -- NEED TO WRITE THIS SHIT

		// IF YOU ATE A SMALLER BUBBLE
		if( other.gameObject.tag == "TestCube" )
		{
			Debug.Log( "You hit shit!" );

			audio.PlayOneShot( acEatBubble );							// Play eat bubble sound

			other.transform.position = 									// for testing -- move square to a random position
				new Vector3( Random.Range( -4.0f, 4.0f ), 
							 Random.Range( -4.0f, 4.0f ), 0.0f );		

			// IF THERE IS ROOM TO GROW
			if( fMaxGrowSize < fMaxSize )							
			{
				fMaxGrowSize += fSizeIncrement;							// Increment the max grow size
			}
		}
		// ELSE IF YOU COLLIDED WITH ROCKS ON THE LEFT OR THE RIGHT
		else if( other.gameObject.tag == "LeftRocks" || other.gameObject.tag == "RightRocks" )
		{
			Debug.Log( "Hit the left or right rocks" );
			Vector3 temp = Vector3.zero;								// Zero vector
			temp.x = Input.acceleration.x;								// Set tilt value in the x direction
			temp.y = Input.acceleration.y + fYTiltCalibration;			// Set tilt value in the y direction and account for calibration

			temp.x *= -1.0f;											// Reverse the x direction
			v3ReboundVec = temp;										// Set the rebound vector
			Rebound( fReboundTimer );									// Let the bubble rebound off the wall
		}
		// ELSE IF YOU COLLIDED WITH ROCKS ON THE TOP OR THE BOTTOM
		else if( other.gameObject.tag == "TopRocks" || other.gameObject.tag == "BottomRocks" )
		{
			Debug.Log( "Hit the top or bottom rocks" );
			Vector3 temp = Vector3.zero;								// Zero vector
			temp.x = Input.acceleration.x;								// Get tilt value in the x direction
			temp.y = Input.acceleration.y;								// Get tilt value in the y direction
		
			// IF THE CURRENT TILT IS GREATER THAN OR EQUAL TO THE NEGATIVE CALIBRATION
			if( temp.y >= -fYTiltCalibration )							
			{
				temp.y += fYTiltCalibration;							// Add calibration to the tilt
			}
			// ELSE THE CURRENT TILT IS LESS THAN THE NEGATIVE CALIBRATION
			else
			{
				temp.y += fYTiltCalibration * -4.2f / fSpeed;			// Add calibration to the tilt -- Don't multiply speed twice
			}	

			temp.y *= -1.0f;											// Reverse the y direction						
			v3ReboundVec = temp;										// Set the rebound vector
			Rebound( fReboundTimer );									// Let the bubble rebound off the wall
		}
	}

	public void Rebound( float timer )
	{
		bRebounding = true;												// Set rebounding to true -- will freeze controls
		fReboundTimer = timer;											// Set the rebound timer
	}

	public void GrowShrinkBubble()
	{
		transform.localScale = new Vector3( fCurrentSize, 				// Set the scale of the player bubble based on its current size
		                                     fCurrentSize, 0.0f );
	}
}
