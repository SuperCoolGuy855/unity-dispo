using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	void SetText()
	{
		counttext.text = "Count: " + count.ToString ();
	}
	public float speed;
	public Text counttext;
	private int count = 0;

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		SetText ();
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
    }
}

