using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleCameras : MonoBehaviour
{
    // Cameras
    public GameObject camera1;
    public GameObject camera2;
    // Start is called before the first frame update
    void Start()
    {
        camera1.SetActive(true);
        camera2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1)) changementCamera(camera1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) changementCamera(camera2);

    }
    
    void changementCamera(GameObject camera)
    {

        print("Fonction changementCamera appele");
        camera1.SetActive(false);
        camera2.SetActive(false);
        camera.SetActive(true);
    }
}
