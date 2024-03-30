using TMPro;
using UnityEngine;

public class NominatorScript : MonoBehaviour
{
    public float updateSpeed = 5f; // speed of the score update (if increases speed will also increases)
    private int displayedScore = 0; 
    
    private int targetScore = 0; 
    private TMP_Text scoreText; 
    public Box box;
    private int denominator;

    void Start()
    {
        denominator = box.GetScoreToWin();   //every level has its own score to win
        scoreText = GetComponent<TMP_Text>(); 
        scoreText.text = $"{displayedScore} / {denominator}"; //fixes the score text its initial state
    }

    void Update()
    {
        // makes target score equal to the score count of the box
        targetScore = box.GetScoreCount();

        // slowly updates the displayed score to the target score
        int interpolatedScore = IntLerp(displayedScore, targetScore, updateSpeed);

        // updates the score text
        scoreText.text = $"{interpolatedScore} / {denominator}";
    }

    int IntLerp(int from, int to, float t)
    {
        float intResult = Mathf.Lerp(from, to, t);
        return Mathf.RoundToInt(intResult);
    }
}