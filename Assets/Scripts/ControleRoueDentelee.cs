using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleRoueDentelee : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip sonExplosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Megaman")
        {
            // On active l'animation + on la rend non-collide
            GetComponent<Animator>().enabled = true;
            GetComponent<Collider2D>().enabled = false;

            // On joue le son de l'explosion
            GetComponent<AudioSource>().PlayOneShot(sonExplosion);
            
            // On arrete la roue 
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            GetComponent<Rigidbody2D>().gravityScale = 0;

            // Detruit la roue apres 1 seconde
            Invoke("detruireRoue", 1f);

        }
      
    }

    // Fonction de destruction de la roue
    void detruireRoue()
    {
        gameObject.SetActive(false);
    }
}
