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
    
    private void Awake()
    {
        Services.EventManagerNew = new EventManagerNew();
        //Services.EnemyManager = new EnemyManager();
        
        _fsm = new FSM<GameManager>(this);
        _fsm.TransitionTo<GamePlayState>();
    }

    private void Start()
    {
        //Services.EnemyManager.Start();
    }

    private void Update()
    {
        _fsm.Update();
        //Services.EnemyManager.Update();
    }

    private void OnDestroy()
    {
        //Services.EnemyManager.OnDestroy();
    }
}