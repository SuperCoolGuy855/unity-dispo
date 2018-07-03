using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float force;
    [HideInInspector]
    public bool died = false;
    private Animator playerAnimator;
	private Rigidbody2D player;
    private bool onGround = false;

    void OnCollisionEnter2D()
    {
        onGround = true;
    }
    
    void OnTriggerEnter2D()
    {
        died = true;
    }
    // Use this for initialization
    void Start () {
		player = GetComponent<Rigidbody2D> ();
        playerAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerAnimator.SetBool("duck", true);
        }
        else
        {
            playerAnimator.SetBool("duck", false);
        }
    }

	void FixedUpdate () {
		if ((Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.UpArrow)) && onGround) {
			player.AddForce (Vector2.up * force, ForceMode2D.Impulse);
		}
	}
}
