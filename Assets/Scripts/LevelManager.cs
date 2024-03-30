using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int currentLevel;
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    private bool gameStarted = false;
    private PlayerMovementController playerMovementController;
    [SerializeField] private GameObject player;

    void Start() {
        playerMovementController = player.GetComponent<PlayerMovementController>();
    }
    void Update() {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            StartGame();
            gameStarted = true;
        }
    }
    public void LoadNextLevel()
    {
        int nextLevel = currentLevel + 3;
        if (nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Additive);
            Debug.Log("Loading level " + nextLevel);
        } else {
            Debug.Log("No more levels to load.");
        }

        currentLevel++;
    }

    public void UnloadPreviousLevel()
    {
        Debug.Log("Unloading level " + (currentLevel - 2));
        SceneManager.UnloadSceneAsync(currentLevel - 2);
    }


    /*public void OnStartButtonClicked()
    {
        Debug.Log("Start button clicked");
        GameManager.Instance.DeactivateMainMenu();
        currentLevel = 0;
        //SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
    }*/

    void StartGame()
    {
        Debug.Log("Start button clicked");                      
        GameManager.Instance.DeactivateMainMenu();
        playerMovementController.SetCanMove(true);    //player can move 
        currentLevel = 0;
        //SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

    }


}
