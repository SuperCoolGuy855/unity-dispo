using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	void SetText()
	{
		counttext.text = "Count: " + count.ToString ();
		if (count >= 10) 
		{
			wintext.text = "You Win";
		}
	}

	public float speed;
	public Text counttext;
	public Text wintext;
	private int count = 0;
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		SetText ();
		wintext.text = "";
	}

    void FixedUpdate()
    {
		float hori = Input.GetAxis ("Horizontal");
		float verti = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (hori, 0, verti);
		rb.AddForce (movement * speed);
    }

    void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Pickup"))
		{
			other.gameObject.SetActive(false);
		    count++;
		    SetText ();
		}
		if (other.gameObject.CompareTag ("Ground") && count >= 10)
		{
			other.gameObject.SetActive (false);
		}
    }
}

