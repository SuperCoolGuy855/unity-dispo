using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hor = Input.GetAxis ("Horizontal");
        float ver = Input.GetAxis ("Vertical");
        rb.AddForce(new Vector3(hor * speed, 0, ver * speed));
    }
}
