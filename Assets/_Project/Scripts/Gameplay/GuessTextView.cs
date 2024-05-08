using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GuessTextView : MonoBehaviour
{
    public int row;
    public int index;
    public TextMeshProUGUI letterText;
    [SerializeField] private Image backgroundImage;
    private ReactiveProperty<GuessWordState> keyboardState = new ReactiveProperty<GuessWordState>(GuessWordState.Normal);
    [SerializeField] private SerializedDictionary<GuessWordState, Color> colorDict = new SerializedDictionary<GuessWordState, Color>();
    private GuessPod guessPod;

    void Start()
    {
        guessPod = GuessPod.Instance;
        keyboardState.Subscribe(state => backgroundImage.color = colorDict[state]);
        guessPod.guessCount.Subscribe(row =>
        {
            if (this.row >= row)
            {
                letterText.text = "";
                keyboardState.Value = GuessWordState.Normal;
            }
        });
        guessPod.guessWord.Where(x => guessPod.guessCount.Value == row).Subscribe(guessWord =>
        {
            if (guessWord.Length > index)
            {
                letterText.text = guessWord[index].ToString();
            }
            else
            {
                letterText.text = "";
            }
        });

        guessPod.onCheck.Where(x => guessPod.guessCount.Value == row).Subscribe(resultList =>
        {
            keyboardState.Value = resultList[index];
        });
    }

}
