using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroGoal : MonoBehaviour
{
    [SerializeField] private GameObject flag;

    GameObject[] coin;

    private void Start()
    {
        coin = GameObject.FindGameObjectsWithTag("GyroCoin");
        //Coin(Clone)
    }

    private void Update()
    {
        if (AllOBJ())
            flag.SetActive(true);
    }

    private bool AllOBJ()
    {
        for (int i = 0; i < coin.Length; ++i)
        {
            if (coin[i].activeSelf == false)
            {
                return false;
            }
        }

        return true;
    }
}
