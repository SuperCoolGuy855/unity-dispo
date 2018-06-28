using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	private Rigidbody rbody;
	private AudioSource au;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	void Start()
	{
		rbody = GetComponent<Rigidbody> ();
		au = GetComponent<AudioSource> ();
	}

	void Update()
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			au.Play ();
		}
	}

	void FixedUpdate()
	{
		float hor = Input.GetAxis ("Horizontal");
		float ver = Input.GetAxis ("Vertical");
		rbody.velocity = new Vector3 (hor, 0.0f, ver) * speed;
		rbody.position = new Vector3 (
			Mathf.Clamp (rbody.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (rbody.position.z, boundary.zMin, boundary.zMax)
		);
		rbody.rotation = Quaternion.Euler (0f, 0f, rbody.velocity.x * -tilt);
	}
}
