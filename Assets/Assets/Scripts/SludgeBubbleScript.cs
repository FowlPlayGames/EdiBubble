using UnityEngine;
using System.Collections;

public class SludgeBubbleScript : EnemyScript {

	// Use this for initialization
	void Awake()
	{
		bIsAlive = true;							// Set the enemy to be alive
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public override void MoveEnemy()
	{
		// Possible movement description -- move a little, stop for a little, rinse and repeat
		// Maybe have this enemy move in random direction each time it starts from a stop
	}
}
