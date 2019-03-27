using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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