using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyKilledEvent : MyEvent
{
    public GameObject enemyToDestroy;
    public int score;

    public EnemyKilledEvent(GameObject obj,int score)
    {
        enemyToDestroy = obj;
        this.score = score;
    }
}

public class EnemyManager : MonoBehaviour
{
    public int enemyNumber = 3;
    public float spawnInterval = 1f;
    public float waveInterval = 3f;
    public int waveBeforeBoss = 1;

    // the count of enemies that is going to spawn in this wave
    [SerializeField]private int enemiesWaitingCounts;
    //[SerializeField] private int enemyLeft;
    [SerializeField] private bool waveCleared = true;
    [SerializeField] private int waveIndex;

    private Transform[] spawnPoints;
    private Vector3 newSpawnPosition;
    private List<GameObject> enemies;
    private List<GameObject> enemiesToDestroy;
    private Text scoreText;
    private int score;

    private void OnEnable()
    {
        //EventManager.Instance.StartListening("EnemyKilled", EnemyKilled);
        Services.EventManagerNew.Register<EnemyKilledEvent>(EnemyKilled);
    }

    private void OnDisable()
    {
        //EventManager.Instance.StopListening("EnemyKilled", EnemyKilled);
        Services.EventManagerNew.Unregister<EnemyKilledEvent>(EnemyKilled);
    }

    public void Start()
    {
        spawnPoints = GameObject.Find("Spawn Points").transform.GetComponentsInChildren<Transform>();
        enemies = new List<GameObject>();
        enemiesToDestroy = new List<GameObject>();
        scoreText = GameObject.Find("Canvas").transform.Find("Score").GetComponent<Text>();
        scoreText.text = "0";
        
        //Services.EventManagerNew.Register<EnemyKilledEvent>(EnemyKilled);
    }

    public void Update()
    {
        foreach (GameObject dead in enemiesToDestroy)
        {
            enemies.Remove(dead);
            Destroy(dead);
        }

        enemiesToDestroy.Clear();

        if (!waveCleared) return;

        if (waveIndex < waveBeforeBoss)
        {
            StartCoroutine(SpawnEnemies());
        }
        else
        {
            StartCoroutine(SpawnBoss());
        }
    }

    IEnumerator SpawnEnemies()
    {     
        waveCleared = false;
        enemies.Clear();
        waveIndex++;
        enemiesWaitingCounts = enemyNumber;

        //enemyLeft = enemyNumber;
        yield return new WaitForSeconds(waveInterval);
        
        for (int i = 0; i < enemyNumber; i++)
        {
            GetRandomSpawnPosition();
            GameObject newEnemy = Instantiate(Resources.Load("Prefabs/Enemy"), newSpawnPosition,
                ((GameObject) Resources.Load("Prefabs/Enemy")).transform.rotation) as GameObject;
            enemies.Add(newEnemy);
            enemiesWaitingCounts--;
            
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    IEnumerator SpawnBoss()
    {      
        waveCleared = false;
        //enemyLeft = enemyNumber;
        enemies.Clear();
        waveIndex = 0;
        enemiesWaitingCounts = 1;

        yield return new WaitForSeconds(waveInterval);

        GetRandomSpawnPosition();
        int randomBoss = Random.Range(0, 2);
        GameObject newEnemy = new GameObject();
        switch (randomBoss)
        {
            case 0:
                newEnemy = Instantiate(Resources.Load("Prefabs/BossNew"), newSpawnPosition,
                    ((GameObject) Resources.Load("Prefabs/BossNew")).transform.rotation) as GameObject;
                break;
            case 1:
                newEnemy = Instantiate(Resources.Load("Prefabs/Boss"), newSpawnPosition,
                    ((GameObject) Resources.Load("Prefabs/Boss")).transform.rotation) as GameObject;
                break;
        }
        enemies.Add(newEnemy);
        enemiesWaitingCounts--;
        Services.EventManagerNew.Fire(new EnterBossEvent());
        
        yield return new WaitForSeconds(spawnInterval);
    }

    private void GetRandomSpawnPosition()
    {
        Transform newSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        newSpawnPosition = newSpawn.position;
    }

    /*    private void EnemyKilled(GameObject obj)
    {
        enemiesToDestroy.Add(obj);
        score++;
        scoreText.text = score.ToString();
        if (--enemyLeft <= 0)
        {
            waveCleared = true;
        }
    }*/
    
    private void EnemyKilled(EnemyKilledEvent myEvent)
    {
        enemiesToDestroy.Add(myEvent.enemyToDestroy);
        if (enemies.Count - enemiesToDestroy.Count <= 0 && enemiesWaitingCounts == 0)
        {
            waveCleared = true;
            if (myEvent.enemyToDestroy.CompareTag("Boss"))
            {
                Services.EventManagerNew.Fire(new ExitBossEvent());
            }
        }

        score += myEvent.score;
        scoreText.text = score.ToString();
    }
}