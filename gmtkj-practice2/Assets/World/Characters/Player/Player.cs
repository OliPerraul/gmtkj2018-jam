﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, NSFSM.IContext {

    public InputControllerDefault inputs;
    public NSFSM.FSM fsm;
    public ThirdPersonCamera cam;
    public Animator anim;                      // Reference to the animator component.
    public Rigidbody playerRigidbody;          // Reference to the player's rigidbody.

    public float speed = 6f;            // The speed that the player will move at.
    public Vector3 movement;                   // The vector to store the direction of the player's movement.


    // Use this for initialization
    void Start () {
        movement = new Vector3();
        fsm.Launch(this);
	}
	
	// Update is called once per frame
	void Update () {
        fsm.Tick();
	}
}
