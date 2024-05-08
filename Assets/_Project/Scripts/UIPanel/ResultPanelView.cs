using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;

public class ResultPanelView : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI guessCountText;
    [SerializeField] private TextMeshProUGUI guessWrongText;

    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.GetState().Subscribe(gameState => gameObject.SetActive(gameState == GameState.Result));

        restartButton.onClick.AddListener(OnClick);

    }

    private void OnClick()
    {
        gameManager.ChangeState(GameState.Gameplay);
    }

}
