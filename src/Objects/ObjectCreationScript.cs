using UnityEngine;
using System.Collections;

public class ObjectCreationScript : MonoBehaviour {
	
	#region Fields
	
	// A reference to the coin prefab
	public GameObject coinPrefab;
	
	// A reference to the bomb prefab
	public GameObject bombPrefab;
	
	// A reference to the life prefab
	public GameObject lifePrefab;
	
	// The minimum object generation time.
	// It will be enhanced according to the current level
	public float MIN_GENERATION_TIME = 1.5f;
	
	// The maximum object generation time
	// It will be reduced according to the current level
	public float MAX_GENERATION_TIME = 7.0f;
	
	// The elapsed time between update calls
	private float elapsedTime = 0.0f;
	
	// The time between objects generation
	private float generationTimer = 0.0f;
	
	// A flag to enable/disable objects creation
	private bool createObjects = false;
	
	#endregion
	
	// Use this for initialization
	void Start () 
	{
		// First generation
		GenerateNewTimer();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (createObjects)
		{
			elapsedTime += Time.deltaTime;
			
			if (elapsedTime >= generationTimer)
			{
				elapsedTime = 0.0f;
				
				// this var defines which object will be created
				// chances of creating bombs are 30%, coins are 60%, lifes are 10%
				int objectToCreate = Random.Range(0, 10);
				
				Debug.Log("Object to create number = " + objectToCreate);
				
				if (objectToCreate < 3) CreateBomb();
				else if (objectToCreate < 9) CreateCoin();
				else CreateLife();
				
				// generate a new timer
				GenerateNewTimer();
			}
		}
	}
	
	// This method generates the time range for
	// the current level, the higher the level
	// the smaller the time range gets
	private void GenerateNewTimer()
	{
		LevelScript levelScript = GameObject.Find("LevelText").GetComponent<LevelScript>();
		
		float maxRange = 
			Mathf.Min(MAX_GENERATION_TIME, (MAX_GENERATION_TIME + 3.0f - (float) levelScript.CurrentLevel));
		
		// Can't have too low values
		if (maxRange < (MIN_GENERATION_TIME * 2)) maxRange = MIN_GENERATION_TIME * 2;
		
		generationTimer = Random.Range(
			Mathf.Max(MIN_GENERATION_TIME, (MIN_GENERATION_TIME + 3.0f - (float) levelScript.CurrentLevel)), 
			maxRange);
	}
	
	#region ObjectCreation
	
	// This method creates coin type objects
	private void CreateCoin()
	{
		int direction = Random.Range(0, 2);
		float x = Random.Range(5.0f, 30.0f);
		float y = Random.Range(0.5f, 2.0f);
		Vector3 pos;
		
		if (direction == 1) // positive x
		{
			pos = new Vector3(x, y, coinPrefab.transform.position.z);
		}
		else // negative x
		{
			pos = new Vector3(-x, y, coinPrefab.transform.position.z);
		}
			
		Instantiate(coinPrefab, pos, Quaternion.identity);		
	}
	
	// This method creates bomb type objects
	private void CreateBomb()
	{
		int direction = Random.Range(0, 2);
		float x = Random.Range(5.0f, 30.0f);
		Vector3 pos;
		
		if (direction == 1) // positive x
		{
			pos = new Vector3(x, bombPrefab.transform.position.y, bombPrefab.transform.position.z);
		}
		else // negative x
		{
			pos = new Vector3(-x, bombPrefab.transform.position.y, bombPrefab.transform.position.z);
		}
			
		Instantiate(bombPrefab, pos, Quaternion.identity);
	}
	
	// This methos creates life type objects
	private void CreateLife()
	{
		int direction = Random.Range(0, 2);
		float x = Random.Range(5.0f, 30.0f);
		float y = Random.Range(0.5f, 2.5f);
		Vector3 pos;
		
		if (direction == 1) // positive x
		{
			pos = new Vector3(x, y, lifePrefab.transform.position.z);
		}
		else // negative x
		{
			pos = new Vector3(-x, y, lifePrefab.transform.position.z);
		}
			
		Instantiate(lifePrefab, pos, Quaternion.identity);	
	}
	
	#endregion 
		
	// This method is called to start the object
	// creation loop
	public void StartCreatingObjects()
	{
		createObjects = true;
	}
	
	// This method is called when the game finishes.
	// i.e. the user loses all his lifes
	public void StopCreatingObjects()
	{
		createObjects = false;	
	}
}
