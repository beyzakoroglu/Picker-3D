using UnityEngine;

public class Slower : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tags.ELEMENT_TAG))
         {
            //Debug.Log("Slower");
            other.gameObject.GetComponent<Element>().Fall();
        }
    }
}
