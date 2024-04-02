using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get => instance; }


    private int currentLevel;
    public int CurrentLevel { get => currentLevel; set { currentLevel = value; UpdateLevelText(); SaveToPrefs(value);}}


    private int elementGoal;
    public int ElementGoal { get => elementGoal; private set => elementGoal = value; }
    private int elementCount;
    public int ElementCount { get => elementCount; private set => elementCount = value; }
    

    private Slider elementSlider;
    private TextMeshProUGUI levelText;
    


    private PlayerMovementController playerMovementController;
  

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } 
        instance = this;
        DontDestroyOnLoad(gameObject);        
        CurrentLevel = LoadFromPrefs();
        
    }

    void Start() {
        InitVariables();
        Debug.Log("LevelMaAWDADAANJFS DJS FBJLOSB FDJCASKMJnager Started");
        RestartLevel();
    }


    public void InitVariables()
    {
        playerMovementController = Player.Instance.PlayerMovementController;
        elementSlider = GameObject.FindGameObjectWithTag("ElementSlider").GetComponent<Slider>();
        levelText = GameObject.FindGameObjectWithTag("LevelText").GetComponent<TextMeshProUGUI>();
        elementCount = 0;

        CurrentLevel = LoadFromPrefs();
        UpdateLevelText();
    }





    public void LoadNextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings >= CurrentLevel) 
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
        // Unload all scenes except the current one
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
                SceneManager.UnloadSceneAsync(scene);
            
        }

        // Load the specified scene
        SceneManager.LoadSceneAsync(GetSceneIndex(CurrentLevel));
    }

    private void UpdateLevelText()
    {
        if (levelText == null)
            levelText = GameObject.FindGameObjectWithTag("LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = CurrentLevel.ToString();
    }


    public void LoseLevel()
    {
        //playerMovementController.SetCanMove(false);
        GameManager.Instance.ActivateLoseUI();
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

    private void SaveToPrefs(int level)
    {
        PlayerPrefs.SetInt("CurrentLevel", level);
    }

    private int LoadFromPrefs()
    {
        return PlayerPrefs.GetInt("CurrentLevel", Constants.LEVEL_START_INDEX);
    }
}
