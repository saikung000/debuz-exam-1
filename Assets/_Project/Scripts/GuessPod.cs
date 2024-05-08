using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

public class GuessPod : MonoSingleton<GuessPod>
{
    public ReactiveProperty<int> guessCount = new ReactiveProperty<int>(0);
    public ReactiveProperty<string> correctWord = new ReactiveProperty<string>("");
    public ReactiveProperty<string> guessWord = new ReactiveProperty<string>("");
    public ReactiveCollection<string> correctChar = new ReactiveCollection<string>();
    public ReactiveCollection<string> wrongChar = new ReactiveCollection<string>();

    void Start()
    {
        GameManager.Instance.GetState().Where(state => state == GameState.Gameplay).Subscribe(state => StartGame());
    }

    public void StartGame()
    {
        guessCount.Value = 0;
        correctWord.Value = "";
        guessWord.Value = "";
        correctChar.Clear();
        wrongChar.Clear();
        RandomWord();

    }

    public void RandomWord()
    {
        List<Char> chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList();
        correctWord.Value = new string(chars.Shuffle().Take(5).ToArray());
        Debug.Log(correctWord.Value);
    }

    public void AddWord(string input)
    {
        if(guessWord.Value.Length == 5) return;
        guessWord.Value = guessWord.Value + input;
        Debug.Log(guessWord.Value);
    }

    public void RemoveLastWord()
    {
        if(guessWord.Value.Length == 0) return;
        string temp = guessWord.Value;
        guessWord.Value = temp.Remove(temp.Length - 1, 1);
        Debug.Log(guessWord.Value);
    }

    public void Submit()
    {
        
    }
}
