using TMPro;
using UnityEngine;

public class NominatorScript : MonoBehaviour
{
    public float updateSpeed = 5f; // speed of the score update (if increases speed will also increases)
    private int displayedScore = 0; 
    public int maxScore = 3; 
    private int targetScore = 0; 
    private TMP_Text scoreText; 
    public Box box;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>(); // veya GetComponent<Text>(), eğer TextMeshPro kullanmıyorsanız.
        scoreText.text = $"{displayedScore} / {maxScore}"; // Skor metnini başlangıç değerine ayarlar.
    }

    void Update()
    {
        // makes target score equal to the score count of the box
        targetScore = box.getScoreCount();

        // slowly updates the displayed score to the target score
        int interpolatedScore = IntLerp(displayedScore, targetScore, updateSpeed);

        // updates the score text
        scoreText.text = $"{interpolatedScore} / {maxScore}";
    }

    int IntLerp(int from, int to, float t)
    {
        float intResult = Mathf.Lerp(from, to, t);
        return Mathf.RoundToInt(intResult);
    }
}