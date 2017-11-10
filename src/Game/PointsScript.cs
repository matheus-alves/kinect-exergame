using UnityEngine;
using System.Collections;

public class PointsScript : MonoBehaviour 
{	
	// A reference to the points text
	public GUIText points;
	
	// The points counter
	public int CurrentPoints = 0;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		points.text = "Pontos = " + CurrentPoints;
	}
	
	public void UpdatePoints()
	{
		CurrentPoints++;
				
		Debug.Log("Points Counter = " + CurrentPoints);
	}
}
