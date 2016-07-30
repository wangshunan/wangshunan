﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class FloatingPlayer2DController : MonoBehaviour {
    public float moveForce = 5, boostMultiplier = 2;
    Rigidbody2D myBody;
	//uvuidfvndfiunvdif
	void Start () 
    {
        myBody = this.GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () 
    {
        Vector2 moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * moveForce;
        bool isJumping = CrossPlatformInputManager.GetButton("Jump");
        Debug.Log(isJumping ? boostMultiplier :1);
        //myBody.AddForce(moveVec * (isJumping ? boostMultiplier : 1));
         GetComponent<Rigidbody2D>().velocity = moveVec;
        
	}
}
