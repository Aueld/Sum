using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        float rate = 1 - (GameManager.playTime / GameManager.gameTime);
        slider.value = rate;

    }
}
