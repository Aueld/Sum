using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpinBallTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
