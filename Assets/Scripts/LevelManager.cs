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
        playerMovementController = PlayerMovementController.Instance;
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
        if (SceneManager.sceneCountInBuildSettings > currentLevel) // 433 - 1 = 432 + 1 = 433 BURAI kontrol etmedim
        {
            SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log("Game Completed");
            GameManager.Instance.ActivateWinUI();
        }
        
    }

    public void UnloadPreviousLevel()
    {
        SceneManager.UnloadSceneAsync(currentLevel - 1);
    }


    public void RestartLevel()
    {
        SceneManager.UnloadSceneAsync(currentLevel);
        SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Additive);
    }




    public bool TryWinLevel()
    {
        if (currentParkour == targetParkour)
        {
            WinLevel();
            return true;
        }
        return false;
    }

    private void WinLevel()
    {
        //playerMovementController.SetCanMove(false);
        // yeni levelı yükle cartcurt
        Debug.Log("You Win!");
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
        currentParkour++;
    }

    public void SetTargetParkour(int target) // 433 şimdilik kimse kullanmıcak
    {
        targetParkour = target;
    }


}
