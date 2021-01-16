using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [SerializeField] GameObject UI = null;
    [SerializeField] GameObject canvas = null;
    [SerializeField] GameObject playAgainUI = null;
    [SerializeField] GameObject timerUI = null;
    [SerializeField] PlayAgain playAgain = null;
    [SerializeField] int nbPlayers = 0;
    [SerializeField] ParticleSystem fireworks;

    #region Singleton
    public static MatchManager instance;
    public static PlayerManager playerManager;

    private void Awake()
    {
        instance = this;
        playerManager = GetComponentInChildren<PlayerManager>();
        UI = transform.Find("UI").gameObject;
        canvas = UI.transform.Find("Canvas").gameObject;
        timerUI = canvas.transform.Find("Timer").gameObject;
        playAgainUI = canvas.transform.Find("PlayAgain").gameObject;
        playAgain = playAgainUI.GetComponent<PlayAgain>();
    }

    #endregion


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        timerUI.GetComponent<Timer>().SetManager(this);
        playerManager.SetMatchUp();
        timerUI.SetActive(true);
        var list = FindObjectsOfType<Inputs.PlayerInputHandler>();
        foreach (Inputs.PlayerInputHandler input in list)
        {
            input.FindPlayer();
        }
    }
    public ParticleSystem GetFireworks()
    {
        return fireworks;
    }

    public void StartGame()
    {
        timerUI.SetActive(false);
        playerManager.SetCanMove(true);
        GetComponent<GameMode>().StartGame();
    }
    public void ResetGame()
    {
        playAgain.playAgain = false;
        playerManager.ResetMatchUp();
        timerUI.SetActive(true);
        if (GetComponent<SoccerMode>())
        {
            GetComponent<SoccerMode>().ResetBallPosition();
            GetComponent<SoccerMode>().DisplayUI(true);
        }
    }
    public void SetPlayers(List<GameObject> players)
    {
        nbPlayers = players.Count;
        PlayerManager.instance.SpawnPlayers(players);
    }
    public void DeletePlayers()
    {
        PlayerManager.instance.DeletePlayers();
    }
    public void PlayerWin(Player player)
    {
        playAgain.SetWinnerName(player.GetName(), player.controller.GetColor());
        playAgainUI.SetActive(true);
        GetComponent<GameMode>().ResetGame();
        GetComponent<GameMode>().PlayFireworks(fireworks);
    }
    public void TeamWin(string teamName, Color teamColor)
    {
        playAgain.SetWinnerName(teamName, teamColor);
        playAgainUI.SetActive(true);
        GetComponent<GameMode>().ResetGame();
        GetComponent<GameMode>().PlayFireworks(fireworks);
    }


}
