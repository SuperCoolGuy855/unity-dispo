using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	private Rigidbody2D rb2d;
	public float speed;
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}
	void FixedUpdate()
	{
		float hor = Input.GetAxis ("Horizontal");
		float ver = Input.GetAxis ("Vertical");
		Vector2 move = new Vector2 (hor, ver);
		rb2d.AddForce (move * speed);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive (false);
		}
	}
}
