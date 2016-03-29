using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float turnSpeed;

	private Vector3 moveDirection;
	private Animator anim;
	private bool isRunning;


	void Start () {
		moveDirection = Vector3.right;
		anim = GetComponent<Animator>();
		anim.SetBool("isRunning", false);
		isRunning = false;
		Debug.Log("Start");
	}


	void Update () {

		Vector3 currentPosition = transform.position;

		if (Input.GetButton ("Fire1")) {
						
			if (isRunning != true)
			{
				isRunning = true;
				anim.SetBool ("isRunning", true);
			}
			    
			Vector3 moveToward = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0; 
			moveDirection.Normalize ();

			Vector3 target = moveDirection * moveSpeed + currentPosition;
			transform.position = Vector3.Lerp (currentPosition, target, Time.deltaTime);
		} 
		else if (isRunning != false)
		{
			isRunning = false;
			anim.SetBool ("isRunning", false);
		}

		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = 
			Quaternion.Slerp( transform.rotation, 
			                 Quaternion.Euler( 0, 0, targetAngle ), 
			                 turnSpeed * Time.deltaTime );
	}


}
