using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpinStage : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] Transform SpinBall;

    private int[] generateNum = new int[] { 2, 2, 3, 3, 4, 4, 5, 5, 5, 7 };

    private Vector3 spownPos;

    private void Start()
    {
        spownPos = new Vector3(0, 0, SpinBall.position.z + 15);

        GameManager.instance.State[4] = 0;

        StartCoroutine(CoBlockGen());
    }

    private IEnumerator CoBlockGen()
    {
        int count = 0;
        GameObject block = null;

        for (int i = 0; i < generateNum.Length; i++)
        {
            for (int j = 0; j <= generateNum[i]; j++)
            {
                if(j == generateNum[i])
                    block = Instantiate(prefabs[0], spownPos, Quaternion.identity);
                else
                    block = Instantiate(prefabs[Random.Range(1, prefabs.Count)], spownPos, Quaternion.identity);

                yield return new WaitForSeconds(8f);

                spownPos = new Vector3(0, 0, SpinBall.position.z + 15f);
            }

            if(GameManager.instance.State[4] > 10)
            {
                GameManager.instance.State[4] = 10;
                SceneManager.LoadScene("TitleScene");
            }
            GameManager.instance.State[4]++;
            
            count++;
            Debug.Log("»£√‚ " + count);
        }
    }
}
