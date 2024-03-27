using UnityEngine;

public class Box : MonoBehaviour
{
   
    private int scoreCount = 0;
    //private int scoreToWin = 3;

    public void incrementScoreCount()
    {
        scoreCount++;
    }

    public int getScoreCount()
    {
        return scoreCount;
    }


}
