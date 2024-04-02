using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }


    private Canvas canvas;


    [SerializeField] private GameObject mainMenu;
    private bool isMainMenuActive;
    public GameObject winUI;
    public GameObject loseUI;
    public GameObject congratsUI;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        InitializaGame();

        ActivateMainMenu(); 
        SceneManager.sceneLoaded += OnSceneLoaded;   // observer pattern 
    }
    

    private void InitVariables()
    {
        canvas = FindObjectOfType<Canvas>();
    }



    private void InitializaGame()
    {
        InitVariables();

        DeactivateLoseUI();
        DeactivateWinUI();
        DeactivateCongratsUI();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializaGame();
        LevelManager.Instance.InitVariables();
        Player.Instance.RestartPlayer();
    }

    void Update(){
    
        if(isMainMenuActive)
        { 
            if(Input.GetMouseButtonDown(0) && IsMouseOverCanvas(canvas)){
                DeactivateMainMenu();
            }
        }
    }

    public void ActivateMainMenu(){
        Time.timeScale = 0;
        isMainMenuActive = true;
        mainMenu.SetActive(true);
    }

    public void DeactivateMainMenu(){
        Time.timeScale = 1;
        isMainMenuActive = false;
        mainMenu.SetActive(false);
    }

    public void ActivateWinUI()
    {
        AudioManager.Instance.PlaySFX(Constants.Paths.WIN_SOUND_PATH);
        winUI.SetActive(true);
    }

    public void DeactivateWinUI()
    {
        winUI.SetActive(false);
    }

    public void ActivateLoseUI()
    {
        loseUI.SetActive(true);
    }

    public void DeactivateLoseUI()
    {
        loseUI.SetActive(false);
    }

    public void ActivateCongratsUI()
    {
        AudioManager.Instance.PlaySFX(Constants.Paths.WIN_SOUND_PATH);
        congratsUI.SetActive(true);
    }

    public void DeactivateCongratsUI()
    {
        congratsUI.SetActive(false);
    }

    public void RestartLevel()
    {
        Debug.Log("Restarting Level");

        LevelManager.Instance.RestartLevel();
        Player.Instance.RestartPlayer();
        
        InitializaGame();
    }

    public void LevelWon()
    {
        Debug.Log("Level Won");
        LevelManager.Instance.CurrentLevel++;
        ActivateWinUI();
    }

    public void ContinueToGame()
    {
        Player.Instance.PlayerMovementController.SetCanMove(true);
        InitializaGame();
        DeactivateMainMenu();
    }

    private bool IsMouseOverCanvas(Canvas canvas)
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        return true;
    }
}
