using UnityEngine;
using System.Collections;

public class FireBubbleScript : EnemyScript {

	//constant
	float TERRITORY_RADIUS = 5.0f;	//how far away from the initial spawn the bubble will chase the player
	float CHASE_DISTANCE   = 500.0f;  //how far away from the bubble itself the player has to be before it will give chase

	//private
	Vector3 initialPosition;	//location the enemy spawned at


	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		GameObject player = GameObject.FindWithTag ("Player"); //find player

		//exit if there is no player
		if (player == null)
			return;

		//if player is in territory
		if ((initialPosition - player.transform.position).magnitude < TERRITORY_RADIUS) 
		{
			//and this bubble can see the player
			RaycastHit hit;
			Vector3 vToPlayer = player.transform.position - this.transform.position;
			if ( Physics.Raycast( this.transform.position,								//from the bubble's position
			                      vToPlayer.normalized,									//towards the player
			                      out hit,												//store result in hit
			                      CHASE_DISTANCE))										//don't chase if they are too far away
			{
				if (hit.collider.tag == "Player")
				{
					print ("I see you!"); // test
				}
			}
		}

	}
}
