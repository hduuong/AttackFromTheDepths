using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	
	public bool paused = false;
	public Texture tint;
	
	void Start ()
	{
		Time.timeScale = 1;
	}
	
	void OnGUI()
	{

		if(paused)
		{
			GUI.DrawTexture(new Rect(0, 0, Screen.width,Screen.height), tint);
			if(GUI.Button (new Rect(Screen.width/2-100,Screen.height/2-100, 200, 50),"Resume Game"))
			{
				paused = TogglePause();
				
			}
			if(GUI.Button (new Rect(Screen.width/2-100,Screen.height/2-25, 200, 50),"Options"))
			{
			
			}
			if(GUI.Button (new Rect(Screen.width/2-100,Screen.height/2+50, 200, 50),"Quit to Main Menu"))
			{
				Application.LoadLevel("MainMenu");
			}
		}
	}
	
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Escape) && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isDead)
			paused = TogglePause();

	}
	
	bool TogglePause()
	{
		if(Time.timeScale == 0)
		{
			
			Time.timeScale = 1;
			return false;
		}
		else
		{
			Time.timeScale = 0;
			return true;
		}
	}
}