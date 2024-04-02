using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private static Player player;
    public static Player Instance { get => player; }


    private static PlayerInputController playerInputController;
    private static PlayerMovementController playerMovementController;
    public PlayerInputController PlayerInputController { get => playerInputController; private set => playerInputController = value; }
    public PlayerMovementController PlayerMovementController { get => playerMovementController; private set => playerMovementController = value; }


    private List<GameObject> objectsInsideMagnet = new List<GameObject>();



    void Awake()
    {
        if (player != null)
        {
            Destroy(gameObject);
        }
        
        player = this;
        PlayerInputController = GetComponent<PlayerInputController>();
        PlayerMovementController = GetComponent<PlayerMovementController>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = FindObjectOfType<Player>();
        PlayerInputController = player.GetComponent<PlayerInputController>();
        PlayerMovementController = player.GetComponent<PlayerMovementController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!objectsInsideMagnet.Contains(other.gameObject) && other.gameObject.CompareTag(Constants.Tags.ELEMENT_TAG))
        {
            objectsInsideMagnet.Add(other.gameObject);
            AudioManager.Instance.PlaySFX(Constants.Paths.COLLECT_SOUND_PATH, .15f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsInsideMagnet.Contains(other.gameObject))
        {
            objectsInsideMagnet.Remove(other.gameObject);
        }
    }

    public void ThrowObjects()
    {
        foreach (GameObject obj in objectsInsideMagnet)
        {
            obj.GetComponent<Element>().Throw();
        }
        objectsInsideMagnet.Clear();
    }

    public bool HasElements()
    {
        return objectsInsideMagnet.Count != 0;
    }

    public void RestartPlayer()
    {
        playerMovementController.Initialize();
        objectsInsideMagnet.Clear();
        Transform levelStart = GameObject.FindObjectOfType<LevelStartTrigger>().transform;
        transform.position = new Vector3(transform.position.x, transform.position.y, levelStart.position.z);
        PlayerMovementController.SetCanMove(true);
    }

    public void Stop()
    {
        PlayerMovementController.SetCanMove(false);
    }

}
