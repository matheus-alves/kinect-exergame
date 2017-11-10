using UnityEngine;
using System.Collections;
using System.Collections.Specialized;

public class CoinBehaviourScript : MonoBehaviour {

	#region Fields
	
	// A reference to the coin
	public GameObject coin;
	
	// A reference to the user
	public GameObject User;
	
	// A reference to the effect of the coin catch
	public GameObject coinEffect;
	
	// A reference to the coin sound
	public GameObject coinSound;
	
	// The minimum speed of the coin
	public float MIN_SPEED = 0.1f;
	
	// The current speed of the object
	private float speed;
	
	// A thresholf in x axis so that
	// the user needs to move to catch the coin
	public float CollisionThreshold = 0.5f;
	
	// A flag to check if this object
	// has collided already, this is
	// useful to avoid multiple collisions
	private bool hasCollidedOnce = false;
	
	#endregion

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{	
		LevelScript levelScript = GameObject.Find("LevelText").GetComponent<LevelScript>();
		
		// Updates the speed based on the current level.
		// The higher the level, the higher the speed will be
		// i.e speed will de doubled after ten levels.
		// This will only happen up to level 10
		if (levelScript.CurrentLevel < 10)
		{
			speed = (MIN_SPEED * (levelScript.CurrentLevel / 10)) + MIN_SPEED;
		}
		
		if (transform.position.x > 0.0f) // positive x
		{
			// Check x axis
			if (transform.position.x - CollisionThreshold > User.transform.position.x) 
				transform.Translate(new Vector3(-speed, 0, 0));
		}
		else  // negative x
		{
			// Check x axis
			if (transform.position.x + CollisionThreshold < User.transform.position.x) 
				transform.Translate(new Vector3(speed, 0, 0));
		}
		
		// Check collision in z axis
		if (transform.position.z > -20.0f)
			transform.Translate(new Vector3(0, 0, -speed));
		else Object.Destroy(coin);
	}
	
	// Collision detection
	void OnTriggerEnter(Collider other)
	{		
		if (transform.position.z < 5.0) // BUG FIX
		{
			if (!hasCollidedOnce) 
			{				
				hasCollidedOnce = true;
				Instantiate(coinEffect, transform.position, Quaternion.identity);
				coinSound.audio.Play();
				
				GameObject.Find("PointsText").SendMessage("UpdatePoints");
			}
			
			Object.Destroy(coin);
		}
	}
}
