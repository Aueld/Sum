using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class FadeInOut : MonoBehaviour
{
	[SerializeField] private Image FrontImage;
	[SerializeField] private List<GameObject> profile;
	[SerializeField] private List<Image> IconImage;
	[SerializeField] private List<string> titleTexts;
    [SerializeField] private TextMeshProUGUI title;

	public bool endCheck;

	private Dictionary<string, List<string>> textDict = new Dictionary<string, List<string>>();
	private TextMeshProUGUI centerText;
	private Image centerImage;
	private int[] index = {1, 2, 2, 3, 3, 2 };

	private void Start()
	{
		if (title == null)
			return;
		else
		{
			SetTextDict();

			titleTexts = new List<string>(textDict.Keys);
			enabled = false;

			StartCoroutine(Skip());

			StartCoroutine(PrintText());
		}
	}

    private void SetTextDict()
	{
		textDict.Add("Key0", new List<string>() { "Value0", "Value1" });
		textDict.Add("Key1", new List<string>() { "Value2", "Value3" });
		textDict.Add("Key2", new List<string>() { "Value4", "Value5", "Value6" });
		textDict.Add("Key3", new List<string>() { "Value7", "Value8" });
		textDict.Add("Key4", new List<string>() { "Value9" });
		textDict.Add("Key5", new List<string>() { "Value10", "Value11" });
	}

	private IEnumerator PrintText()
	{
		int count = 0;

        for (int i = 0;  i < titleTexts.Count; i++)
        {
			if (profile != null)
				for (int k = 0; k < 3; k++)
				{
					profile[k].SetActive(false);
				}

			// 키값 텍스트에 숫자 제거
			title.text = Regex.Replace(titleTexts[i], @"\d", "");

			List<string> str = textDict[titleTexts[i]];

			for (int j = 0; j < index[i]; j++)
            {
                profile[j].SetActive(true);

				centerText = profile[j].GetComponentInChildren<TextMeshProUGUI>();
				centerText.text = str[j];

				centerImage = profile[j].GetComponentInChildren<Image>();
                centerImage = IconImage[count];
				count++;
            }

            yield return StartCoroutine(CycleOutIn(2f, FrontImage));
		}

		endCheck = true;
	}

	private IEnumerator Skip()
    {
        while (true)
        {
			if (GM.Instance.skipCheck == true)
			{
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					Debug.Log("스킵3");
					SceneManager.LoadScene("");
				}
			}

			yield return null;
        }
    }

	public IEnumerator CycleOutIn(float time, Image image)
	{
		yield return StartCoroutine(CoFadeOut(time / 2, image));
		yield return StartCoroutine(View(time));
		yield return StartCoroutine(CoFadeIn(time / 2, image));
	}

	public IEnumerator CycleInOut(float time, Image image)
	{
		yield return StartCoroutine(CoFadeIn(time / 2, image));
		yield return StartCoroutine(View(time));
		yield return StartCoroutine(CoFadeOut(time / 2, image));
	}

	// 투명 -> 불투명
	public IEnumerator CoFadeIn(float fadeOutTime, Image image)
	{
		Color tempColor = image.color;

		while (tempColor.a < 1f)
		{
			tempColor.a += Time.deltaTime / fadeOutTime;
			image.color = tempColor;

			if (tempColor.a >= 1f) tempColor.a = 1f;

			yield return null;
		}

		image.color = tempColor;


	}

	// 불투명 -> 투명
	private IEnumerator CoFadeOut(float fadeOutTime, Image image)
	{
		Color tempColor = image.color;

		while (tempColor.a > 0f)
		{
			tempColor.a -= Time.deltaTime / fadeOutTime;
			image.color = tempColor;

			if (tempColor.a <= 0f)
				tempColor.a = 0f;

			yield return null;
		}

		image.color = tempColor;

    }
	private IEnumerator View(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
	}
}
