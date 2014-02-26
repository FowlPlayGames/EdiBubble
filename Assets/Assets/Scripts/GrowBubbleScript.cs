using UnityEngine;
using System.Collections;

public class GrowBubbleScript : MonoBehaviour {

	public Vector2 scaleSize;
	CircleCollider2D circleCollider;

	// Use this for initialization
	void Start () {
		float scale = Random.Range( 0.5f, 3.0f );
		scaleSize = new Vector2( scale, scale );
		transform.localScale = scaleSize;	
		circleCollider = gameObject.GetComponent<CircleCollider2D>();
		circleCollider.radius = 0.2f * scale * 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
