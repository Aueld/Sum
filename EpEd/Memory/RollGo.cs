using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RollGo : MonoBehaviour
{
    [SerializeField] private List<BounceBall> Ball;

    private int selectCount;
    private int count;

    private bool ban;

    private void Start()
    {
        GameManager.instance.selectLevel = 1;

        selectCount = 0;
        count = 2;

        ban = true;

        foreach (var ba in Ball)
        {
            ba.gameObject.SetActive(false);
        }

        StartCoroutine(StartRoll(GameManager.instance.selectLevel));
    }

    private void Update()
    {
        if (ban)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "MemoryBall")
                {
                    if (hit.transform.gameObject.GetComponent<BounceBall>().GetTrueBall())
                    {
                        hit.transform.gameObject.GetComponent<BounceBall>().GetMaterial().material.color = Color.green;
                        selectCount++;

                        if (count == selectCount)
                        {
                            selectCount = 0;

                            GameManager.instance.selectLevel++;

                            foreach (var ba in Ball)
                            {
                                ba.gameObject.SetActive(false);
                                ba.SetisKinematic(false);
                                ba.GetMaterial().material.color = Color.white;
                            }

                            StartCoroutine(StartRoll(GameManager.instance.selectLevel));

                        }

                    }
                    else
                    {
                        hit.transform.gameObject.GetComponent<BounceBall>().GetMaterial().material.color = Color.red;
                        ban = true;

                        StartCoroutine(GoTitle());
                    }
                }
            }
        }
    }

    private IEnumerator StartRoll(int level)
    {
        yield return new WaitForSeconds(2f);

        int index = 0;

        if (level == 2)
            count = 3;
        else if (level == 4)
            for (int r = 0; r < Ball.Count; r++)
                Ball[r].SetPower(120f);
        else if (level == 6)
            count = 4;
        else if (level == 7)
            for (int r = 0; r < Ball.Count; r++)
                Ball[r].SetPower(150f);
        else if (level == 9)
            count = 5;

        for (int i = 0; i < Ball.Count; i++)
        {
            Ball[i].SetTrueBall(false);
        }

        for (int i = 0; i < count; i++)
        {
            index = UnityEngine.Random.Range(0, Ball.Count);

            if (Ball[index].gameObject.activeSelf)
                continue;

            Ball[index].gameObject.SetActive(true);
            Ball[index].SetTrueBall(true);

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < Ball.Count; i++)
        {
            if (!Ball[i].gameObject.activeSelf)
                Ball[i].gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < Ball.Count; i++)
        {
            Ball[i].SetisKinematic(true);
        }
        
        yield return new WaitForSeconds(0.5f);

        ban = false;
    }

    private IEnumerator GoTitle()
    {
        yield return new WaitForSeconds(3f);
    }
}
