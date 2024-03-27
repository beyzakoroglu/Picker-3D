using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private float horizontalValue;
    public float HorizontalValue { get { return horizontalValue; } }

    void Update()
    {
        HorizontalMovementInput() ;
    }

    private void HorizontalMovementInput() {
        if(Input.GetMouseButton(0))
        {
            // Fare hareketini listeye ekle
            float mouseX = Input.GetAxis("Mouse X");
            
            if(Mathf.Abs(mouseX) < 0.01f){
                horizontalValue *= 0.9f;
            }
            else{
                mouseX = Mathf.Clamp(mouseX, -0.5f, 0.5f);
                horizontalValue = mouseX;
            }
        } else {
            horizontalValue = 0f;
        }
    }

}
