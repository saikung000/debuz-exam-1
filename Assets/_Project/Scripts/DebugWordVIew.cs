using TMPro;
using UnityEngine;
using UniRx;

public class DebugWordVIew : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        GuessPod.Instance.correctWord.Subscribe(word => text.text = word);  
    }

  
}
