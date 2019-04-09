using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : FSM<GameManager>.State
{
    //private GameObject gameOverText;
    
    public override void OnEnter()
    {
        Services.EventManagerNew.Register<EnterBossEvent>(EnterBoss);
        Services.EventManagerNew.Register<GameOverEvent>(GameOver);
        //gameOverText = Context.canvas.transform.Find("GameOver").gameObject;
    }

    public override void OnExit()
    {
        Services.EventManagerNew.Unregister<EnterBossEvent>(EnterBoss);
        Services.EventManagerNew.Unregister<GameOverEvent>(GameOver);
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