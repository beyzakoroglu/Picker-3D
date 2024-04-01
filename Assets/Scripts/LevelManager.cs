using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get => instance; }


    private int currentLevel;
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    private int currentParkour; // start from 1
    public int CurrentParkour { get => currentParkour;}
    private int targetParkour; // min value is 1 ----- it is always 3 for now




    private PlayerMovementController playerMovementController;
    private GameObject player;
  

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } 
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        playerMovementController = Player.Instance.PlayerMovementController;
        InitVariables();
    }

    private void InitVariables()
    {
        currentLevel = Constants.LEVEL_START_INDEX;
        currentParkour = Constants.PARKOUR_START_INDEX;
        targetParkour = Constants.PARKOUR_TARGET_INDEX;
    }

    public void LoadNextLevel()
    {
        currentLevel++;
        if (SceneManager.sceneCountInBuildSettings >= currentLevel) // 433 - 1 = 432 + 1 = 433 BURAI kontrol etmedim
        {
            SceneManager.LoadSceneAsync(GetSceneIndex(currentLevel), LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log("Game Completed");
            GameManager.Instance.ActivateWinUI();
        }

        
        
    }

    public void UnloadPreviousLevel()
    {
        if (currentLevel > Constants.LEVEL_START_INDEX)
            SceneManager.UnloadSceneAsync(GetSceneIndex(currentLevel - 1));
    }


    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(GetSceneIndex(currentLevel));
    }




    public bool TryWinLevel()
    {
        Debug.Log("Trying to win level");
        Debug.Log("Current Parkour: " + currentParkour + " Target Parkour: " + targetParkour);  
        if (currentParkour == targetParkour)
        {
            WinLevel();
            ResetCurrentParkour();
            return true;
        }
        return false;
    }



    private void WinLevel()
    {
        Debug.Log("Loading Next Level");
        LoadNextLevel();
    }


    public void LoseLevel()
    {
        //playerMovementController.SetCanMove(false);
        GameManager.Instance.ActivateLoseUI();
    }

    public void IncrementCurrentParkour()
    {
        Debug.Log("Incrementing Parkour");
        currentParkour++;
    }

    public void SetTargetParkour(int target) // 433 şimdilik kimse kullanmıcak
    {
        targetParkour = target;
    }

    public LevelStartTrigger GetLevelStartTrigger()
    {
        // unless the moment of taptap, there will be only 1 level start trigger
        // this can cause problem in the future
        // so you shouldn't use this method in tap tap moment
        
        return GameObject.FindObjectOfType<LevelStartTrigger>();
    }

    private int GetSceneIndex(int levelIndex) => levelIndex - 1;

    public void ResetCurrentParkour()
    {
        currentParkour = Constants.PARKOUR_START_INDEX;
    }

}
