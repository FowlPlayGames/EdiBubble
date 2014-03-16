using UnityEngine;
using System.Collections;

public class GrowBubbleScript : MonoBehaviour {

	public Vector2 scaleSize;
	CircleCollider2D circleCollider;

	public float greenValue = 0.0f;
	public float blueValue = 1.0f;
	public float colorChangeRate = 0.05f;
	public bool greenValueIncreasing = true;

	// Use this for initialization
	void Start () 
	{
		float scale = Random.Range( 0.5f, 3.0f );
		scaleSize = new Vector2( scale, scale );
		transform.localScale = scaleSize;	
		circleCollider = gameObject.GetComponent<CircleCollider2D>();
		circleCollider.radius = 0.2f * scale * 0.5f;
	}
	
	// Update is called once per frame
	void Update () 
	{

		Color tintColor = GenerateTintColor( ref greenValue, ref blueValue, ref greenValueIncreasing );
		renderer.material.SetColor( "_Color", tintColor );
	}

	public Color GenerateTintColor( ref float green, ref float blue, ref bool increasing )
	{
		if( increasing && green < 1.0f )
		{
			green += colorChangeRate;
			blue = 1.0f - green;
			if( green >= 1.0f )
			{
				increasing = false;
			}
		}
		else if( !increasing && green > 0.0f )
		{
			green -= colorChangeRate;
			blue = 1.0f - green;
			if( green <= 0.0f )
			{
				increasing = true;
			}
		}

		return new Color( green, green, blue, 1.0f );
	}
}
