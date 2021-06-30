using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMode : GameMode
{
    [SerializeField] private uint maxDoor;
    [SerializeField] private uint[] playersDoor;
    [SerializeField] private uint nbLap = 1;
    [SerializeField] private uint[] playersLap;
    [SerializeField] private Player winner;

    private void Awake()
    {
        mode = GetModes()[0];
    }
    public void SetLapNumber(int max)
    {
        nbLap = (uint) Mathf.Abs(max);
    }
    public void SetMaxDoor(uint max)
    {
        maxDoor = max;
    }
    override public void ResetGame()
    {
        playersDoor = new uint[4] { 0, 0, 0, 0 };
        playersLap = new uint[4] { 0, 0, 0, 0 };
    }

    public void PlayerPassingDoor(uint nbDoor, Player player)
    {
        var playerId = PlayerManager.instance.GetPlayerId(player);
        if (playersDoor[playerId] == nbDoor - 1)
        {
            playersDoor[playerId] = nbDoor;
        }

        if (nbDoor == 0 && playersDoor[playerId] == maxDoor)
        {
            playersDoor[playerId] = nbDoor;
            playersLap[playerId]++;
            if (playersLap[playerId] == nbLap) { winner = player; MatchManager.instance.PlayerWin(player, "Chrono" + "\n" + "Bien joué le boss"); }
        }
    }
    public override void PlayFireworks(ParticleSystem particle)
    {
        particle.transform.position = winner.transform.position;
        base.PlayFireworks(particle);
    }
}
