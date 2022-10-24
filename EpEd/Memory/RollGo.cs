using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollGo : MonoBehaviour
{
    [SerializeField] private List<BounceBall> Ball; 

    private void Start()
    {
        foreach(var ba in Ball)
        {
            ba.gameObject.SetActive(false);
        }

        StartCoroutine(StartRoll(0));
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if(Physics.Raycast)
        //}

        //if (trueBall)
        //{
        //    Debug.Log("dd");
        //    material.color = Color.green;
        //}
        //else
        //{
        //    Debug.Log("ss");
        //    material.color = Color.red;
        //}
    }

    private IEnumerator StartRoll(int level)
    {
        int index = 0;
        int count = 2;

        if (level == 1)
            count = 3;
        else if (level == 3)
            for (int r = 0; r < Ball.Count; r++)
                Ball[r].SetPower(120f);
        else if (level == 5)
            count = 4;
        else if (level == 6)
            for (int r = 0; r < Ball.Count; r++)
                Ball[r].SetPower(150f);
        else if (level == 8)
            count = 5;

        for (int i = 0; i < Ball.Count; i++)
        {
            Ball[i].SetTrueBall(false);
        }

        for (int i = 0; i < 3; i++)
        {


            for (int j = 0; j < count; j++)
            {
                index = Random.Range(0, Ball.Count);

                if (Ball[index].gameObject.activeSelf)
                    j--;

                Ball[index].gameObject.SetActive(true);
                Ball[index].SetTrueBall(true);

                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(2f);

            for (int j = 0; j < Ball.Count; j++)
            {
                if (!Ball[j].gameObject.activeSelf)
                    Ball[j].gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(2f);

            for (int j = 0; j < Ball.Count; j++)
            {
                Ball[j].SetisKinematic(true);
            }
        }



        yield return null;

        yield return null;
    }
}
