using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationBalles : MonoBehaviour
{
    // Cette balle est celle dans les dossiers (La source PreFab) et non une instance deja dans le jeu car sinon, nous allons faire reference a un objet detruit (Car toutes les "Balles" contiennent le script qui les detruit eventuellement)
    public GameObject balleSource;

    // Start is called before the first frame update
    void Start()
    {
        // Pour faire apparaitre des choses a intervalles Problem = Va etre Random.Range mais random 1 fois. Ex: Va donner 6, mais chaque Repeating va etre a 6
        // InvokeRepeating("creationObjet", 1f, Random.Range(5f,10f));
        // Meilleur facon avec Invoke ici et dans la fonction
        Invoke("creationObjet", Random.Range(1f, 5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }







    // Code paired avec celui pour faire apparaitre des choses a intervalles du Start()
    void creationObjet()
    {
        // Ceci serait suffisant, mais on veut parler a l'objet (Le placer, etc.) 
        // Instantiate(balleSource);

        GameObject nouvelObjet = Instantiate(balleSource);
        nouvelObjet.transform.position = new Vector2(Random.Range(-20f, -13f), 2f);

        Invoke("creationObjet", Random.Range(1f, 5f));
    }
}
