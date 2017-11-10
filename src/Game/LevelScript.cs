using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour 
{
	// A reference to the level text
	public GUIText levelText;
	
	// The current level
	public int CurrentLevel = 1;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Updates the current level based on the number of points of the user
		// Each three points raises the level by one
		PointsScript pointsScript = GameObject.Find("PointsText").GetComponent<PointsScript>();
		
		CurrentLevel = Mathf.FloorToInt(pointsScript.CurrentPoints / 3);
		
		levelText.text = "Nivel = " + CurrentLevel;
	}
	
	public void UpdateLevel()
	{
		CurrentLevel++;
		
		Debug.Log("Current Level = " + CurrentLevel);
	}
}
