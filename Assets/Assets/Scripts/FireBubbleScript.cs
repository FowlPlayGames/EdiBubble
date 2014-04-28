using UnityEngine;
using System.Collections;

public class FireBubbleScript : EnemyScript {

	//constant
	float TERRITORY_RADIUS = 5.0f;	  //how far away from the initial spawn the bubble will chase the player
	float RETURN_SPEED	   = 1.0f;    //how quickly the bubble returns to its start point
	float CHASE_SPEED	   = 2.0f;    //how quickly the bubble chases the player

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

		//update position
		transform.position += ( v3Velocity * Time.deltaTime );

		//update velocity
		if ( (player.transform.position - initialPosition).magnitude < TERRITORY_RADIUS)
		{
			//chase player
			Vector3 vToPlayer = player.transform.position - this.transform.position;
			v3Velocity = vToPlayer.normalized * CHASE_SPEED;
		}
		else
		{
			//return to initial position
			Vector3 vToInitialPosition = initialPosition - this.transform.position;
			v3Velocity = vToInitialPosition.normalized * RETURN_SPEED;
		}

	}
}
