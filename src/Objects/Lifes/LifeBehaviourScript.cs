using UnityEngine;
using System.Collections;

public class LifeBehaviourScript : MonoBehaviour 
{
	#region Fields
	
	// A reference to the life box
	public GameObject life;
	
	// A reference to the user
	public GameObject User;
	
	// A reference to the life catch effect
	public GameObject lifeEffect;
	
	// A reference to the life sound
	public GameObject lifeSound;
	
	// The minimum speed of the life box
	public float MIN_SPEED = 0.2f;
	
	// The current speed of the object
	private float speed;
	
	// A thresholf in x axis so that
	// the user needs to move to catch the life box
	public float CollisionThreshold = 0.7f;
	
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
		else Object.Destroy(life);
	}
	
	// Collision detection
	void OnTriggerEnter(Collider other)
	{
		if (transform.position.z < 5.0) // BUG FIX
		{
			if (!hasCollidedOnce) 
			{				
				hasCollidedOnce = true;
				Instantiate(lifeEffect, transform.position, Quaternion.identity);
				lifeSound.audio.Play();
				
				GameObject.Find("LifesText").SendMessage("AddLife");
			}
			
			Object.Destroy(life);
		}
	}
}
