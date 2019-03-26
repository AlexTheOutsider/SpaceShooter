using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayState : FSM<GameManager>.State
{
    public override void OnEnter()
    {
        EventManagerNew.Instance.Register<GameOverEvent>(GameOver);
    }

    public override void OnExit()
    {
        EventManagerNew.Instance.Unregister<GameOverEvent>(GameOver);
    }
    
    private void GameOver(GameOverEvent myEvent)
    {
        TransitionTo<GameOverState>();
        Time.timeScale = 0;
        GameObject.Destroy((myEvent).objToDestroy);
        Context.gameOverText.SetActive(true);
    }
}

public class GameOverState : FSM<GameManager>.State
{
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
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
