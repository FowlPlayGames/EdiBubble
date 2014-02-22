using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public bool 	bIsAlive;							// Is the enemy alive?
	public float 	fSize;								// Size / scale of the enemy
	public Vector3 	v3Velocity;							// Velocity vector

	// Use this for initialization
	void Awake()
	{}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetIsAlive( bool alive )
	{
		bIsAlive = alive;
	}

	public bool GetIsAlive()
	{
		return bIsAlive;
	}


	public float GetSize()
	{
		return fSize;
	}

	public virtual void MoveEnemy()
	{}
}
