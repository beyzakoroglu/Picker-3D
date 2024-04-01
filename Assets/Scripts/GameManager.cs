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
        Debug.Log("GameManager Started");
        InitializaGame();

        SceneManager.sceneLoaded += OnSceneLoaded;   // observer pattern abone-izleyici
                                            //scene yüklendiğinde OnSceneLoaded fonksiyonunu çalıştır
                                            //bulması daha kolay

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
        ActivateMainMenu();
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);
        InitializaGame();
        DeactivateMainMenu();
    }

    void Update(){
    
        if(isMainMenuActive)
        { 
            if(Input.GetMouseButtonDown(0) && IsMouseOverCanvas(canvas)){ // 433 sağdaki boş
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
        // Canvas'ın RectTransform'ını al
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        //return RectTransformUtility.RectangleContainsScreenPoint(canvasRect, Input.mousePosition, null);
        //433 nedense çalışmıyr şimdilik böle kalsın
        return true;
    }
}
