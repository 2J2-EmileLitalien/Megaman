using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionFinGagne : MonoBehaviour
{
    // Variable de la source du son (Dans le meme gameObject que le script)
    AudioSource musiqueVictoireSource;

    // Variable de l'image du trophee
    public GameObject tropheeImage;
    // Start is called before the first frame update
    void Start()
    {
        if (ControleMegaman.score < ControleMegaman.highscore)
        {
            tropheeImage.SetActive(false);
        }
        musiqueVictoireSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // CHANGER DE SCENE AVEC BARRE D'ESPACE
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            Invoke("changeScene", 0f);
        };

        // CHANGER DE SCENE APRES QUE LA MUSIQUE SOIS FINIE
        if (!musiqueVictoireSource.isPlaying)
        {
            Invoke("changeScene", 0f);
        };        
    }

    void changeScene()
    {
        SceneManager.LoadScene("introduction");
    }
}

