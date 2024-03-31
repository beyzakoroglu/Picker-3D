using UnityEngine;

public class Element : MonoBehaviour
{
    Rigidbody rb;
    private bool isFlying = false;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        if(isFlying){
            Fly();
        }
    }

    public void ExplodeInTime()
    {
        Invoke("Explode", 1.25f);
    }

    private void Explode()
    {
        Destroy(gameObject);
        GameObject particle = Resources.Load("Particals") as GameObject;
        Instantiate(particle, transform.position, Quaternion.identity);

    }


    private void Fly(){
        rb.velocity = new Vector3(0, 10, 35);
    }   

    public void Throw(){
        isFlying = true;
    }

    public void Fall(){
        rb.velocity = new Vector3(0, 0, 15);
        isFlying = false;
    }


}
