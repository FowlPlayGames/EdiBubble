using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	// CONSTANT VARS
	private const float fYTiltCalibration = 0.4f;						// Calibration -- Might want to use first so many frames to capture this per user
	private const float fMinSize = 1.0f;								// Minimum size player bubble can be
	private const float fMaxSize = 10.0f;								// Maximum size player bubble can be
	private const float fSizeIncrement = 1.0f;							// How much to grow player bubble by

	// PUBLIC VARS
	public float fSpeed;												// Speed of the player bubble
	public float fCurrentSize;											// Current size of the player bubble
	public float fMaxGrowSize;											// Maximum size player bubble can grow to

	// PRIVATE VARS
	private Vector3 v3Tilt;												// Vector to hold tilt values


	void Start () 
	{
		v3Tilt = Vector3.zero;											// Zero out v3Tilt vector
		fCurrentSize = 1.0f;											// Player will start at size 1.0f
		fMaxGrowSize = 1.0f; 											// Player currently cannot grow past 1.0f
	}

	void Update () 
	{
		v3Tilt.x = Input.acceleration.x * fSpeed * Time.deltaTime;		// Calculate tilt value in x direction
		v3Tilt.y = ( Input.acceleration.y + fYTiltCalibration ) * 		// Calculate tilt value in y direction and account for calibration
			fSpeed * Time.deltaTime;

		transform.position += v3Tilt;									// Update the overall position

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
	}

	public void GrowBubble()
	{
		// TODO -- NEED TO WRITE THIS SHIT TOO
	}
}
