using UnityEngine;
using System.Collections;

public class LevelPieceSpawnController : MonoBehaviour 
{
	//	Class Instance
	public static LevelPieceSpawnController Instance;
	//	Other Objects
	private GameObject player;
	public GameObject growBubble;
	public GameObject []enemies;
	public GameObject []powerUps;
	private GameObject []clones;
	private Transform fakePiece;
	// Class Variables
	private int enemyCount;
	private int growBubbleCount;
	private int powerUpCount;
	private int size = 10;
	private int currentCount = 0;
	private bool gotOne = false;
	public float distance = 0.0f;
	public int count = 0;
	void Awake()
	{
		Instance  = this;
		player = GameObject.FindGameObjectWithTag("Player");
		clones = new GameObject[size];
	}

	void Start()
	{

	}
	// Update is called once per frame
	void Update() 
	{
		distance = Vector2.Distance(player.transform.position, this.transform.position);
	
	}

	// do we want enemy 
	// dow we want powerup
	// how many grow bubbles do we want 

	void Spawn()
	{
		if(currentCount < size  && !gotOne)
		{
			if(distance <= 10.0f)
			{
				clones[currentCount] = Instantiate(growBubble, this.transform.position, this.transform.rotation) as GameObject;
				currentCount++;
				gotOne = true;
			}
		}
	}
}
