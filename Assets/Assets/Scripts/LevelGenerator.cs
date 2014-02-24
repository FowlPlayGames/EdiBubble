using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour 
{
	// public variables
	public GameObject 		horizontalPiece; 	
	public GameObject 		startPiece;      
	public GameObject 		verticalPiece;
	public GameObject 	[] 	verticalTurns;     	
	public GameObject 	[] 	horizontalTurns; 	
	public GameObject 	[]	piecesPlaced;		
	private GameObject  	tempPiecePlaced;
	//private variables
	private PlayerScript player;			
	private Vector2 playerPosition; 
	private Vector2 furthestPosition;
	private Vector2 capPosition;
	private Vector2 currentPosition; 
	private Vector2 newPosition;      
	private bool  	goingVertical; 	 
	private int  	straightCount;	
	private int 	size; 
	private int 	temp;
	// This is call first
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>(); 		
		currentPosition = player.transform.position;											
																								
		goingVertical  = true;

		Random.seed = System.DateTime.Now.Millisecond;															
		straightCount = Random.Range(2,7);														
		newPosition =  new Vector2(0,0);															
		size =100;
		piecesPlaced = new GameObject[size];
		temp = 0;
	}
	/*
	 * if vertical right  horizontal + 8 and gets horizontal left.
	 * if vertical left   horizontal - 8 and gets horizontal right.
	 * if horizontal left 
	 * 
	 * .
	 */
	// Use this for initialization
	void Start () 
	{
		tempPiecePlaced = Instantiate(startPiece, newPosition, Quaternion.identity) as GameObject;
		piecesPlaced[0] = tempPiecePlaced;


		for (int i = 1; i < piecesPlaced.Length; i++) 
		{

			if(goingVertical && straightCount > 0)
			{


				newPosition = new Vector2(newPosition.x ,newPosition.y + 8 );
				tempPiecePlaced = Instantiate(verticalPiece, newPosition, Quaternion.identity) as GameObject;
				piecesPlaced[i] = tempPiecePlaced;
				straightCount--;

			}
			else if(goingVertical && straightCount <= 0)
			{
			
				newPosition = new Vector2(newPosition.x, newPosition.y + 8);
				temp = Random.Range(10,12)-10;
				tempPiecePlaced = Instantiate(verticalTurns[temp], newPosition, verticalTurns[temp].transform.rotation) as GameObject;
				piecesPlaced[i] = tempPiecePlaced;
				straightCount = Random.Range(2,7);
				goingVertical = false;
			}
			else if(!goingVertical && straightCount > 0)
			{
				if(temp == 1)
					newPosition = new Vector2(newPosition.x + 8, newPosition.y);
				else
					newPosition = new Vector2(newPosition.x - 8, newPosition.y);

				tempPiecePlaced = Instantiate(horizontalPiece, newPosition, horizontalPiece.transform.rotation) as GameObject;
				piecesPlaced[i] = tempPiecePlaced;
				straightCount--;
			}
			else if(!goingVertical && straightCount <= 0)
			{
				if(temp == 1)
					newPosition = new Vector2(newPosition.x + 8, newPosition.y);
				else
					newPosition = new Vector2(newPosition.x - 8, newPosition.y);

				tempPiecePlaced = Instantiate(horizontalTurns[temp], newPosition, horizontalTurns[temp].transform.rotation) as GameObject;
				piecesPlaced[i] = tempPiecePlaced;
				straightCount = Random.Range(2,7);
				goingVertical = true;
			}

			
			piecesPlaced[i].transform.parent = this.transform;
		}
	}

}