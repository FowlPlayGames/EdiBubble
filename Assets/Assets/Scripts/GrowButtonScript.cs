/*************************************************
PlayerScript - Jill Gray (Feb. 13, 2014)
	- Controls player growing
*************************************************/

using UnityEngine;
using System.Collections;

public class GrowButtonScript : MonoBehaviour {
	private RaycastHit hit	= new RaycastHit();								// Raycast object
	private Ray ray			= new Ray();									// Ray object
	
	public Sprite mainSprite;												// Regular grow button sprite
	public Sprite glowSprite;												// Glow grow button sprite
	private bool bMainSprite;												// Are we currently using the main sprite?

	public GameObject player;												// Player object
	private PlayerScript playerScript;										// Player script

	// Use this for initialization
	void Start ()
	{
		GetComponent<SpriteRenderer>().sprite = mainSprite;					// Set the current sprite to the regular sprite
		bMainSprite = true;													// Set using main sprite to true
		player = GameObject.Find( "PlayerBubble" );							// Set the player object
		playerScript = player.GetComponent<PlayerScript>();					// Set the script object
	}
	
	// Update is called once per frame
	void Update () 
	{
		// IF PLAYER HAS TOUCHED THE SCREEN
		if( Input.touchCount > 0 )
		{
			ray = Camera.main.ScreenPointToRay( 							// Set ray position
					Input.GetTouch( 0 ).position );
			
			// WE HIT SOMETHING -- LETS SEE WHAT IT WAS
			if( Physics.Raycast( ray, out hit, 15.0f ) )
			{
				// IF PLAYER PRESSED THE GROW BUTTON
				if( hit.transform.tag == "GrowButton" )
				{
					// ONLY SET SPRITE TO GLOW SPRITE IF ITS CURRENTLY SET AS MAIN SPRITE
					if( bMainSprite )
					{
						GetComponent<SpriteRenderer>().sprite = glowSprite;	// Change the sprite to the glow sprite
						bMainSprite = false;								// Set using main sprite to false
					}
					// IF PLAYER CAN GROW
					if( playerScript.GetCurrentSize() < playerScript.GetMaxGrowSize() )
					{
						playerScript.IncreaseCurrentSize( 					// Increase the current size by the grow rate
								playerScript.GetGrowRate() );
						playerScript.GrowShrinkBubble();					// Apply the change in size						
					}
				}
			}
		}
		// ELSE MAKE SURE WE USE THE REGULAR SPRITE
		else
		{
			// DON'T SET SPRITE TO MAIN SPRITE IF WE ARE ALREADY THERE
			if( !bMainSprite )
			{
				GetComponent<SpriteRenderer>().sprite = mainSprite;			// Set the current sprite to the regular sprite
				bMainSprite = true;											// Set main sprite back to true
			}
		}
	}
}
