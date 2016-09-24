﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	Rigidbody2D playerBody = null;

	// Used to keep track of if the player is standing on the ground.
	GameObject groundedOn = null;
	bool isGrounded = false;

	// Neccesary for jumping.
	Vector2 velocity;
	public float speed = 10.0f;
	public Vector2 jumpSpeed = new Vector2 (0.0f, 19.81f);
	public Vector2 gravity = new Vector2(0.0f,-9.81f);

	// Use this for initialization
	void Start () {
		playerBody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		velocity = new Vector2(Input.GetAxis("Horizontal") * speed, velocity.y);
		if (isGrounded)
		{
			if (Input.GetButtonDown("Jump"))
			{
				gravity += jumpSpeed;
			}
		}
	}

	void FixedUpdate(){
		if (!isGrounded)
		{
			velocity += gravity * Time.deltaTime;
			if (gravity.y > -9.81f) {
				//gravity -= gravity * Time.deltaTime;
			}
		}
		playerBody.MovePosition (playerBody.position + velocity * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D theCollision){
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