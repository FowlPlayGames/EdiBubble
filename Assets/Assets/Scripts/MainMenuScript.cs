//**Christopher Hake**//
//MainMenuScript.cs: displays the main menu and defines associated actions

using UnityEngine;
using System.Collections;



public class MainMenuScript : MonoBehaviour {

	//To adapt for various screen sizes, no objects can be given an absolute position.  
	//So these variables are used to help determine placement
	//adjust the floats to change the button's size and spacing
	private int buttonWidth = (int)(0.1f * Screen.width); //10% the screen
	private int buttonHeight = (int)(0.1f * Screen.height); //10% of the screen
	private int buttonGapHeight = (int)(0.1 * Screen.height); //10% of the screen

	public GUISkin UISkin = null;
	
	//called every frame to create the GUI
	void OnGUI () {


		GUI.skin = UISkin;

		//initial button location
		int left = (Screen.width/2) - (buttonWidth/2); //center of button should be at center of screen
		int top = Screen.height/2 - (int)(buttonHeight*1.5f) - buttonGapHeight; //top of first button should be 1.5 buttons, plus the botton gap, above the center of the screen

		if (GUI.Button (new Rect (left, top, buttonWidth, buttonHeight), "Start Game")) 
		{
			//TODO: start the game here
		}

		top += buttonHeight + buttonGapHeight; //but next button below the last one

		if (GUI.Button ( new Rect (left, top, buttonWidth, buttonHeight), "Options"))
		{
			//TODO: load options menu here
		}

		top += buttonHeight + buttonGapHeight; //but next button below the last one

		if (GUI.Button ( new Rect (left, top, buttonWidth, buttonHeight), "Quit Game"))
		{
			//TODO: quit the game here
		}

	}
}
