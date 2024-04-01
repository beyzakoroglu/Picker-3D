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
        ActivateMainMenu();


    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);
        InitializaGame();
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

    public void RestartLevel()
    {
        Debug.Log("Restarting Level");
        // first you should restart the level, then the player
        // because previous level's elements should be deleted
        // so player could find the new elements start trigger position
        // 433 emin deilim belki gerek bile yoktur bu dedigime

        LevelManager.Instance.RestartLevel();
        Player.Instance.RestartPlayer();
        
        InitializaGame();
    }


    private void DeactiveAllUI()
    {
        DeactivateMainMenu();
        DeactivateWinUI();
        DeactivateLoseUI();
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
