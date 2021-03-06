﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	private Rigidbody2D player = null;
	public AudioSource beerSound = null;

	public int score = 0;

	// Used to keep track of if the player is standing on the ground.
	GameObject groundedOn = null;
	bool isGrounded = false;

	// Neccesary for jumping.
	Vector2 velocity;
	public float speed = 10.0f;
	public float jumpSpeed = 7.5f;
	public Vector2 gravity = new Vector2(0.0f,-9.81f);

	// Use this for initialization
	void Start () {

		player = this.GetComponent<Rigidbody2D> ();
		beerSound = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		velocity = new Vector2(Input.GetAxis("Horizontal") * speed, velocity.y);
		if (isGrounded)
		{
			if (Input.GetButtonDown("Jump"))
			{
				velocity.y = jumpSpeed;
			}
		}
	}

	void FixedUpdate(){
		if (!isGrounded)
		{
			velocity += gravity * Time.deltaTime;
		}
		Debug.Log (velocity);
		player.velocity = velocity;
	}

	void OnCollisionEnter2D(Collision2D theCollision){

		// if colliding with beer 
		// remove and add score
		if (theCollision.collider.CompareTag ("Beer")) {
			score++;
			beerSound.Play ();
			Destroy(theCollision.gameObject);
			Debug.Log ("Beer");
		}

		foreach(ContactPoint2D contact in theCollision.contacts)
		{
			if(contact.normal.y > 0.1)
			{
				isGrounded = true;
				groundedOn = theCollision.gameObject;
				break;
			}
		}
	}

	void OnCollisionExit2D(Collision2D theCollision){
		if(theCollision.gameObject == groundedOn)
		{
			groundedOn = null;
			isGrounded = false;
		}
	}

}
