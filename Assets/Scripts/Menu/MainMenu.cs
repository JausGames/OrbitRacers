using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int nbPlayers;
    private bool inTeam = true;
    [SerializeField] private List<Inputs.PlayerInputHandlerMenu> inputs = new List<Inputs.PlayerInputHandlerMenu>();
    [SerializeField] GameObject MatchManagerPrefab;
    [SerializeField] List<GameObject> playerTypes = new List<GameObject>();
    [SerializeField] List<GameObject> menus = new List<GameObject>();
    [SerializeField] MapHandler mapHandler;
    [SerializeField] private Text UICount;
    [SerializeField] private Text pressStart;
    [SerializeField] private Button next;
    [SerializeField] private Button play;
    [SerializeField] private Button back;
    [SerializeField] private Button quit;
    [SerializeField] private GameObject mapPicker;
    [SerializeField] private Text mapName;
    [SerializeField] private Text title;
    [SerializeField] private Button nextMap;
    [SerializeField] private Button previousMap;
    [SerializeField] private GameObject modePicker;
    [SerializeField] private Text gameModeName;
    [SerializeField] private Button nextMode;
    [SerializeField] private Button previousMode;
    [SerializeField] private Image mapPicture;
    [SerializeField] private List<Image> playerCircle = new List<Image>();
    [SerializeField] private List<Text> playerText = new List<Text>();
    [SerializeField] private List<Image> playerPicture = new List<Image>();
    [SerializeField] private List<Slider> playerColor = new List<Slider>();
    [SerializeField] private Color[] colors;
    [SerializeField] private GameObject teamSubMenu;
    private Color[] teamColors = new Color[] { new Color(0.5f, 0.5f, 1f, 1f), new Color(1f, 0.5f, 0.5f, 1f) };
    int[,] teamPlayer = new int[,] { { 1, 1 }, { 1, 2 }, { 2, 1 }, { 2, 2 } };
    [SerializeField] private Button playInTeam;
    [SerializeField] private int[] numTeam = new int[] { 1, 1, 2, 2 };
    [SerializeField] private List<Button> PlayerSelect = new List<Button>();
    [SerializeField] private List<Button> TeamPicker = new List<Button>();
    [SerializeField] private List<InputField> TeamName = new List<InputField>();
    [SerializeField] private List<Slider> teamColorSliders = new List<Slider>();


    void Start()
    {
        PlayInTeam();
        for(int nbButton = 0; nbButton < TeamPicker.Count; nbButton++)
        {
            TeamPicker[nbButton].gameObject.SetActive(false);
            SetColorToBtn(TeamPicker[nbButton], teamColors[numTeam[nbButton] - 1]);
        }
        DontDestroyOnLoad(this.gameObject);
        ChangeMenu(0);
        mapName.text = mapHandler.GetMaps()[0].GetName();
        gameModeName.text = GameMode.GetModes()[0];
        play.onClick.AddListener(ChangeScene);
        quit.onClick.AddListener(QuitGame);
        next.onClick.AddListener(delegate { ChangeMenu(1); });
        back.onClick.AddListener(delegate { ChangeMenu(0); });
        playInTeam.onClick.AddListener(PlayInTeam);

        nextMap.onClick.AddListener(NextMap);
        previousMap.onClick.AddListener(PreviousMap);

        nextMode.onClick.AddListener(NextMode);
        previousMode.onClick.AddListener(PreviousMode);

        for (int i = 0; i < playerText.Count; i++)
        {
            Debug.Log(playerText[i].text);
            playerText[i].text = playerTypes[0].name;
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

        playerColor[0].onValueChanged.AddListener(delegate { ChangePlayerColor(0, playerColor[0].value); });
        playerColor[1].onValueChanged.AddListener(delegate { ChangePlayerColor(1, playerColor[1].value); });
        playerColor[2].onValueChanged.AddListener(delegate { ChangePlayerColor(2, playerColor[2].value); });
        playerColor[3].onValueChanged.AddListener(delegate { ChangePlayerColor(3, playerColor[3].value); });


        teamColorSliders[0].onValueChanged.AddListener(delegate { ChangeTeamColor(0, teamColorSliders[0].value); });
        teamColorSliders[1].onValueChanged.AddListener(delegate { ChangeTeamColor(1, teamColorSliders[1].value); });


        TeamPicker[0].onClick.AddListener(delegate { SwithTeam(0); });
        TeamPicker[1].onClick.AddListener(delegate { SwithTeam(1); });
        TeamPicker[2].onClick.AddListener(delegate { SwithTeam(2); });
        TeamPicker[3].onClick.AddListener(delegate { SwithTeam(3); });

        ChangePlayerColor(0, 0f);
        ChangePlayerColor(1, 0.25f);
        ChangePlayerColor(2, 0.5f);
        ChangePlayerColor(3, 0.75f);

        ChangeTeamColor(0, 0f);
        ChangeTeamColor(1, 0.6667f);
    }

    private void PlayInTeam()
    {
        inTeam = !inTeam;
        DisplayTeamButton(inTeam);
        var color = playInTeam.colors.normalColor;
        if (inTeam)
        {
            playInTeam.GetComponentInChildren<Text>().text = "Yes";

            color = Color.HSVToRGB(0.3f, 0.5f, 1f);
        }
        else
        {
            playInTeam.GetComponentInChildren<Text>().text = "No";
            color = Color.HSVToRGB(0f, 0.5f, 1f);
        }
        SetColorToBtn(playInTeam, color);
    }
    private void DisplayTeamButton(bool value)
    {

        teamSubMenu.SetActive(value);
        if (value)
        {
            for (int i = 0; i < nbPlayers; i++)
            {
                TeamPicker[i].gameObject.SetActive(value);
                TeamPicker[i].GetComponentInChildren<Text>().text = playerText[i].text;
            }
            for (int i = nbPlayers; i < 4; i++)
            {
                TeamPicker[i].gameObject.SetActive(false);
            }
        }
    }

    private void SwithTeam(int nbButton)
    {
        var team = numTeam[nbButton];
        if (team == 1)
        {
            numTeam[nbButton] = 2;
        }
        else
        {
            numTeam[nbButton] = 1;
        }
        SetColorToBtn(TeamPicker[nbButton], teamColors[numTeam[nbButton] - 1]);
    }

    public void ChangeMenu(int menuOrder)
    {
        foreach(GameObject menu in menus)
        {
            menu.SetActive(false);
        }
        title.gameObject.SetActive(true);
        menus[menuOrder].SetActive(true);
    }
    public void DisplayMenu(bool value)
    {
        if(!value)
        foreach (GameObject menu in menus)
        {
            menu.SetActive(value);
        }
        else menus[menus.Count - 1].SetActive(value);

        title.gameObject.SetActive(value);
        this.enabled = value;
    }

    private void Update()
    {
        List<Inputs.PlayerInputHandlerMenu> inputLocal = new List<Inputs.PlayerInputHandlerMenu>(); 
        inputLocal.AddRange(FindObjectsOfType<Inputs.PlayerInputHandlerMenu>());
        if (inputLocal.Count >= 1)
        {
            mapPicture.enabled = true;
            modePicker.SetActive(true);
            mapPicker.SetActive(true);
            pressStart.enabled = false;
        }
        else
        {
            modePicker.SetActive(false);
            mapPicker.SetActive(false);
            pressStart.enabled = true;
        }
        nbPlayers = inputLocal.Count;
        if (inputLocal.Count >= 1) play.interactable = true;
        if (inputLocal.Count < 1) play.interactable = false;
        for (int i = 0; i < inputLocal.Count; i++)
        {
            playerColor[i].gameObject.SetActive(true);
            playerText[i].enabled = true;
            playerPicture[i].enabled = true;
            PlayerSelect[2 * i + 1].gameObject.SetActive(true);
            PlayerSelect[2 * i].gameObject.SetActive(true);
        }
        for (int i = inputLocal.Count; i < 4; i++)
        {
            playerColor[i].gameObject.SetActive(false);
            playerText[i].enabled = false;
            playerPicture[i].enabled = false;
            PlayerSelect[2 * i].gameObject.SetActive(false);
            PlayerSelect[2 * i + 1].gameObject.SetActive(false);
        }
        inputs.Clear();
        inputs.AddRange(inputLocal);
        
    }
    private void NextMap()
    {
        var nb = mapHandler.GetMaps().Count;
        var currMap = mapHandler.GetMapByName(mapName.text);
        var nextId = mapHandler.GetMapId(currMap) + 1;
        if (nextId < nb) mapName.text = mapHandler.GetMaps()[nextId].GetName();
        else mapName.text = mapHandler.GetMaps()[0].GetName();

        if (!mapHandler.GetMapByName(mapName.text).FindInModes(gameModeName.text)) NextMap();

        mapPicture.sprite = mapHandler.GetMapByName(mapName.text).GetPicture();
    }
    private void PreviousMap()
    {
        var nb = mapHandler.GetMaps().Count;
        var currMap = mapHandler.GetMapByName(mapName.text);
        var previousId = mapHandler.GetMapId(currMap) - 1;
        if (previousId >= 0) mapName.text = mapHandler.GetMaps()[previousId].GetName();
        else mapName.text = mapHandler.GetMaps()[nb - 1].GetName();

        if (!mapHandler.GetMapByName(mapName.text).FindInModes(gameModeName.text)) PreviousMap();

        mapPicture.sprite = mapHandler.GetMapByName(mapName.text).GetPicture();
    }
    private void NextMode()
    {
        var nb = GameMode.GetModes().Count;
        var currId = GameMode.GetIdByName(gameModeName.text);
        var nextId = currId + 1;
        if (nextId < nb) gameModeName.text = GameMode.GetModes()[nextId];
        else gameModeName.text = GameMode.GetModes()[0];

        if (!mapHandler.GetMapByName(mapName.text).FindInModes(gameModeName.text) )NextMap();

    }
    private void PreviousMode()
    {
        var nb = GameMode.GetModes().Count;
        var currId = GameMode.GetIdByName(gameModeName.text);
        var previousId = currId - 1;
        if (previousId >= 0) gameModeName.text = GameMode.GetModes()[previousId];
        else gameModeName.text = GameMode.GetModes()[nb - 1];

        if (!mapHandler.GetMapByName(mapName.text).FindInModes(gameModeName.text)) NextMap();

    }
    private void ChangePlayerColor(int playerNb, float value)
    {
        colors[playerNb] = Color.HSVToRGB(value, 1f, 1f);
        ActualizeColor(playerNb);
    }
    private void ChangeTeamColor(int teamNb, float value)
    {
        teamColors[teamNb] = Color.HSVToRGB(value, 0.5f, 1f);
        ActualizeTeamColor(teamNb);
    }
    private void SetColorToBtn(Button btn, Color color)
    {
        var colorBlock = btn.colors;
        var h = 0f;
        var s = 0f;
        var v = 0f;
        Color.RGBToHSV(color, out h, out s, out v);
        colorBlock.normalColor = color;
        colorBlock.highlightedColor = Color.HSVToRGB(h, s, 3f / 4f * v);
        colorBlock.selectedColor = Color.HSVToRGB(h, s, 1f/2f * v);
        colorBlock.pressedColor = Color.HSVToRGB(h, s, 1f / 4f * v); ;
        btn.colors = colorBlock;
    }
    private void SetColorToInput(InputField input, Color color)
    {
        var colorBlock = input.colors;
        var h = 0f;
        var s = 0f;
        var v = 0f;
        Color.RGBToHSV(color, out h, out s, out v);
        colorBlock.normalColor = color;
        colorBlock.highlightedColor = Color.HSVToRGB(h, s, 3f / 4f * v);
        colorBlock.selectedColor = Color.HSVToRGB(h, s, 1f / 2f * v);
        colorBlock.pressedColor = Color.HSVToRGB(h, s, 1f / 4f * v); ;
        input.colors = colorBlock;
    }
    private void ActualizeColor(int playerNb)
    {
        playerPicture[playerNb].sprite = SpriteMaker.GetInstance().ColorSaturateSprite(playerPicture[playerNb].sprite, colors[playerNb], FilterMode.Bilinear);
        playerColor[playerNb].GetComponentInChildren<Image>().color = colors[playerNb];
    }
    private void ActualizeTeamColor(int teamNb)
    {
        for (int i = 0; i < nbPlayers; i++)
        {
            if (numTeam[i] - 1 == teamNb) SetColorToBtn(TeamPicker[i], teamColors[teamNb]);
        }
        SetColorToInput(TeamName[teamNb], teamColors[teamNb]);
        teamColorSliders[teamNb].GetComponentInChildren<Image>().color = teamColors[teamNb];
    }

    private void ChangeScene()
    {
        var map = mapHandler.GetMapByName(mapName.text);
        var spawnwPos = new Vector3[4];
        for (int i = 0; i < map.GetPositions().Length; i++)
        {
            spawnwPos[i] = new Vector3(map.GetPositions()[i].x, map.GetPositions()[i].y, map.GetPositions()[i].z);
        }
        if (playInTeam)
        {
            var teamACount = 0;
            var teamBCount = 0;


            for (int i = 0; i < nbPlayers; i++)
            {
                if (numTeam[i] == 1)
                {
                    teamACount++;
                    teamPlayer[1, teamACount] = i;
                }
                if (numTeam[i] == 2)
                {
                    teamBCount++;
                    teamPlayer[2, teamBCount] = i;
                }
            }

            if (teamACount == 0 || teamBCount == 0
                || teamACount == 3 || teamBCount == 3) return;

            var oldSpawnPos = new Vector3[4];
            for (int i = 0; i < spawnwPos.Length; i++)
            {
                oldSpawnPos[i] = new Vector3(map.GetPositions()[i].x, map.GetPositions()[i].y, map.GetPositions()[i].z);
            }
            spawnwPos[0] = oldSpawnPos[teamPlayer[1, 1]];
            spawnwPos[1] = oldSpawnPos[teamPlayer[2, 1]];
            if (nbPlayers >= 3) spawnwPos[2] = oldSpawnPos[teamPlayer[1, 2]];
            
            if (nbPlayers == 4) spawnwPos[3] = oldSpawnPos[teamPlayer[2, 2]];

            Debug.Log("MainMenu, ChangeScene : spawnPos[0] = " + spawnwPos[0]);

        }
        DisplayMenu(false);
        if (MatchManager.instance == null) Instantiate(MatchManagerPrefab);
        Type type = Type.GetType(GameMode.GetMode(gameModeName.text));
        GameMode mode = null;

        if (type == typeof(RaceMode))
        {
            mode = MatchManager.instance.gameObject.AddComponent(typeof(RaceMode)) as RaceMode;
            var race = mode.GetComponent<RaceMode>();
            race.SetMaxDoor((uint)(int)map.gameObject.GetComponent<RaceMap>().GetDoors().Length / 2 - 1);
        }
        PlayerManager.instance.SetColors(colors);
        PlayerManager.instance.SetSpawnPositions(spawnwPos);
        var list = new List<GameObject>();
        for (int i = 0; i < inputs.Count; i++)
        {
            list.Add(FindNamePlayer(playerText[i].text));
        }
        Debug.Log("ChangeScene");
        MatchManager.instance.SetPlayers(list);
        SceneManager.LoadScene(mapName.text + "Scene");
        if (type == typeof(SoccerMode))
        {
            mode = MatchManager.instance.gameObject.AddComponent(typeof(SoccerMode)) as SoccerMode;
            var soccer = mode.GetComponent<SoccerMode>();
            soccer.SetTeamGame(inTeam);
            soccer.SetTeamArray(numTeam);
            if (inTeam)
            {
                soccer.SetTeamColors(teamColors);
                Debug.Log("MainMenu, ChangeScene : Team A : " + TeamName[0].text);
                Debug.Log("MainMenu, ChangeScene : Team B : " + TeamName[1].text);
                soccer.SetTeamNames(new string[] { TeamName[0].text, TeamName[1].text });
            }
            soccer.SetUI();
            FindObjectOfType<SoccerMap>().SetMode(soccer);
            //soccer.SetGoals(map.gameObject.GetComponent<SoccerMap>().GetGoals());
        }

        mode.ResetGame();
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
                ActualizeColor(0);
                break;
            case 1:
                if (FindNameID(playerText[0].text) == playerTypes.Count - 1) return;
                playerText[0].text = FindPlayerName(playerTypes[FindNameID(playerText[0].text) + 1]);
                playerPicture[0].sprite = FindSprite(FindNameID(playerText[0].text) + 1, 1);
                ActualizeColor(0);
                break;
            case 2:
                if (FindNameID(playerText[1].text) == 0) return;
                playerText[1].text = FindPlayerName(playerTypes[FindNameID(playerText[1].text) - 1]);
                playerPicture[1].sprite = FindSprite(FindNameID(playerText[1].text) - 1, -1);
                ActualizeColor(1);
                break;
            case 3:
                if (FindNameID(playerText[1].text) == playerTypes.Count - 1) return;
                playerText[1].text = FindPlayerName(playerTypes[FindNameID(playerText[1].text) + 1]);
                playerPicture[1].sprite = FindSprite(FindNameID(playerText[1].text) + 1, 1);
                ActualizeColor(1);
                break;
            case 4:
                if (FindNameID(playerText[2].text) == 0) return;
                playerText[2].text = FindPlayerName(playerTypes[FindNameID(playerText[2].text) - 1]);
                playerPicture[2].sprite = FindSprite(FindNameID(playerText[2].text) - 1, -1);
                ActualizeColor(2);
                break;
            case 5:
                if (FindNameID(playerText[2].text) == playerTypes.Count - 1) return;
                playerText[2].text = FindPlayerName(playerTypes[FindNameID(playerText[2].text) + 1]);
                playerPicture[2].sprite = FindSprite(FindNameID(playerText[2].text) + 1, 1);
                ActualizeColor(2);
                break;
            case 6:
                if (FindNameID(playerText[3].text) == 0) return;
                playerText[3].text = FindPlayerName(playerTypes[FindNameID(playerText[3].text) - 1]);
                playerPicture[3].sprite = FindSprite(FindNameID(playerText[3].text) - 1, -1);
                ActualizeColor(3);
                break;
            case 7:
                if (FindNameID(playerText[3].text) == playerTypes.Count - 1) return;
                playerText[3].text = FindPlayerName(playerTypes[FindNameID(playerText[3].text) + 1]);
                playerPicture[3].sprite = FindSprite(FindNameID(playerText[3].text) + 1, 1);
                ActualizeColor(3);
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
