using UnityEngine;

public class Slower : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            //Debug.Log("Slower");
            other.gameObject.GetComponent<Element>().Fall();
        }
    }
}
