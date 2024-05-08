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
    public ReactiveCollection<Char> correctCharList = new ReactiveCollection<Char>();
    public ReactiveCollection<Char> haveCharList = new ReactiveCollection<Char>();
    public ReactiveCollection<Char> wrongCharList = new ReactiveCollection<Char>();
    public Subject<List<GuessWordState>> onCheck = new Subject<List<GuessWordState>>();
    public bool isWin = false;

    void Start()
    {
        GameManager.Instance.GetState().Where(state => state == GameState.Gameplay).Subscribe(state => StartGame());
    }

    public void StartGame()
    {
        isWin = false;
        guessCount.Value = 0;
        correctWord.Value = "";
        guessWord.Value = "";
        correctCharList.Clear();
        wrongCharList.Clear();
        RandomWord();

    }

    public void RandomWord()
    {
        List<Char> chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList();
        correctWord.Value = new string(chars.Shuffle().Take(5).ToArray());
    }

    public void AddWord(string input)
    {
        if (guessWord.Value.Length == 5) return;
        guessWord.Value = guessWord.Value + input;
    }

    public void RemoveLastWord()
    {
        if (guessWord.Value.Length == 0) return;
        string temp = guessWord.Value;
        guessWord.Value = temp.Remove(temp.Length - 1, 1);
    }

    public void Submit()
    {
        if (guessWord.Value.Length != 5) return;

        List<GuessWordState> checkLetterList = new List<GuessWordState>();
        string guessWordString = guessWord.Value;
        string correctWordString = correctWord.Value;
        for (int i = 0; i < guessWordString.Length; i++)
        {
            if (guessWordString[i] == correctWordString[i])
            {
                checkLetterList.Add(GuessWordState.Correct);
                correctCharList.Add(guessWordString[i]);
                haveCharList.Remove(guessWordString[i]);

            }
            else if (correctWordString.Contains(guessWordString[i]) && !IsHaveMatch(guessWordString[i], guessWordString, correctWordString))
            {
                checkLetterList.Add(GuessWordState.Have);
                if (!correctCharList.Contains(guessWordString[i]))
                {
                    haveCharList.Add(guessWordString[i]);
                }
            }
            else
            {
                checkLetterList.Add(GuessWordState.Wrong);
                wrongCharList.Add(guessWordString[i]);
            }
        }
        onCheck.OnNext(checkLetterList);


        if (guessCount.Value != 5)
        {
            guessCount.Value++;
            if (checkLetterList.All(state => state == GuessWordState.Correct))
            {
                isWin = true;
                GameManager.Instance.ChangeState(GameState.Result);
            }
            else
            {
                guessWord.Value = "";
            }
        }
        else
        {
            GameManager.Instance.ChangeState(GameState.Result);
        }
    }

    private bool IsHaveMatch(char charToCheck, string guessString, string correctString)
    {
        if (!guessString.Contains(charToCheck)) return false;
        for (int i = 0; i < guessString.Length; i++)
        {
            if (charToCheck == guessString[i] && guessString[i] == correctString[i])
            {
                return true;
            }
        }
        return false;
    }

}
