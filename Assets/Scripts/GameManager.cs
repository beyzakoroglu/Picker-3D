using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }
    [SerializeField] private GameObject mainMenu;
    private Canvas canvas;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        //mainMenu = canvas.transform.Find("Manager/Canvas/MainMenu/StartButton").gameObject;
    }

    public void ActivateMainMenu(){
        mainMenu.SetActive(true);
    }

    public void DeactivateMainMenu(){
        mainMenu.SetActive(false);
    }
}
