using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : FSM<GameManager>.State
{
    //private GameObject gameOverText;
    
    public override void OnEnter()
    {
        EventManagerNew.Instance.Register<EnterBossEvent>(EnterBoss);
        EventManagerNew.Instance.Register<GameOverEvent>(GameOver);
        //gameOverText = Context.canvas.transform.Find("GameOver").gameObject;
    }

    public override void OnExit()
    {
        EventManagerNew.Instance.Unregister<EnterBossEvent>(EnterBoss);
        EventManagerNew.Instance.Unregister<GameOverEvent>(GameOver);
    }
    
    private void EnterBoss(EnterBossEvent myEvent)
    {
        TransitionTo<BossState>();
    }
    
    private void GameOver(GameOverEvent myEvent)
    {
        TransitionTo<GameOverState>();
        
        Time.timeScale = 0;
        GameObject.Destroy((myEvent).objToDestroy);
        Context.gameOverText.SetActive(true);
    }
}