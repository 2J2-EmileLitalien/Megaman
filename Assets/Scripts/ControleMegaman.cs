using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
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

    // Variables d'attaque
    private bool peutAttaquer = true;
    public float vitesseMaximale;

    // Variables pour les balles 
    public GameObject balleOriginale;
    public AudioClip sonBalle;



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

            // Si barre espace appuyer + il peut attaquer + saut est faux (Devient vrai car le ! inverse le resultat, ce qui permet la if grace au &&)
            if (Input.GetKeyDown(KeyCode.Space) && peutAttaquer && !(GetComponent<Animator>().GetBool("Saut")))
            {
                peutAttaquer = false;
                GetComponent<Animator>().SetBool("Saut", false);
                GetComponent<Animator>().SetBool("Attaque", true);
                Invoke("finAttaque", 0.5f);
            }

            // Animation marche ou non selon la vitesse du personnage
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 0.9)
            {
                GetComponent<Animator>().SetBool("Marche", true);
            } else
            {
                GetComponent<Animator>().SetBool("Marche", false);
            }
        

            if (peutAttaquer == false && (Mathf.Abs(vitesseX) <= vitesseMaximale) )
            {
                mouvement.x *= 2f;
            }


            // Voir si on peut tirer
            if (Input.GetKeyDown(KeyCode.Return) && !GetComponent<Animator>().GetBool("Saut") && !GetComponent<Animator>().GetBool("Attaque"))
            {
                print("On tire!");
                Invoke("LancerBombe", 0f);
            } else if (Input.GetKeyUp(KeyCode.Return))
            {
                GetComponent<Animator>().SetBool("tireBalle", false);
            }

            // Appliquer la velociter 
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
            if (peutAttaquer) // Si peut attaquer (Donc n'attaque pas actuellement)
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

                // Changement a la scene de mort 
                Invoke("finMort", 1.5f);
            
            } else // Megaman est en attaque -> Ennemi meurt
            {
                collision.gameObject.tag = "Untagged";
                collision.gameObject.GetComponent<Animator>().SetBool("Mort", true);
                Destroy(collision.gameObject, 0.7f);
            }
        }
    }

    void LancerBombe()
    {
        GetComponent<Animator>().SetBool("tireBalle", true);

        GameObject balleClone = Instantiate(balleOriginale);
        balleClone.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(sonBalle);

        if (GetComponent<SpriteRenderer>().flipX)
        {
            balleClone.GetComponent<Transform>().position = transform.position + new Vector3(-0.6f, 1, 0);
            balleClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-25, 0);
        } else
        {
            balleClone.GetComponent<Transform>().position = transform.position + new Vector3(0.6f, 1, 0);
            balleClone.GetComponent<Rigidbody2D>().velocity = new Vector2(25, 0);
        }
    }


    void finAttaque()
    {
        peutAttaquer = true;
        GetComponent<Animator>().SetBool("Attaque", false);
    }


    void finMort()
    {
        SceneManager.LoadScene("FinMort");
    }
}



