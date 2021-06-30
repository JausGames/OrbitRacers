using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chronometer : MonoBehaviour
{
    public enum Mode
    {
        Chrono,
        Countdown
    }

    [SerializeField] Text chronoUI;
    [SerializeField] float startTime;
    [SerializeField] float gameTime;
    [SerializeField] float currentTime;
    [SerializeField] bool inGame;
    [SerializeField] bool overtime;
    [SerializeField] Mode mode;
    private bool pause;

    private void Awake()
    {
        ResetChrono();
    }
    private void Update()
    {
        if (inGame && !overtime && !pause) SetCurrentTime();
    }
    public void ResetChrono()
    {
        SetOvertime(false);
        startTime = Time.time;
        if (mode == Mode.Chrono) currentTime = 0f;
        else currentTime = gameTime;
        UpdateDisplay();
    }
    public void ShowHideChrono(bool value)
    {
        chronoUI.enabled = value;
    }

    private void SetCurrentTime()
    {
        if (mode == Mode.Chrono) currentTime += Time.deltaTime;
        else currentTime -= Time.deltaTime;
        UpdateDisplay();
    }
    private void UpdateDisplay()
    {
       
        var unit = Mathf.Floor(currentTime);
        if (unit < 60)
        {
            var textUnit = unit.ToString();
            if (Check1Digit((int)unit)) textUnit = "0" + textUnit;

            var floating = Mathf.Floor((currentTime - unit) * 100f);
            var textFloat = floating.ToString();
            if (Check1Digit((int)floating)) textFloat = "0" + textFloat;

            chronoUI.text = textUnit + ":" + textFloat;
        }
        else
        {
            var minutes = unit % 60f;
            minutes = (unit - minutes) / 60f;
            unit -= minutes;
            var textUnit = unit.ToString();
            if (Check1Digit((int)unit)) textUnit = "0" + textUnit;

            var floating = Mathf.Floor((currentTime - unit) * 100f);
            var textFloat = floating.ToString();
            if (Check1Digit((int)floating)) textFloat = "0" + textFloat;

            chronoUI.text = minutes + ":" + textUnit + ":" + textFloat;

        }
    }
    public void SetInGame(bool inGame, bool resetTimer)
    {
        if (resetTimer) ResetChrono();
        this.inGame = inGame;
        ShowHideChrono(inGame);
    }
    public void SetInGame(bool inGame, bool resetTimer, bool hide)
    {
        if (resetTimer) ResetChrono();
        if (hide) ShowHideChrono(inGame);
        this.inGame = inGame;
        
    }

    public void SetPause(bool pause)
    {
        this.pause = pause;
    }

    private bool Check1Digit(int value)
    {
        if (value < 10) return true;
        return false;
    }
    public string GetDisplayedChrono()
    {
        return chronoUI.text;
    }
    public float GetCurrentTime()
    {
        return currentTime;
    }
    public void SetMode(Mode mode)
    {
        this.mode = mode;
    }
    public void SetOvertime(bool value)
    {
        overtime = value;
        if (value)
        {
            currentTime = Time.time + 1000f;
            chronoUI.text = "overtime";
        }
    }
    public void SetGameTime(float gameTime)
    {
        this.gameTime = gameTime;
        currentTime = gameTime;
    }


}
