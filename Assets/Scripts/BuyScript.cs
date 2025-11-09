using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BuyScript : MonoBehaviour
{
    private UIScript UIScript;
    private PlayerStats playerStats;
    private EnemySpawner enemySpawner;
    private AudioManager audioManager;

    private int cashNeededToUpgradePlayer = 7;
    private int cashNeededToUpgradeHelper = 6;
    private int cashNeededToBuySlots = 5;

    [SerializeField] TextMeshProUGUI upgradePlayerText;
    [SerializeField] TextMeshProUGUI upgradeHelperText;
    [SerializeField] TextMeshProUGUI buySlotsText;



    private int playerUpgradeLevel = 0;


    
    public void UpgradePlayer()
    {
        if (UIScript.getCash() >= cashNeededToUpgradePlayer )
        {
            //Handheld.Vibrate();
            UIScript.addCash(-cashNeededToUpgradePlayer);
            cashNeededToUpgradePlayer += 7;
            //limitováno aby hráè nemohl mít negativní attack speed
            if (playerUpgradeLevel < 3)
            {
                playerStats.UpgradePlayerAttSpeed();
            }
            playerStats.UpgradePlayerDmg();
            playerUpgradeLevel++; 
        }

    }

    public void UpgradeHelper()
    {
        //list pripojenych turret
        var helpers = GameObject.FindGameObjectsWithTag("AttachedHelper");
        List<GameObject> helpersToUpgrade = new List<GameObject>();
        foreach (var helper in helpers)
        {
            //kontrola jestli jeste pripojeny turret neni vylepsen na max lvl
            if (helper.GetComponent<HelperAttach>().Upgradable())
            {
                //prida do seznamu upgradovatelnych helperu
                helpersToUpgrade.Add(helper);
            }
        }
        if (UIScript.getCash() >= cashNeededToUpgradeHelper && helpersToUpgrade.Count > 0)
        {
            //Handheld.Vibrate();
            var whoToUpgrade = Random.Range(0, helpersToUpgrade.Count);
            helpersToUpgrade[whoToUpgrade].GetComponent<HelperAttach>().UpgradeHelper();
            UIScript.addCash(-cashNeededToUpgradeHelper);
            cashNeededToUpgradeHelper += 6;
        }
    }

    public void BuySlots()
    {
        //lze provest pouze pokud se jiz nenachazi v prostøedí nejaky spawnuty turret
        if (UIScript.getCash() >= cashNeededToBuySlots && GameObject.FindWithTag("Helper") == null)
        {
            UIScript.addCash(-cashNeededToBuySlots);
            cashNeededToBuySlots += 5;
            enemySpawner.RequestHelperSpawn();
        }

    }

    private void Awake()
    {
        UIScript = FindObjectOfType<UIScript>();
        playerStats = FindObjectOfType<PlayerStats>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        audioManager = FindObjectOfType<AudioManager>();


    }

    // Start is called before the first frame update
    void Start()
    {
        upgradeHelperText.text = "LOCKED";
    }

    // Update is called once per frame
    void Update()
    {
        upgradePlayerText.text = "UPGRADE PLAYER: " + cashNeededToUpgradePlayer.ToString("000") + "$";
        if (GameObject.FindGameObjectsWithTag("AttachedHelper").Length > 0)
            upgradeHelperText.text = "UPGRADE TURRET: " + cashNeededToUpgradeHelper.ToString("000") + "$"; 
        buySlotsText.text = "BUY TURRET: " + cashNeededToBuySlots.ToString("000") + "$";
    }
}
