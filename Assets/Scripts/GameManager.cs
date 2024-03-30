using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject failedScreen;
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
    }

    public void ActivateMainMenu(){
        mainMenu.SetActive(true);
    }

    public void DeactivateMainMenu(){
        mainMenu.SetActive(false);
    }

    public void ActivateFailedScreen(){
        failedScreen.SetActive(true);
    }

    public void DeactivateFailedScreen(){
        failedScreen.SetActive(false);
    }


}
