using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class BossState : FSM<GameManager>.State
{
    //private GameObject bossHP;

    public override void OnEnter()
    {
        EventManagerNew.Instance.Register<BossHealthEvent>(UpdateHP);
        EventManagerNew.Instance.Register<ExitBossEvent>(ExitBoss);
        
        //bossHP = Context.canvas.transform.Find("BossHP").gameObject;
        Context.bossHpSlider.SetActive(true);
        Context.bossHpSlider.GetComponent<Slider>().value = 1f;
    }
    
    public override void OnExit()
    {
        EventManagerNew.Instance.Unregister<BossHealthEvent>(UpdateHP);
        EventManagerNew.Instance.Unregister<ExitBossEvent>(ExitBoss);
        
        Context.bossHpSlider.SetActive(false);
    }
    
    private void UpdateHP(BossHealthEvent myEvent)
    {
        Context.bossHpSlider.GetComponent<Slider>().GetComponent<Slider>().value = myEvent.hpRate;
    }
    
    private void ExitBoss(ExitBossEvent myEvent)
    {
        TransitionTo<GamePlayState>();
    }
}

public class BossHealthEvent : MyEvent
{
    public float hpRate;

    public BossHealthEvent(float rate)
    {
        hpRate = rate;
    }
}

public class EnterBossEvent : MyEvent {}

public class ExitBossEvent : MyEvent {}
