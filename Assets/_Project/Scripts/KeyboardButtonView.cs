using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardButtonView : MonoBehaviour
{
    [SerializeField] private string input;
    [SerializeField] private Button button;
    [SerializeField] private Image backgroundImage;
    private ReactiveProperty<KeyboardState> keyboardState = new ReactiveProperty<KeyboardState>(KeyboardState.Normal);
    [SerializeField] private SerializedDictionary<KeyboardState, Color> colorDict = new SerializedDictionary<KeyboardState, Color>();

    private GuessPod guessPod;
    void Start()
    {
        guessPod = GuessPod.Instance;
        keyboardState.Subscribe(state => backgroundImage.color = colorDict[state]);
        button.onClick.AddListener(OnClick);

    }

    public void OnClick()
    {
        guessPod.AddWord(input);
    }

    void OnValidate()
    {
        gameObject.name = "Key_" + input.ToUpper();
    }
}
