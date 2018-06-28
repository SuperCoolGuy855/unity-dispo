using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Rigidbody rbody;
    private bool grounded = false;
	public float speedMultiplier;
    public float falling = 1.5f;
    public float jump;

	void Start () {
        //Get component section
		rbody = GetComponent<Rigidbody> ();

        //Other init stuff
		rbody.freezeRotation = true;
	}

	void FixedUpdate () {
        //Move player horizontally
		float hor = Input.GetAxis ("Horizontal");
		rbody.velocity = new Vector3 (hor * speedMultiplier, rbody.velocity.y, 0);

        //Stop player from going off screen horizontally
		rbody.position = new Vector3 (Mathf.Clamp (rbody.position.x, -3, 3), rbody.position.y, 0);

        //Jump
        if (grounded && Input.GetKeyDown (KeyCode.Space)) {
            rbody.velocity += jump * transform.up;
            grounded = false;
        }

        //Increase gravity when falling
        if (rbody.velocity.y < 0)
        {
            rbody.velocity += Vector3.up * Physics.gravity.y * Time.deltaTime * falling;
        }
	}

	void OnCollisionStay ()
	{
        grounded = true;
	}
}
