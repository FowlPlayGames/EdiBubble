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
	private GameObject holder;
	// Class Variables
	private int size = 100;
	public int currentCount = 0;
	private int bubbleCount = 0;
	private float distance = 0.0f;
	private bool gotOne = false;
	private bool getEnemy = false;
	private bool getPowerup = false;
	void Awake()
	{
		Instance  = this;
		player = GameObject.FindGameObjectWithTag("Player");
		holder = GameObject.FindGameObjectWithTag("Holder");
		clones = new GameObject[size];
	}

	void Start()
	{

	}
	// Update is called once per frame
	void Update() 
	{
		if(!gotOne)
			distance = Vector2.Distance(player.transform.position, this.transform.position);

		if(distance < 10 && !gotOne)
		{
			GetCounts();

		}
	
	}

	void GetCounts()
	{
		// do we want a enemy;
		int temp = Random.Range(0,3);

		if(temp == 1)
			getEnemy = true;
		else
			getEnemy = false;

		// do we want a power up;
		temp = Random.Range(0,3);

		if(temp == 1)
			getPowerup = true;
		else
			getPowerup = false;

		//how many bubbles do we want
		temp = Random.Range(0,4);

		Spawn(temp);
	}
	void Spawn(int bubbleCount)
	{

		if(currentCount < size )
		{
			for (int i = 0; i < bubbleCount; i++) 
			{
				clones[currentCount] = Instantiate(growBubble, this.transform.position, this.transform.rotation) as GameObject;
				clones[currentCount].transform.parent = holder.transform;
				currentCount++;

			}

			if(getEnemy)
			{
				int temp = Random.Range(0,4);
				clones[currentCount] = Instantiate(enemies[temp], new Vector2(this.transform.position.x,this.transform.position.y - 2), this.transform.localRotation) as GameObject;
				clones[currentCount].transform.parent = holder.transform;
				currentCount++;

			}

			if(getPowerup)
			{
				int temp = Random.Range(0,2);
				clones[currentCount] = Instantiate(powerUps[temp], new Vector2(this.transform.position.x,this.transform.position.y + 2), this.transform.localRotation) as GameObject;
				clones[currentCount].transform.parent = holder.transform;
				currentCount++;
			

			}
	
			gotOne = true;
		}
	}
}
