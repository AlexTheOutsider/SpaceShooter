using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverText;
    public GameObject bossHpSlider;
    
    [SerializeField] private FSM<GameManager> _fsm;
    
    private void Start()
    {
        Services.EventManagerNew = EventManagerNew.Instance;
        
        _fsm = new FSM<GameManager>(this);
        _fsm.TransitionTo<GamePlayState>();
    }

    private void Update()
    {
        _fsm.Update();
    }
}