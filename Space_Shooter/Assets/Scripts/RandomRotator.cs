using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {
	public float tumble;
	private Rigidbody rbody;

	void Start()
	{
		rbody = GetComponent<Rigidbody> ();
		rbody.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
