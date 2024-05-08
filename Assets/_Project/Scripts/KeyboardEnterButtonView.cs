using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardEnterButtonView : MonoBehaviour
{
    [SerializeField] private Button button;
    private GuessPod guessPod;
    void Start()
    {
        guessPod = GuessPod.Instance;
        button.onClick.AddListener(OnClick);

    }

    public void OnClick()
    {
        guessPod.Submit();
    }

}
