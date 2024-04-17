using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class texteClignote : MonoBehaviour
{
    public float intervale;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("apparaitre", intervale);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void apparaitre()
    {
        gameObject.SetActive(true);
        Invoke("disparaitre", intervale);
    }

    void disparaitre()
    {
        gameObject.SetActive(false);
        Invoke("apparaitre", intervale);
    }
}
