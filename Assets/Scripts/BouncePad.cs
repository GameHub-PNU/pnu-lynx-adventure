using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private Animator anim;

    public float bounceForce = 22f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.rigidBody.velocity = new Vector2(PlayerController.instance.rigidBody.velocity.x, bounceForce);
            anim.SetTrigger("Bounce");
        }
    }

}
