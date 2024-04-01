using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get => instance; }


    private int currentLevel = Constants.LEVEL_START_INDEX;
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }


    private int elementGoal;
    public int ElementGoal { get => elementGoal; private set => elementGoal = value; }
    private int elementCount;
    public int ElementCount { get => elementCount; private set => elementCount = value; }
    

    private Slider elementSlider;
    


    private PlayerMovementController playerMovementController;
  

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } 
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        InitVariables();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void InitVariables()
    {
        playerMovementController = Player.Instance.PlayerMovementController;
        elementSlider = GameObject.FindGameObjectWithTag("ElementSlider").GetComponent<Slider>();
        elementCount = 0;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitVariables();
    }


    public void LoadNextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings >= CurrentLevel) // 433 - 1 = 432 + 1 = 433 BURAI kontrol etmedim
            if(SceneManager.GetSceneByBuildIndex(GetSceneIndex(CurrentLevel + 1)).isLoaded == false)
                SceneManager.LoadSceneAsync(GetSceneIndex(CurrentLevel + 1), LoadSceneMode.Additive); 
        else
        {
            Debug.Log("Game Finished");
            Debug.Log("No More Levels");
        }

        
        
    }

    public void UnloadPreviousLevel()
    {
        if (CurrentLevel >= Constants.LEVEL_START_INDEX + 1)
        {
            int previousLevelIndex = GetSceneIndex(CurrentLevel - 1);
            if (SceneManager.GetSceneByBuildIndex(previousLevelIndex).isLoaded)
                SceneManager.UnloadSceneAsync(previousLevelIndex);
            else
                Debug.Log("Previous Level is not loaded");
        }
    }


    public void RestartLevel()
    {
        Debug.Log("Restarting Level");
        SceneManager.LoadSceneAsync(GetSceneIndex(CurrentLevel));
        LoadNextLevel();
        InitVariables(); // bunu sonradan ekledim emin deilim 433
    }






    public void LoseLevel()
    {
        //playerMovementController.SetCanMove(false);
        GameManager.Instance.ActivateLoseUI();
    }


    public LevelStartTrigger GetLevelStartTrigger()
    {
        // unless the moment of taptap, there will be only 1 level start trigger
        // this can cause problem in the future
        // so you shouldn't use this method in tap tap moment
        
        return GameObject.FindObjectOfType<LevelStartTrigger>();
    }

    private int GetSceneIndex(int levelIndex) => levelIndex - 1;


    public void IncrementElementCount()
    {
        ElementCount = ElementCount + 1;
        UpdateElementView();
    }

    public void DecreaseElementCount()
    {
        ElementCount = ElementCount - 1;
        UpdateElementView();
    }

    public void ResetElementCount()
    {
        ElementCount = 0;
        UpdateElementView();
    }

    private void UpdateElementView()
    {
        
        elementSlider.value = (float) elementCount / (float) elementGoal;
    }

    
    public void SetElementView(float value)
    {
        elementSlider.value = value;
    }
    
    public float GetElementRatio()
    {
        return (float) elementCount / (float) elementGoal;
    }

    public void SetElementGoal(int goal)
    {
        ElementGoal = goal;
        elementSlider = GameObject.FindGameObjectWithTag("ElementSlider").GetComponent<Slider>();
        UpdateElementView();
    }
}
