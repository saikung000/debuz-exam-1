using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using System;

public class ResultPanelView : MonoBehaviour
{
    private GuessPod guessPod;
    private GameManager gameManager;
    [SerializeField] private GameObject backgroundImage;
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI answerText;
    [SerializeField] private TextMeshProUGUI guessCountText;
    [SerializeField] private TextMeshProUGUI guessWrongText;
    private IDisposable delayShow;

    void Start()
    {
        guessPod = GuessPod.Instance;
        gameManager = GameManager.Instance;
        gameManager.GetState().Subscribe(gameState =>
        {
            gameObject.SetActive(true);
            answerText.text = guessPod.correctWord.Value;
            if (gameState == GameState.Result)
            {
                delayShow?.Dispose();
                delayShow = Observable.Timer(TimeSpan.FromSeconds(0.5f)).Subscribe(_ =>
                {
                    backgroundImage.SetActive(true);
                    guessCountText.gameObject.SetActive(guessPod.isWin);
                    guessWrongText.gameObject.SetActive(!guessPod.isWin);
                    guessCountText.text = "Guess Correct \n Guess " + guessPod.guessCount.Value + " Times";
                });
            }
            else
            {
                delayShow?.Dispose();
                gameObject.SetActive(false);
                backgroundImage.SetActive(false);
            }
        });

        restartButton.onClick.AddListener(OnClick);

    }

    private void OnClick()
    {
        gameManager.ChangeState(GameState.Gameplay);
    }

}
