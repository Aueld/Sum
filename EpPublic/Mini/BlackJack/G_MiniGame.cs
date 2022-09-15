using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_MiniGame : MonoBehaviour
{
    [SerializeField] private GameObject BlackJack;

    private GameObject controller;
    private GameObject play;

    private void OnEnable()
    {
        controller = gameObject;
    }

    public void StartBlackJack()
    {
        play = BlackJack;
        BlackJack.SetActive(true);
        controller.SetActive(false);
    }

    public void Exit()
    {
        play.SetActive(false);
        controller.SetActive(true);
    }
}
