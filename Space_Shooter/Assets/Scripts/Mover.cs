using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
	private Rigidbody rbody;
	public float speed;

	void Start () {
		rbody = GetComponent<Rigidbody> ();
		rbody.velocity = transform.forward * speed;
	}
}
