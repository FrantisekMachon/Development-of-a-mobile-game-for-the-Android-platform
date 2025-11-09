using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float spawnInterval = 5f;
    private float spawnTimer = 5f;
    private float updateSpawnCountTimer = 10f;
    private float updateSpawnCountInterval = 10f;
    private float elapsedTime = 0;
    private int timer = 0;
    private int lastTime = 0;
    private float healingKitSpawnTimer = 30f;
    private float healingKitSpawnInterval = 30f;
    private float basicEnemyPercentage = 1f;
    private float fastEnemyPercentage = 0;
    private float shooterEnemyPercentage = 0;

    int amountOfBasic;
    int amountOfFast;
    int amountOfShooter;

    private int waveSize = 4;
    private int minWaveSize = 2;


    //spawn range
    Vector2 minPos = new Vector2(-6,-6);
    Vector2 maxPos = new Vector2(6,6);
    //min vzdalenost od hrace
    float noGoZone = 4f;

    private bool updateSpawns = true;


    //list of possible enemies/helpers 
    [SerializeField] private List<GameObject> enemyTypeList = new List<GameObject>();

    private AudioManager audioManager;
    //private KillzoneScript killzoneScript;
    private BossEventScript bossEventScript;
    //private EnemyStats enemyStats; 

    void Start()
    {
        //bossSpawned = true;   
    }

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        //killzoneScript = FindObjectOfType<KillzoneScript>();
        bossEventScript = FindObjectOfType<BossEventScript>();
        //enemyStats = FindObjectOfType<EnemyStats>();
    }


    public int GetTimer()
    {
        return timer;
    }

    public void RequestHelperSpawn()
    {
        Instantiate(enemyTypeList[3], GenerateSpawnPos(), Quaternion.identity);
    }

    void Update()
    {
        if (true) //predelat
        {
            //ukládání èasu
            elapsedTime += Time.deltaTime;
            //èasovaè pøeveden do celých èísel
            timer = Mathf.RoundToInt(elapsedTime);
            
            if (updateSpawns)
            {
                switch (timer)
                {
                    case 30:
                        basicEnemyPercentage = 0;
                        fastEnemyPercentage = 1;
                        waveSize = 3;
                        updateSpawns = false;
                        lastTime = timer;
                        break;
                    case 50:
                        fastEnemyPercentage = 0;
                        shooterEnemyPercentage = 1;
                        updateSpawns = false;
                        waveSize = 3;
                        lastTime = timer;
                        break;
                    case 70:
                        basicEnemyPercentage = 0.6f;
                        fastEnemyPercentage = 0.3f;
                        shooterEnemyPercentage = 0.1f;
                        updateSpawns = false;
                        waveSize = 8;
                        lastTime = timer;
                        break;
                    case 180:
                        //spawn boss
                        bossEventScript.SetHasStarted();
                        updateSpawns = false;
                        lastTime = timer;
                        break;
                    case 300:
                        //enemyStats.DoubleHPScore();
                        updateSpawns = false;
                        lastTime = timer;
                        break;
                    case 420:
                        //spawn boss
                        if(!bossEventScript.GetHasStarted())
                            bossEventScript.SetHasStarted();
                        //bossSpawned = true;
                        updateSpawns = false;
                        lastTime = timer;
                        break;
                    case 540:
                        //enemyStats.DoubleHPScore();
                        updateSpawns = false;
                        lastTime = timer;
                        break;
                }
            }
            else if(timer != lastTime)
            {
                updateSpawns = true;
            }
            if (!bossEventScript.GetHasStarted() && GameObject.FindGameObjectWithTag("Player") !=null)
            {
                if ((elapsedTime > updateSpawnCountTimer) && timer > 70)
                {
                    waveSize += 2;
                    minWaveSize++;
                    updateSpawnCountTimer = elapsedTime + updateSpawnCountInterval;
                }
                if ((elapsedTime > healingKitSpawnTimer))
                {
                    audioManager.PlayBackgroundNoise();
                    Instantiate(enemyTypeList[4], GenerateSpawnPos(), Quaternion.identity);
                    healingKitSpawnTimer = elapsedTime + healingKitSpawnInterval;
                }

                if (elapsedTime > spawnTimer)
                {
                    //randomizace velikosti vlny nepøátel, tato velikost je postupnì navyšována
                    //v momentální verzi každých 10 sekund
                    int sizeOfWave = Random.Range(minWaveSize, waveSize);
                    //procento jednotlivých nepøátel ve vlnì
                    amountOfBasic = Mathf.RoundToInt(sizeOfWave * basicEnemyPercentage);
                    amountOfFast = Mathf.RoundToInt(sizeOfWave * fastEnemyPercentage);
                    amountOfShooter = Mathf.RoundToInt(sizeOfWave * shooterEnemyPercentage);
                    //když je vlna vetší než 10 dojde k jejímu rozdìlení na tøetiny
                    //problém s vyýkonem sice nenastává již hned pøi spawnu
                    //10 nepøátel v jednu chvíli avšak i tak to výkonu pomùže
                    if (sizeOfWave > 10)
                    {
                        amountOfBasic = Mathf.RoundToInt(sizeOfWave * basicEnemyPercentage * 0.33f);
                        amountOfFast = Mathf.RoundToInt(sizeOfWave * fastEnemyPercentage * 0.33f);
                        amountOfShooter = Mathf.RoundToInt(sizeOfWave * shooterEnemyPercentage * 0.33f);
                        //Spawn jednotlivých tøetin je odložen o 1.5 sekundy
                        //kdyby se v budoucích verzích hry ukázalo takovéto rozdìlení jako stále nedostateèné,
                        //šlo by rozdelit spawny i dále èi využít pooling (pøedem naspawnovat enemy mimo herní plochu
                        //a následnì je pouze do herní oblasti pøemístit)
                        Invoke("SpawnEnemies", 0f);
                        Invoke("SpawnEnemies", 1.5f);
                        Invoke("SpawnEnemies", 3f);
                    }
                    else
                    {
                        SpawnEnemies();
                    }
                    spawnTimer = elapsedTime + spawnInterval;
                }
            }

        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < amountOfBasic; i++)
            Instantiate(enemyTypeList[0], 
                GenerateSpawnPos(), Quaternion.identity);
        for (int i = 0; i < amountOfFast; i++)
            Instantiate(enemyTypeList[1], 
                GenerateSpawnPos(), Quaternion.identity);
        for (int i = 0; i < amountOfShooter; i++)
            Instantiate(enemyTypeList[2], 
                GenerateSpawnPos(), Quaternion.identity);
    }

    private Vector2 GenerateSpawnPos()
    {
        Vector2 tempPos;
        do
        {
            tempPos = new Vector2(
                    Random.Range(minPos.x, maxPos.x),
                    Random.Range(minPos.y, maxPos.y)
                    );
        }
        while (Vector2.Distance(tempPos, GameObject.FindGameObjectWithTag("Player").transform.position) < noGoZone);
        
        return tempPos;
    }
}
