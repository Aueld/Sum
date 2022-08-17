using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class G_GameManager : MonoBehaviour
{
    // 게임 버튼 프리팹
    //public Button StartButton;
    public Button hitButton;
    public Button standButton;
    public Button exitButton;

    private int standClicks = 0;

    // 플레이어와 딜러
    public G_Player playerScript;
    public G_Player dealerScript;

    // TMP
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI dealerScoreText;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI standText;
    public TextMeshProUGUI SoulText;

    // 딜러 카드 위에 덮을 뒷면의 카드
    public GameObject hideCard;

    // 배팅되는 영혼의 파편 수
    // 승리시 기존 보유수의 두배, 패배시 기존 보유수의 절반, 무승부시 변동 없음 (소수점일때 버림 처리)
    public int soul = 1000;

    private void Start()
    {
        // 버튼 리스너 부여
        // 버튼 onClick
        hitButton.onClick.AddListener(() => HitClicked());
        standButton.onClick.AddListener(() => StandClicked());

        InitGame();
    }

    // 게임 시작
    private void InitGame()
    {
        // 게임 초기화
        playerScript.ResetHand();
        dealerScript.ResetHand();
        
        // 딜러 카드 한장 숨기기
        dealerScoreText.gameObject.SetActive(false);
        mainText.gameObject.SetActive(false);
        dealerScoreText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<G_Deck>().Shuffle();

        // 셔플 후 배치
        playerScript.StartHand();
        dealerScript.StartHand();
        
        // 갱신
        scoreText.text = "내 숫자 합 : " + playerScript.handValue.ToString();
        dealerScoreText.text = "딜러 숫자 합 : " + dealerScript.handValue.ToString();
        
        // 딜러 카드
        hideCard.GetComponent<Image>().enabled = true;
        
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
        
        standText.text = "카드 받지 않기";

        SoulText.text = playerScript.GetSoul().ToString();
    }

    private void HitClicked()
    {
        // 뽑을 수 있는 카드가 있는지 판단
        if (playerScript.cardIndex <= 12)
        {
            //playerScript.GetCard().CardMove();

            playerScript.GetCardValue();
            scoreText.text = "내 숫자 합 : " + playerScript.handValue.ToString();
            
            if (playerScript.handValue > 20)
                RoundOver();
        }
    }

    private void StandClicked()
    {
        standClicks++;
        if (standClicks > 1) RoundOver();
        HitDealer();
        standText.text = "카드 공개";
    }

    private void HitDealer()
    {
        while (dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCardValue();
            dealerScoreText.text = "딜러 숫자 합 : " + dealerScript.handValue.ToString();
            if (dealerScript.handValue > 20) RoundOver();
        }
    }

    // 승패 판단
    void RoundOver()
    {
        // 블랙잭 판단
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;

        // 스탠드 두번 클릭 이하일때 게임 종료시
        if (standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21) return;
        
        bool gameOver = true;

        // 둘 다 버스트인 경우
        if (playerBust && dealerBust)
        {
            mainText.text = "무승부";
        }
        // 플레이어 버스트 또는 딜러가 더 많은 합을 가질때
        else if (playerBust || (!dealerBust && dealerScript.handValue > playerScript.handValue))
        {
            mainText.text = "패배";
            playerScript.Lose();
        }
        // 딜러 버스트 또는 플레이어가 더 많은 합을 가질때
        else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {
            mainText.text = "승리";
            playerScript.WIn();
        }
        // 동일한 합일때
        else if (playerScript.handValue == dealerScript.handValue)
        {
            mainText.text = "무승부";
        }
        else
        {
            gameOver = false;
        }

        // 게임 오버
        if (gameOver)
        {
            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            hideCard.GetComponent<Image>().enabled = false;
            
            exitButton.gameObject.SetActive(true);
            mainText.gameObject.SetActive(true);
            dealerScoreText.gameObject.SetActive(true);

            SoulText.text = "" + playerScript.GetSoul().ToString();
            standClicks = 0;
        }
    }

    // 영혼 배팅
    //void BetClicked()
    //{
    //    int intBet = int.Parse(newBet.text.ToString().Remove(0, 1));
    //    playerScript.AdjustSoul(-intBet);
    //    soulText.text = "" + playerScript.GetSoul().ToString();
    //    soul += (intBet * 2);
    //}
}
