using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text lvText;
    public Text destroyEnemyCount;
    public Image soulBar;
    public GameObject rewardPanel;
    public GameObject startPanel;
    public AbilityManager abilityManager;

    public GameObject player;
    public Text playTimeText;

    public int level = 1;
    public float experience;
    public float maxExperience;

    private int destroyEnemyCnt = 0;
    private int wave = 1;
    private int gameTime = 0;
    private float gameTimer = 0f;

    private bool isBossSpawn;

    private string[] enemyNames = { "Skeleton", "Goblin", "Boss1" };

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        PlayTime();
        Spawn();
    }

    public void GameStart()
    {
        Time.timeScale = 1;
        startPanel.SetActive(false);
    }

    public void ReStart()
    {
        SceneManager.LoadScene(0);
    }

    private void PlayTime()
    {
        gameTimer += Time.deltaTime;
        if (gameTimer >= 1)
        {
            gameTime++;
            playTimeText.text = string.Format("{0:D2}:{1:D2}", gameTime / 60, gameTime % 60);   // 타이머 표시
            gameTimer = 0;
        }
    }

    // 적 소환 (player의 위치에서 원형으로 랜덤 소환)
    // 5초마다 10 + level 마리씩 소환, 최대 200마리 + maxUnit
    private void Spawn()
    {
        if (ObjectPool.Instance.MonsterPoolCount > 200)
            return;

        int maxUnit = 5 + level;

        if(gameTime > 5 * wave)  
        {
            wave++;

            for (int i = 0; i < maxUnit; i++)
            {
                int ran = Random.Range(0, 10);
                int index;

                if (ran > 3)
                    index = 0;
                else
                    index = 1;

                var obj = ObjectPool.Instance.GetObject(enemyNames[index]);// enemyNames[index]);
                EnemyController enemyController;


                if (isBossSpawn)
                {
                    isBossSpawn = false;
                    obj = ObjectPool.Instance.GetObject(enemyNames[2]);// enemyNames[index]);
                }

                enemyController = obj.GetComponent<EnemyController>();
                enemyController.gm = this;
                enemyController.player = player;
                obj.transform.position = RandomPosition();
                obj.SetActive(true);
            }
            
        }
    }

    public void DestroyEnemyCount()
    {
        destroyEnemyCnt++;
        if (destroyEnemyCnt % 100 == 0)
            isBossSpawn = true;
        destroyEnemyCount.text = destroyEnemyCnt.ToString();
    }

    public Vector3 RandomPosition()
    {
        float ranAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector3 pos = player.transform.position + new Vector3(60 * Mathf.Cos(ranAngle), 60 * Mathf.Sin(ranAngle), 0);
        return pos;
    }

    public void LevelUp(float expAmount)
    {
        experience += expAmount;
        
        lvText.text = "LV : " + level.ToString();
        soulBar.fillAmount = experience / maxExperience;

        if (experience >= maxExperience)
        {
            rewardPanel.SetActive(true);

            level++;
            experience = experience - maxExperience;
            maxExperience *= 1.1f;

            if (level % 10 == 0)
                maxExperience *= 1.5f;

            lvText.text = "LV : " + level.ToString();
            soulBar.fillAmount = experience / maxExperience;

            Time.timeScale = 0;

            abilityManager.Display();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        rewardPanel.SetActive(false);
    }
}
