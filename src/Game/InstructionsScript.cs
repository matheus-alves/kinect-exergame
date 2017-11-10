using UnityEngine;
using System.Collections;

public class InstructionsScript : MonoBehaviour {
	
	// A reference to the instructions texture
	public GUITexture InstructionsTexture;
	
	// How many time the instructions will be displayed
	public float InstructionsTime = 10.0f;
	
	// The elapsed time between update calls
	private float elapsedTime = 0.0f;
	
	// A flag that indicates if the message to start creating objects was sent
	private bool messageSent = false;
	
	// Use this for initialization
	void Start () 
	{
		transform.Translate(1.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		elapsedTime += Time.deltaTime;
			
		if (elapsedTime >= InstructionsTime && !messageSent)
		{
			GameObject.Find("GameController").SendMessage("StartCreatingObjects");
			
			GameObject.Destroy(InstructionsTexture);
			
			messageSent = true;
		}
	}
}
