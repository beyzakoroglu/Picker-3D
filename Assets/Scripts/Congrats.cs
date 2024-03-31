using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Congrats : MonoBehaviour
{
    private List<string> congratsTypes = new List<string> { "Awesome!", "Super!", "Great!" };
    private TextMeshProUGUI congratsText;

    void Awake()
    {
        congratsText = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log("Congrats started");
        Debug.Log(congratsText);
    }

    void OnEnable()
    {
        Debug.Log("Congrats enabled");
        congratsText.text = GetRandomCongrats();
    }

    private string GetRandomCongrats()
    {
        int index = Random.Range(0, congratsTypes.Count);
        return congratsTypes[index];
    }
}
