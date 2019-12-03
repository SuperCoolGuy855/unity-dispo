using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed = 5;
    // private Transform transform;
    private Animator animator;
    private SpriteRenderer sprite_renderer;
    private bool facing_right = true;
    private bool previous_right = true;
    // Start is called before the first frame update
    void Start()
    {
        // transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set horizontal position
        float hor = Input.GetAxisRaw("Horizontal") * speed;
        if (hor < 0) {
            facing_right = false;
            animator.SetBool("isRunning", true);
        } else if (hor > 0) {
            facing_right = true;
            animator.SetBool("isRunning", true);
        }
        if (facing_right != previous_right) {
            previous_right = facing_right;
            transform.Rotate(0, 180, 0);
        }
        // Set vertical position
        float ver = Input.GetAxisRaw("Vertical") * speed;
        if (ver != 0) {
            animator.SetBool("isRunning", true);
        }
        
        // Apply position
        transform.position += new Vector3(hor * Time.deltaTime, ver * Time.deltaTime, 0);
        
        animator.SetBool("isRunning", false);
    }
}
