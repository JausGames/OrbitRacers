﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int nbPlayers;
    [SerializeField] private List<Inputs.PlayerInputHandlerMenu> inputs = new List<Inputs.PlayerInputHandlerMenu>();
    [SerializeField] GameObject MatchManagerPrefab;
    [SerializeField] List<GameObject> playerTypes = new List<GameObject>();
    [SerializeField] MapHandler mapHandler;
    [SerializeField] private Text UICount;
    [SerializeField] private Text pressStart;
    [SerializeField] private Button play;
    [SerializeField] private Button quit;
    [SerializeField] private GameObject mapPicker;
    [SerializeField] private Text mapName;
    [SerializeField] private Button nextMap;
    [SerializeField] private Button previousMap;
    [SerializeField] private List<Image> playerCircle = new List<Image>();
    [SerializeField] private List<Text> playerText = new List<Text>();
    [SerializeField] private List<Image> playerPicture = new List<Image>();
    [SerializeField] private List<Button> PlayerSelect = new List<Button>();
    public Scrollbar scrollbar;


    void Start()
    {
        mapName.text = mapHandler.GetMaps()[0].GetName();
        play.onClick.AddListener(ChangeScene);
        quit.onClick.AddListener(QuitGame);

        nextMap.onClick.AddListener(NextMap);
        previousMap.onClick.AddListener(PreviousMap);

        for (int i = 0; i < playerText.Count; i++)
        {
            Debug.Log(playerText[i].text);
            playerText[i].text = playerTypes[0].name;
            //playerPicture[i].sprite = playerTypes[0].GetComponent<SpellManager>().GetPicture();
        }
        /*for (int i = 0; i < PlayerSelect.Count; i++)
        {
            Debug.Log("Debug ok : " + i);
            PlayerSelect[i].onClick.AddListener(delegate { ChangePlayer(i); });
        }*/
        PlayerSelect[0].onClick.AddListener(delegate { ChangePlayer(0); });
        PlayerSelect[1].onClick.AddListener(delegate { ChangePlayer(1); });
        PlayerSelect[2].onClick.AddListener(delegate { ChangePlayer(2); });
        PlayerSelect[3].onClick.AddListener(delegate { ChangePlayer(3); });
        PlayerSelect[4].onClick.AddListener(delegate { ChangePlayer(4); });
        PlayerSelect[5].onClick.AddListener(delegate { ChangePlayer(5); });
        PlayerSelect[6].onClick.AddListener(delegate { ChangePlayer(6); });
        PlayerSelect[7].onClick.AddListener(delegate { ChangePlayer(7); });

    }
    private void Update()
    {
        List<Inputs.PlayerInputHandlerMenu> inputLocal = new List<Inputs.PlayerInputHandlerMenu>(); 
        inputLocal.AddRange(FindObjectsOfType<Inputs.PlayerInputHandlerMenu>());
        if (inputLocal.Count >= 1)
        {
            mapPicker.SetActive(true);
            pressStart.enabled = false;
        }
        else
        {
            mapPicker.SetActive(false);
            pressStart.enabled = true;
        }
        if (inputLocal.Count >= 1) play.interactable = true;
        if (inputLocal.Count < 1) play.interactable = false;
        for (int i = 0; i < inputLocal.Count; i++)
        {
            playerText[i].enabled = true;
            playerPicture[i].enabled = true;
            PlayerSelect[2 * i + 1].gameObject.SetActive(true);
            PlayerSelect[2 * i].gameObject.SetActive(true);
        }
        for (int i = inputLocal.Count; i < 4; i++)
        {
            playerText[i].enabled = false;
            playerPicture[i].enabled = false;
            PlayerSelect[2 * i].gameObject.SetActive(false);
            PlayerSelect[2 * i + 1].gameObject.SetActive(false);
        }
        inputs.Clear();
        inputs.AddRange(inputLocal);
        for (int i = 0; i < inputs.Count; i++)
        {
            playerCircle[i].color = Color.green;
        }
        for (int i = inputs.Count; i < 4; i++)
        {
            playerCircle[i].color = Color.red;
        }
    }
    private void NextMap()
    {
        var nb = mapHandler.GetMaps().Count;
        var currMap = mapHandler.GetMapByName(mapName.text);
        var nextId = mapHandler.GetMapId(currMap) + 1;
        if (nextId < nb) mapName.text = mapHandler.GetMaps()[nextId].GetName();
        else mapName.text = mapHandler.GetMaps()[0].GetName();

    }
    private void PreviousMap()
    {
        var nb = mapHandler.GetMaps().Count;
        var currMap = mapHandler.GetMapByName(mapName.text);
        var previousId = mapHandler.GetMapId(currMap) - 1;
        if (previousId >= 0) mapName.text = mapHandler.GetMaps()[previousId].GetName();
        else mapName.text = mapHandler.GetMaps()[nb - 1].GetName();

    }

    private void ChangeScene()
    {
        if (MatchManager.instance == null) Instantiate(MatchManagerPrefab);
        var map = mapHandler.GetMapByName(mapName.text);
        var spawnwPos = map.GetPositions();
        PlayerManager.instance.SetSpawnPositions(spawnwPos);
        var list = new List<GameObject>();
        for (int i = 0; i < inputs.Count; i++)
        {
            list.Add(FindNamePlayer(playerText[i].text));
        }
        Debug.Log("ChangeScene");
        MatchManager.instance.SetPlayers(list);
        SceneManager.LoadScene(mapName.text + "Scene");
    }
    private void ChangePlayer(int nb)
    {
        Debug.Log("Debug ok : ChangePlayer, nb = " + nb);
        switch (nb)
        {
            case 0:
                if (FindNameID(playerText[0].text) == 0) return;
                playerText[0].text = FindPlayerName(playerTypes[FindNameID(playerText[0].text) - 1]);
                playerPicture[0].sprite = FindSprite(FindNameID(playerText[0].text) - 1, -1);
                break;
            case 1:
                if (FindNameID(playerText[0].text) == playerTypes.Count - 1) return;
                playerText[0].text = FindPlayerName(playerTypes[FindNameID(playerText[0].text) + 1]);
                playerPicture[0].sprite = FindSprite(FindNameID(playerText[0].text) + 1, 1);
                break;
            case 2:
                if (FindNameID(playerText[1].text) == 0) return;
                playerText[1].text = FindPlayerName(playerTypes[FindNameID(playerText[1].text) - 1]);
                playerPicture[1].sprite = FindSprite(FindNameID(playerText[1].text) - 1, -1);
                break;
            case 3:
                if (FindNameID(playerText[1].text) == playerTypes.Count - 1) return;
                playerText[1].text = FindPlayerName(playerTypes[FindNameID(playerText[1].text) + 1]);
                playerPicture[1].sprite = FindSprite(FindNameID(playerText[1].text) + 1, 1);
                break;
            case 4:
                if (FindNameID(playerText[2].text) == 0) return;
                playerText[2].text = FindPlayerName(playerTypes[FindNameID(playerText[2].text) - 1]);
                playerPicture[2].sprite = FindSprite(FindNameID(playerText[2].text) - 1, -1);
                break;
            case 5:
                if (FindNameID(playerText[2].text) == playerTypes.Count - 1) return;
                playerText[2].text = FindPlayerName(playerTypes[FindNameID(playerText[2].text) + 1]);
                playerPicture[2].sprite = FindSprite(FindNameID(playerText[2].text) + 1, 1);
                break;
            case 6:
                if (FindNameID(playerText[3].text) == 0) return;
                playerText[3].text = FindPlayerName(playerTypes[FindNameID(playerText[3].text) - 1]);
                playerPicture[3].sprite = FindSprite(FindNameID(playerText[3].text) - 1, -1);
                break;
            case 7:
                if (FindNameID(playerText[3].text) == playerTypes.Count - 1) return;
                playerText[3].text = FindPlayerName(playerTypes[FindNameID(playerText[3].text) + 1]);
                playerPicture[3].sprite = FindSprite(FindNameID(playerText[3].text) + 1, 1);
                break;
            default:
                Debug.Log("Player selection Switch Case not correct");
                break;
        }
    }
    private string FindPlayerName(GameObject player)
    {
        return player.name;
    }
   private Sprite FindSprite(int playerNb, int prevOrNext)
    {
        if (playerNb >= playerTypes.Count || playerNb <= 0) return playerTypes[playerNb - prevOrNext].GetComponent<Player>().GetPicture();
        else return playerTypes[playerNb - prevOrNext].GetComponent<Player>().GetPicture();
    }
    private GameObject FindNamePlayer(string player)
    {
        foreach(GameObject obj in playerTypes)
        {
            if (obj.name == player) return obj;
        }
        return null;
    }
    private int FindNameID(string name)
    {
        foreach(GameObject obj in playerTypes)
        {
            if (name == obj.name) return playerTypes.IndexOf(obj);
        }
        return -1;
    }
    private void QuitGame()
    {
        Application.Quit();
    }
}
