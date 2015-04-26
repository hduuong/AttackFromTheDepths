using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	public float restartLevelDelay = 0.2f;        //Delay time in seconds to restart level.

	Vector3 movement;
	Animator anim;
	Rigidbody2D playerRigidbody2D;
	BoxCollider2D playerBoxCollider2D;
	int floorMask;
	float camRayLength = 100f;
	string stairType;
	int level = 1;
	int floor; 
	
	void Awake()
	{
		floor = int.Parse(Application.loadedLevelName);
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody2D = GetComponent<Rigidbody2D> ();
		playerBoxCollider2D = GetComponent<BoxCollider2D> ();
	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		Move (h, v);
		Turning ();
		//Animating (h, v);
	}
	
	void Move(float h, float v)
	{
		movement.Set (h, v, 0f);
		movement = movement.normalized * speed * Time.deltaTime;
		
		playerRigidbody2D.MovePosition (transform.position + movement);
	}
	
	void Turning()
	{
		/*Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.z = 0f;
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
		}*/
		var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Quaternion rot = Quaternion.LookRotation (transform.position - mousePosition, Vector3.forward);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);
	}

	//OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
	private void OnTriggerEnter2D (Collider2D other)
	{
		//Check if the tag of the trigger collided with is Exit.
		if (other.tag == "Upstairs") 
		{
			stairType = "Upstairs";
			floor++;
			//Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
			Invoke ("Restart", restartLevelDelay);
			
			//Disable the player object since level is over.
			enabled = false;
		}
		else if (other.tag == "Downstairs")
		{
			stairType = "Downstairs";
			floor--;
			//Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
			Invoke ("Restart", restartLevelDelay);
			
			//Disable the player object since level is over.
			enabled = false;
		}
	}

	//Restart reloads the scene when called.
	private void Restart ()
	{
		//Load the floor above
		if (stairType == "Upstairs") 
		{
			string floorString = floor.ToString();
			print (floor);
			Application.LoadLevel (floorString);
		}
		//Load the floor below
		else if (stairType == "Downstairs") 
		{
			string floorString = floor.ToString();
			print (floor);
			Application.LoadLevel (floorString);
		}

	}
	
	//void Animating(float h, float v)
	//{
	//bool walking = h != 0f || v != 0f;
	//anim.SetBool ("IsWalking", walking);
	//}	
}
