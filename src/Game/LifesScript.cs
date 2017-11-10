using UnityEngine;
using System.Collections;

public class LifesScript : MonoBehaviour 
{	
	// A reference to the lifes text
	public GUIText lifesText;
	
	// A reference to the game over text
	public GUIText gameOverText;
	
	// The number of lifes
	public int numOfLifes = 3;
	
	// Use this for initialization
	void Start ()
	{
		gameOverText.text = "";
	}
	
	// Update is called once per frame
	void Update () 
	{
		lifesText.text = "Vidas = " + numOfLifes;
		
		LevelScript levelScript = GameObject.Find("LevelText").GetComponent<LevelScript>();
		
		// game over
		if (numOfLifes == 0 || levelScript.CurrentLevel == 10) 
		{
			GameObject.Find("GameController").SendMessage("StopCreatingObjects");
			
			gameOverText.text = "FIM DE JOGO";
		}
	}
	
	public void UpdateLifes()
	{
		numOfLifes--;
		
		Debug.Log("Number of lifes = " + numOfLifes);
	}
	
	public void AddLife()
	{
		numOfLifes++;
			
		Debug.Log("Gained a life");	
	}
}
