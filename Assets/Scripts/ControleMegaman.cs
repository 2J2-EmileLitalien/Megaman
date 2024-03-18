using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleMegaman : MonoBehaviour
{

    // Variables de mouvement
    public float vitesseX;
    public float vitesseY;
    private Vector2 mouvement;

    // Si Megaman est en vie 
    private bool enVie = true;

    // Variables de son
    public AudioClip sonMort;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enVie) {

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

            if (Input.GetKeyDown(KeyCode.W) && (Physics2D.OverlapCircle(transform.position, 0.25f)))
            {   
                // Print pour voir si deboggage necessaire.
                // Null = Saute sur rien, donc dans le if si ca retourne autre que rien -> Tu peux sauter.
                // S'assurer qu'il ne touche pas le collider de megaman! Donc mettre megaman en Layer Ignore Raycast
                print(Physics2D.OverlapCircle(transform.position, 0.25f)); // transform.position = celle de megaman car dans son script
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.OverlapCircle(transform.position, 0.25f))
        {
            GetComponent<Animator>().SetBool("Saut", false);
        }
        
        // Declencheur de mort = Ennemis. Repetitif si on les nommes tous avec .name donc on a ajouter un tag. Ici Niveau1 car un Ennemi du 2eme Niveau pourrait donner une mort differente
        if (collision.gameObject.tag == "EnnemiNiveau1")
        {
            // Lancer animation de mort && le son de mort 
            GetComponent<Animator>().SetBool("Mort", true);

            GetComponent<AudioSource>().PlayOneShot(sonMort);

            // Desactiver mouvements
            enVie = false;

            // Quand Megaman se fait toucher, ajouter le fait qu'il se fasse pousser (Comme dans Sonic fait en classe)

            // Si ennemi a droite de Megaman
            if (transform.position.x < collision.gameObject.transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 10);

                // Sinon (Il est donc a gauche de Megaman)
            } else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(10, 10);
            }

            // Reset la scene
            Invoke("resetScene", 2f);
            
        }
    }

    void resetScene()
    {
        SceneManager.LoadScene("Megaman");
    }
}



