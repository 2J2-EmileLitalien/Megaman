using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleMegaman : MonoBehaviour
{
    public float vitesseX;
    public float vitesseY;

    private Vector2 mouvement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.D))
        {
            // Appuyer sur D
            GetComponent<SpriteRenderer>().flipX = false;
            mouvement.x = vitesseX;
        } else if (Input.GetKey(KeyCode.A)) 
        {
            // Appuyer sur A 
            GetComponent<SpriteRenderer>().flipX = true;
            mouvement.x = -1 * vitesseX; // Aurais pu faire - mais -1 * rend le negatif plus visible
        } else
        {
            // N'appui sur rien gauche-droite
            mouvement.x = GetComponent<Rigidbody2D>().velocity.x;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            // Appuyer sur W 
            GetComponent<Animator>().SetBool("Saut", true);
            mouvement.y = vitesseY;
        } else
        {
            // N'appui sur rien haut-bas
            mouvement.y = GetComponent<Rigidbody2D>().velocity.y;
        }

        // Animation marche ou non selon la vitesse du personnage
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 0.9)
        {
            GetComponent<Animator>().SetBool("Marche", true);
        } else
        {
            GetComponent<Animator>().SetBool("Marche", false);
        }
        

      

        GetComponent<Rigidbody2D>().velocity = mouvement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Animator>().SetBool("Saut", false);
    }

}
