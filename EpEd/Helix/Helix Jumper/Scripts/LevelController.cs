using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelController : MonoBehaviour {

	public static LevelController instance;

	public TextMeshProUGUI scoreText;
	public GameObject gameOverPanel;
	public bool gameOver;

	private int score;
	private void Awake()
	{
		instance = this;
	}

	public void SetScore()
	{
		score++;
		if(scoreText != null)
			scoreText.text = score.ToString();
	}

	public void GameOver()
	{
		if(gameOverPanel != null)
			gameOverPanel.SetActive(true);

		gameOver = true;
	}

	public void ReloadScene()
	{
        GameManager.instance.State[5] = score / 20;

        if (GameManager.instance.State[5] > 10)
            GameManager.instance.State[5] = 10;

		SceneManager.LoadScene("TitleScene");
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
