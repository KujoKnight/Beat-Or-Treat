using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    public int scoreValue = 1;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform, -Vector3.up);
        transform.localEulerAngles = new Vector3(-transform.localEulerAngles.x, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            PlayerController.instance.score += scoreValue;
            Destroy(gameObject);
        }
    }
}
