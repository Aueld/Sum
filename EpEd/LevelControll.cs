using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelControll : MonoBehaviour
{
    
    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] Transform LevelContent;

    [SerializeField] int maxLevel = 10;

    List<GameObject> btn = new List<GameObject>();

    private List<Vector2> btnPos = new List<Vector2>();

    private void Start()
    {
        btnPos.Clear();

        for (int i = 0; i <= maxLevel / 2; i++)
        {
            btnPos.Add(new Vector2(260, -200 - (i * 400)));
            btnPos.Add(new Vector2(700, -200 - (i * 400)));
        }

        for (int i = 0; i < maxLevel; i++)
        {
            btn.Add(Instantiate(ButtonPrefab, LevelContent).gameObject);

            btn[i].transform.localPosition = btnPos[i];

            int index = i + 1;
            btn[i].GetComponent<Button>().onClick.RemoveAllListeners();
            btn[i].GetComponent<Button>().onClick.AddListener(() => OnSelectButton(index));

            btn[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = (index).ToString();
        }
    }

    public void OnSelectButton(int level)
    {
        GameManager.instance.selectLevel = level;
        SceneManager.LoadScene(GameManager.instance.loadScene) ;
    }
}
