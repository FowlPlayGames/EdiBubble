using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour 
{
	// public variables
	public Transform horizontalPiece; 	// the defualt horizontal piece 
	public Transform verticalPiece; 	// the defualt vertical piece
	public Transform [] verticalTurns;     // turns that connect vertical to horizontal 
	public Transform [] horizontalTurns; // turns that connect horizontal to vertical

	//private variables
	private PlayerScript player;		// access to player script
	private Vector2 playerPosition, firstPosition, currentPosition, lastPosition;      // marker for next spawn position
	private bool goingHorizontal, goingVertical; //bools to tell the numbe generator which numbers ar of access.
	private int  horizontalCount, verticalCount;

	// This is call first
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
	}
	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}
