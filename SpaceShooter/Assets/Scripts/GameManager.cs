using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverText;
    
    private bool isOver;
    
    private void Start()
    {
        Services.EventManagerNew = EventManagerNew.Instance;
    }

    private void Update()
    {
        if (isOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }
        }
    }

    private void OnEnable()
    {
        EventManagerNew.Instance.Register<GameOverEvent>(GameOver);
    }

    private void OnDisable()
    {
        EventManagerNew.Instance.Unregister<GameOverEvent>(GameOver);
    }

    // this is not type safe because the parameter is still the base event class
    private void GameOver(GameOverEvent myEvent)
    {
        Time.timeScale = 0;
        Destroy(((GameOverEvent)myEvent).objToDestroy);
        gameOverText.SetActive(true);
        isOver = true;
    }
}

public class GameOverEvent : MyEvent
{
    public GameObject objToDestroy;

    public GameOverEvent(GameObject obj)
    {
        objToDestroy = obj;
    }
}