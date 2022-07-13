using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public Player player;
    public GameManager gm;
    public GameObject rewardPanel;

    public GameObject bigBulletPrefab;
    public GameObject basicPrefab;

    public string[] abilityName;
    public string[] abilityContent;
    public Sprite[] sprites;

    public Text[] abilityNameText;
    public Text[] abilityContentText;
    public Text[] abilityLevelText;
    public Image[] abilitySpriteImage;

    public Image[] takenAbilityImage;
    public Image[] takenPassiveAbilityImage;

    private List<int> abilityOrder;

    private int[] level = { 0, 1, 0, 0 };
    private int takenAbility = 0;
    private int takenPassiveAbility = 0;

    private void Awake()
    {
        // 기본 공격
        takenAbilityImage[takenAbility].sprite = sprites[1];
        takenAbility++;
    }

    public void Display()
    {
        abilityOrder = new List<int>();
        SelectRandomAbility();

        for (int i = 0; i < abilitySpriteImage.Length; i++)
        {
            int randomAbility = abilityOrder[i];

            abilityNameText[i].text = abilityName[randomAbility];
            abilityContentText[i].text = abilityContent[randomAbility];
            abilitySpriteImage[i].sprite = sprites[randomAbility];
            abilityLevelText[i].text = "Level : " + level[randomAbility].ToString();
        }
    }
    
    private void DisplayAbility(int abilityNumber)
    {
        if(abilityNumber < 2)   // ability
        {
            takenAbilityImage[takenAbility].sprite = sprites[abilityNumber];
            takenAbility++;
        }
        else                    // passive
        {
            takenPassiveAbilityImage[takenPassiveAbility].sprite = sprites[abilityNumber];
            takenPassiveAbility++;
        }
    }

    private void SelectRandomAbility()
    {
        int currentAbility = Random.Range(0, sprites.Length);
        for(int i = 0; i < sprites.Length;)
        {
            if(abilityOrder.Contains(currentAbility)) currentAbility = Random.Range(0, sprites.Length);
            else { abilityOrder.Add(currentAbility); i++; }
        }
    }

    public void RankUp1()
    {
        string ability = abilityNameText[0].text;
        SwitchAbility(ability);
    }
    public void RankUp2()
    {
        string ability = abilityNameText[1].text;
        SwitchAbility(ability);
    }
    public void RankUp3()
    {
        string ability = abilityNameText[2].text;
        SwitchAbility(ability);
    }

    private void SwitchAbility(string ability)
    {
        switch (ability)
        {
            case "칼 휘두르기":
                if (!player.hasAbilityBasic) { player.hasAbilityBasic = true; DisplayAbility(1); }
                else
                {
                    for (int i = 0; i < ObjectPool.Instance.BasicAttackPool.Count; i++)
                    {
                        Basic basic = ObjectPool.Instance.BasicAttackPool[i].GetComponent<Basic>();
                        basic.damage += 2;
                        ObjectPool.Instance.BasicAttackPool[i].transform.localScale += Vector3.one * 3f;
                    }
                }
                level[1]++;
                break;


            case "Power Up":
                if (level[2] == 0) DisplayAbility(2);
                player.playerDamage *= 1.1f;
                level[2]++;
                break;
            case "Speed Up":
                if (level[3] == 0) DisplayAbility(3);
                player.speed *= 1.05f;
                level[3]++;
                break;

                //----------------------------------------------------
                


            case "거대 탄환":
                if (!player.hasAbilityBigBullet) { player.hasAbilityBigBullet = true; DisplayAbility(0); }
                else
                {
                    for (int i = 0; i < ObjectPool.Instance.BigBulletPool.Count; i++)
                    {
                        BigBullet bigBullet = ObjectPool.Instance.BigBulletPool[i].GetComponent<BigBullet>();
                        bigBullet.damage += 0.2f;
                        ObjectPool.Instance.BigBulletPool[i].transform.localScale += Vector3.one * 0.05f;
                    }
                }
                level[0]++;
                break;

        }
        gm.Resume();
    }
}
