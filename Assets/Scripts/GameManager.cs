using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("player stats")]
    public int money;
    public int beatedEnemies;
    public int level;


    [Header("UI objects")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI levelText;
    public GameObject moneyAdviceText;


    [Header("Game settings")]
    public Animator anim;
    public GameObject spawn1, spawn2, spawn3;
    public GameObject enemy;
    public int enemiesInGame = 0;



    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1, 8);
    }


    public void UpLevel()
    {
        if (money == 0)
        {
            StartCoroutine(moneyAdviceCor());
        }
        else
        {
            anim.SetTrigger("victory");
            money -= 1;
            moneyText.text = money.ToString();
            level += 1;
            levelText.text = "Lv: " + level.ToString();
        }
    }


    void SpawnEnemy()
    {
        if (enemiesInGame >= 3)
        {
            return;
        }
        else
        {
            enemiesInGame += 1;
            int rn = Random.Range(1, 3);
            GameObject spawnedEnemy = Instantiate(enemy);
            if (rn == 1) spawnedEnemy.transform.position = spawn1.transform.position;
            if (rn == 2) spawnedEnemy.transform.position = spawn2.transform.position;
            if (rn == 3) spawnedEnemy.transform.position = spawn3.transform.position;
        }
    }


    public void IncreaseMoney(int i)
    {
        money += i;
        moneyText.text = money.ToString();
        beatedEnemies = 0;
    }


    public IEnumerator moneyAdviceCor()
    {
        if (moneyAdviceText.activeInHierarchy)
        {
            yield return null;
        }
        else
        {
            moneyAdviceText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            moneyAdviceText.gameObject.SetActive(false);
        }
    }
}
