using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraProche : MonoBehaviour
{
    public GameObject megaman;

    // Limites 
    public float limiteGauche;
    public float limiteDroite;
    public float limiteHaut;
    public float limiteBas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionCamera = transform.position;

        // Suivi horizontal
        if (megaman.transform.position.x >= limiteGauche && megaman.transform.position.x <= limiteDroite)
        {
            positionCamera.x = megaman.transform.position.x;
        }

        // Suivi vertical
        if (megaman.transform.position.y >= limiteBas && megaman.transform.position.y <= limiteHaut)
        {
            positionCamera.y = megaman.transform.position.y;
        }

        transform.position = positionCamera;

    }
}
