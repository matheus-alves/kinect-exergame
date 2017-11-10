using UnityEngine;
using System.Collections;

public class BombBehaviourScript : MonoBehaviour
{
	#region Fields
	
	// A reference to the bomb
	public GameObject bomb;
	
	// A reference to the user
	public GameObject User;
	
	// A reference to the effect of the explosion
	public GameObject explosionEffect;
	
	// A reference to the sound of the explosion
	public GameObject explosionSound;

	// The minimum speed of the bomb
	public float MIN_SPEED = 0.15f;
	
	// The current speed of the object
	private float speed;
	
	// A thresholf in x axis for the user 
	// to be able to avoid the bomb
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
		else Object.Destroy(bomb);
	}
	
	// Collision detection
	void OnTriggerEnter(Collider other)
	{
		if (transform.position.z < 5.0) // BUG FIX
		{
			if (!hasCollidedOnce) 
			{				
				hasCollidedOnce = true;
				Instantiate(explosionEffect, transform.position, Quaternion.identity);
				explosionSound.audio.Play();
				
				GameObject.Find("LifesText").SendMessage("UpdateLifes");
			}
			
			Object.Destroy(bomb);
		}
	}
}
