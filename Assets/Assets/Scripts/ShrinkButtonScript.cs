/*************************************************
PlayerScript - Jill Gray (Feb. 13, 2014)
	- Controls player movement
	- Controls player growth both up and down
	- Modifies player data members on collision
*************************************************/

using UnityEngine;
using System.Collections;

public class ShrinkButtonScript : MonoBehaviour {
	private RaycastHit hit	= new RaycastHit();							// Raycast object
	private Ray ray			= new Ray();								// Ray object
	
	public Sprite mainSprite;											// Regular grow button sprite
	public Sprite glowSprite;											// Glow grow button sprite
	
	public GameObject player;											// Player object
	private PlayerScript playerScript;									// Player script
	
	// Use this for initialization
	void Start ()
	{
		GetComponent<SpriteRenderer>().sprite = mainSprite;				// Set the current sprite to the regular sprite
		player = GameObject.Find( "PlayerBubble" );						// Set the player object
		playerScript = player.GetComponent<PlayerScript>();				// Set the script object
	}
	
	// Update is called once per frame
	void Update () 
	{
		// IF PLAYER HAS TOUCHED THE SCREEN
		if( Input.touchCount > 0 )
		{
			ray = Camera.main.ScreenPointToRay( 						// Set ray position
					Input.GetTouch( 0 ).position );
			
			// WE HIT SOMETHING -- LETS SEE WHAT IT WAS
			if( Physics.Raycast( ray, out hit, 15.0f ) )
			{
				// IF PLAYER PRESSED THE SHRINK BUTTON
				if( hit.transform.tag == "ShrinkButton" )
				{
					GetComponent<SpriteRenderer>().sprite = glowSprite;	// Change the sprite to the glow sprite
					// IF PLAYER CAN SHRINK
					if( playerScript.GetCurrentSize() > playerScript.GetMinSize() )
					{
						playerScript.DecreaseCurrentSize( 				// Decrease the current size by the grow rate
								playerScript.GetGrowRate() );
						playerScript.GrowShrinkBubble();				// Apply the change in size						
					}
				}
			}
		}
		// ELSE MAKE SURE WE USE THE REGULAR SPRITE
		else
		{
			GetComponent<SpriteRenderer>().sprite = mainSprite;			// Set the current sprite to the regular sprite
		}
	}
}