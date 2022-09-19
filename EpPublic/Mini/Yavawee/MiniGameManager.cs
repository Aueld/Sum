using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private GameObject Riffle;

    private GameObject controller;
    private GameObject play;

    public static MiniGameManager instance;
    public bool isShuffle = false;

    private void OnEnable()
    {
        instance = this;

        controller = gameObject;
    }

    public void StartRiffle()
    {
        play = Riffle;
        Riffle.SetActive(true);
        controller.SetActive(false);
    }

    public void Exit()
    {
        play.SetActive(false);
        controller.SetActive(true);
    }
}
