using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPos;
    private GameObject cam;
    private float lenghtOfparallax;

    [SerializeField] private float parallaxSpeed;

    private void Start()
    {
        cam = GameObject.Find("CM vcam1");
        startPos = transform.position.x;
        lenghtOfparallax = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }


    private void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxSpeed));
        float distance = (cam.transform.position.x * parallaxSpeed);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        if (temp > startPos+ lenghtOfparallax)
        {
            startPos += lenghtOfparallax;
        }else if(temp < startPos - lenghtOfparallax)
        {
            startPos -= lenghtOfparallax;
        }
    }
}
