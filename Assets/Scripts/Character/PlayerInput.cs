using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour {

    Movement _movement;
	// Use this for initialization
	void Awake () {
        _movement = GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {
        var horizontal = Input.GetAxis("Horizontal");
        _movement.Move(horizontal);
        var jump = Input.GetButton("Jump");
        _movement.Jump(jump);
	}
}
