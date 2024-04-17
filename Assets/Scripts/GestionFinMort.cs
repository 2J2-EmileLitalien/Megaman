using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionFinMort : MonoBehaviour
{

    // Variables
    public TextMeshProUGUI textePointageFinal;
    public TextMeshProUGUI texteTempsRestant;
    private float tempsRestant = 10;
    // Start is called before the first frame update
    void Start()
    {
        texteTempsRestant.text = "Ça recommence dans: "+tempsRestant+" secondes";
        Invoke("updateTemps", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateTemps()
    {
        tempsRestant -= 1;
        // Change le texte au pluriel ou singulier 
        if (tempsRestant == 1)
        {
            texteTempsRestant.text = "Ça recommence dans: " + tempsRestant + " seconde";
        } else
        {
            texteTempsRestant.text = "Ça recommence dans: " + tempsRestant + " secondes";
        }

        // Si temps restant est plus que 1 (Donc pas la derniere seconde), on re-update. Sinon on relance le jeu 
        // Recommence dans 0 seconde n'apparaitra donc pas 
        if (tempsRestant > 1)
        {
            Invoke("updateTemps", 1f);
        } else
        {
            tempsRestant = 10;
            SceneManager.LoadScene("Megaman");
        }
    }

}
