using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointHandler : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Child"))
        {
            Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);
            other.gameObject.GetComponent<ChildController>().DamageEvent();
        }
    }
}
