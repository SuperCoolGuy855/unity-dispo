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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Cactus")
        {
            died = true;
        }
        else
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D (Collision2D col)
	{
        onGround = false;
    }

    // Use this for initialization
    void Start () {
		player = GetComponent<Rigidbody2D> ();
        playerAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if ((Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.UpArrow)) && onGround) {
			player.AddForce (Vector2.up * force, ForceMode2D.Impulse);
		}
        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerAnimator.SetBool("duck", true);
        }
        else
        {
            playerAnimator.SetBool("duck", false);
        }
	}
}
