using UnityEngine;
using System.Collections;


public class DoorScript : MonoBehaviour {

	public GameController controller;
	private Animator anim;
	

	void OnTriggerEnter2D (Collider2D hitinfo)
	{
		Debug.Log ("doors are opened");
		anim.SetBool("levelCompleted", true);
		controller.levelCompleted();
	}


	void Start () {
		anim = GetComponent<Animator>();
		anim.SetBool("levelCompleted", false);
	}


	void Update () {
	
	}
}
