using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    [SerializeField] private Image image;

    private FadeInOut fadeInOut;

    private void Start()
    {
        fadeInOut = GetComponent<FadeInOut>();

        StartCoroutine(ScenesCredit());
    }

    private IEnumerator ScenesCredit()
    {
        yield return StartCoroutine(fadeInOut.CycleOutIn(3f, image));
        yield return new WaitForSeconds(2f);

        GM.Instance.skipCheck = false;

        SceneManager.LoadScene("Credit");
    }
}
