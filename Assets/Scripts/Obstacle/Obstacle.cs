using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
       
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shild")
        {
            anim.SetBool("Explosion", true);
            Destroy(this.gameObject, 0.2f);
        }

        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("Explosion", true);
            Destroy(this.gameObject, 0.2f);
        }

        if (collision.gameObject.tag == "GameController")
        {
            anim.SetBool("Explosion", true);
            Destroy(this.gameObject, 0.2f);
        }
    }

 
}
