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


    [Header("Game settings")]
    [SerializeField] Animator anim;
    [SerializeField] GameObject spawn1, spawn2;
    [SerializeField] GameObject enemy;
    public int enemiesInGame = 0;
    bool UsedSpawn1, UsedSpawn2 = false;


    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1, 7);
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
            UIManager.Instance.SetMoneyText(money);
            level += 1;
            UIManager.Instance.SetLevel(level);

        }
    }


    void SpawnEnemy()
    {
        if (enemiesInGame >= 2)
        {
            return;
        }
        else
        {
            enemiesInGame += 1;
            GameObject spawnedEnemy = Instantiate(enemy);

            if (!spawn1.GetComponent<SpawnCheck>().usedSpawn)
            {
                spawnedEnemy.transform.position = spawn1.transform.position;
            }
            else if (!spawn2.GetComponent<SpawnCheck>().usedSpawn)
            {
                spawnedEnemy.transform.position = spawn2.transform.position;
            }
            else return;

        }
    }


    public void IncreaseMoney(int i)
    {
        money += i;
        UIManager.Instance.SetMoneyText(money);
        beatedEnemies = 0;
    }


    public IEnumerator moneyAdviceCor()
    {
        if (UIManager.Instance.moneyAdviceText.activeInHierarchy)
        {
            yield return null;
        }
        else
        {
            UIManager.Instance.moneyAdviceText.SetActive(true);
            yield return new WaitForSeconds(1);
            UIManager.Instance.moneyAdviceText.SetActive(false);
        }
    }
}
