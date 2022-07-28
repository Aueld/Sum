using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Unit
{
    #region Boss variable

    public Slider slider;       // 보스 체력바

    #endregion


    private void LateUpdate()
    {
        float hpBar = HP / MaxHP;
        slider.value = hpBar;

        BossLevelUp();

    }

    // 보스 처치시 레벨 상승 후 레벨에 비례한 회복
    private void BossLevelUp()
    {
        if (HP < 0)
        {
            GameManager.bossDown = true;

            Level++;

            HP = MaxHP * (1 + (Level / 10));
            //HP = MaxHP * Level;
            MaxHP = HP;
        }
    }
}
