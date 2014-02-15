using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour 
{
	// public variables
	public GameObject [] levelPieces;	// an array of level prefabs

	//private variables
	private PlayerScript player;		// access to player script
	private Vector2 spawnPosition;      // marker for next spawn position
	private bool goingHorizontal, goingVertical; //bools to tell the numbe generator which numbers ar of access.

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
