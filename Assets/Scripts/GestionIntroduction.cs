using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionIntroduction : MonoBehaviour
{

    public TextMeshProUGUI pointageTexte;
    // Start is called before the first frame update
    void Start()
    {
        pointageTexte.text = "Pointage à battre: " + ControleMegaman.highscore;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SceneManager.LoadScene("Megaman");
        }
    }
}
