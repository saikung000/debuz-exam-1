using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameplayPanelView : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

}