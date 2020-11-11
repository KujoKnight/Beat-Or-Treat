using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject endPoint;
    public Light redLight;
    public Light blueLight;
    public Light whiteLight;
    public Vector3 destination;
    private Vector3 velocity;
    public bool isCop;
    public float speed = 5f;

    void Start()
    {
        destination = endPoint.transform.position;
        velocity = Vector3.zero;
        if(isCop)
        {
            StartCoroutine(Lights());
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, destination) > 1f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, speed);
        }
        else
        {
            if(destination == endPoint.transform.position)
            {
                destination = startPoint.transform.position;
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                destination = endPoint.transform.position;
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
    }

    public IEnumerator Lights()
    {
        while(gameObject.activeSelf)
        {
            int index = Random.Range(0, 7);

            switch (index)
            {
                case 0:
                    redLight.enabled = false;
                    whiteLight.enabled = false;
                    blueLight.enabled = false;
                    break;
                case 1:
                    redLight.enabled = true;
                    whiteLight.enabled = false;
                    blueLight.enabled = false;
                    break;
                case 2:
                    redLight.enabled = false;
                    whiteLight.enabled = true;
                    blueLight.enabled = false;
                    break;
                case 3:
                    redLight.enabled = false;
                    whiteLight.enabled = false;
                    blueLight.enabled = true;
                    break;
                case 4:
                    redLight.enabled = true;
                    whiteLight.enabled = true;
                    blueLight.enabled = false;
                    break;
                case 5:
                    redLight.enabled = false;
                    whiteLight.enabled = true;
                    blueLight.enabled = true;
                    break;
                case 6:
                    redLight.enabled = true;
                    whiteLight.enabled = false;
                    blueLight.enabled = true;
                    break;
                case 7:
                    redLight.enabled = true;
                    whiteLight.enabled = true;
                    blueLight.enabled = true;
                    break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
