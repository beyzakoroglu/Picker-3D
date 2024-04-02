using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    void OnEnable()
    {
        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * 30f, ForceMode.Impulse);
            }
        }

        Invoke("DestroyEffect", 2f);
    }

    void DestroyEffect()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }


}
