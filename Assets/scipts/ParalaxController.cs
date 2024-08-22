using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxController : MonoBehaviour
{
    
    Transform cam; // Main Camera
     Vector3 camStartPos;
    float distance; // distance between the camera start position and its current position

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;

    float farthestBack;

    [Range(0.05f,0.1f)]
    public float parallaxSpeed;

    void Start()
    {

        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;

        }
        BackSpeedCa1cu1ate(backCount);

    }

    void BackSpeedCa1cu1ate(int backCount)
    {

        for (int i = 0; i < backCount; i++) // find the farhthest background'

        {
            if ((backgrounds[i].transform.position.z-cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }
        for (int i = 0; i < backCount; i++ ) // set the speed of background
        {
            backSpeed[i] = 1-(backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;
        transform.position = new Vector3 (cam.position.x, transform.position.y,0);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i]* parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector3 (distance,0) *speed);
        }

    }
}
