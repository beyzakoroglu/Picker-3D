using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public void ExplodeInTime()
    {
        Invoke("Explode", 2f);
    }

    private void Explode()
    {
        Destroy(gameObject);
        Instantiate(Resources.Load("Particals"), transform.position, Quaternion.identity);
    }
}
