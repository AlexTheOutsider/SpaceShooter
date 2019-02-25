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

public class EnemyManager : Singleton<EnemyManager>
{
    public int enemyNumber = 3;
    public float spawnInterval = 1f;
    public float waveInterval = 3f;
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
        EventManagerNew.Instance.Register<EnemyKilledEvent>(EnemyKilled);
    }

    private void OnDisable()
    {
        //EventManager.Instance.StopListening("EnemyKilled", EnemyKilled);
        EventManagerNew.Instance.Unregister<EnemyKilledEvent>(EnemyKilled);
    }

    private void Start()
    {
        spawnPoints = GameObject.Find("Spawn Points").transform.GetComponentsInChildren<Transform>();
        enemies = new List<GameObject>();
        enemiesToDestroy = new List<GameObject>();
        scoreText = GameObject.Find("Canvas").transform.Find("Score").GetComponent<Text>();
        scoreText.text = "0";
    }

    private void Update()
    {
        foreach (GameObject dead in enemiesToDestroy)
        {
            enemies.Remove(dead);
            Destroy(dead);
        }
        enemiesToDestroy.Clear();

        if (!waveCleared) return;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {      
        waveIndex++;
        waveCleared = false;
        //enemyLeft = enemyNumber;
        enemies.Clear();
        yield return new WaitForSeconds(waveInterval);

        for (int i = 0; i < enemyNumber; i++)
        {
            GetRandomSpawnPosition();
            GameObject newEnemy = Instantiate(Resources.Load("Prefabs/Enemy"), newSpawnPosition,
                ((GameObject) Resources.Load("Prefabs/Enemy")).transform.rotation) as GameObject;
            enemies.Add(newEnemy);
            
            yield return new WaitForSeconds(spawnInterval);
        }
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
        if (enemies.Count - enemiesToDestroy.Count <= 0)
        {
            waveCleared = true;
        }

        score += myEvent.score;
        scoreText.text = score.ToString();
    }
}