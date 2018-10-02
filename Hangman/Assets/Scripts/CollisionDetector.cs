using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Vector2 position = new Vector2(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 0f));
        transform.position = position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		Collider2D test = Physics2D.OverlapCircle(transform.position, 0.3f);
		if (test != null && test.gameObject.name != name)
		{
            Vector2 position = new Vector2(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 0f));
            transform.position = position;
		}
    }
}
