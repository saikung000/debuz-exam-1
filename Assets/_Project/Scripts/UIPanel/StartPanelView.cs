using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;


public class StartPanelView : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private Button startButton;

    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.GetState().Subscribe(gameState => gameObject.SetActive(gameState == GameState.StartMenu));

        startButton.onClick.AddListener(OnClick);

    }

    private void OnClick()
    {
        gameManager.ChangeState(GameState.Gameplay);
    }

}
