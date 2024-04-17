using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionBalle : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        Invoke("delete", 2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Animator>().SetBool("collision", true);
        Invoke("delete", 0.15f);

        if (collision.gameObject.tag == "EnnemiNiveau1")
        {
            print("Collision avec "+collision.gameObject.name);


            if (collision.gameObject.name != "Abeille")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                collision.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
            }

            collision.gameObject.GetComponent<Animator>().enabled = true;

            Destroy(collision.gameObject, 0.5f);
        }
    }


    void delete()
    {
        Destroy(gameObject);
    }
}
