using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControllor : MonoBehaviour {
	private Rigidbody rbody;
	public float speed;

	void Start () {
		rbody = GetComponent<Rigidbody> ();
		rbody.freezeRotation = true;
	}

	void FixedUpdate()
	{
		rbody.MovePosition (transform.position - transform.up * Time.deltaTime * speed);
	}
}
