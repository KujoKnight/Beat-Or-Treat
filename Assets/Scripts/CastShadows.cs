using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastShadows : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Renderer>().receiveShadows = true;
        GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
}
