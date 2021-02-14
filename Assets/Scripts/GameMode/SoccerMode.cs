using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoccerMode : GameMode
{
    private bool ballSet = false;
    private bool overTime = false;
    [SerializeField] private int[] teamArray = new int[4];
    [SerializeField] private bool teamGame = false;
    [SerializeField] private uint leftTeamScore;
    [SerializeField] private uint rightTeamScore;
    [SerializeField] private string[] teamNames = new string[2] { "Home Team", "Guest Team" };
    [SerializeField] private Color[] teamColors = new Color[] { new Color(0.5f, 0.5f, 1f, 1f), new Color(1f, 0.5f, 0.5f, 1f) };
    private float gameTime = 60f;
    [SerializeField] private float timeLeft = 60f;
    [SerializeField] private bool inGame = false;
    [SerializeField] private bool gameOver = false;
    [SerializeField] private List<Text> texts = new List<Text>();
    [SerializeField] private GameObject ball;
    [SerializeField] private float waitTime = 3f;

    private void Start()
    {
        mode = GetModes()[1];
        timeLeft = gameTime;
        Debug.Log("PlayerCount SoccerMode : " + PlayerManager.instance.GetPlayers().Count);
        //SetUI();
        CreateBall();
    }

    private void Update()
    {
        if (gameOver) return;
        if (timeLeft <= 0f) EndGame();
        if (waitTime > 0f && !inGame)
        {
            waitTime -= Time.deltaTime;
        }
        else if (waitTime <= 0f && !inGame && !ballSet)
        {
            ball.GetComponent<MassicObject>().SetInteractables(NBodySimulation.Bodies);
            ballSet = true;
            waitTime = 3f;
        }
        else if (waitTime <= 0f && !inGame)
        {
            RestartKickoff();
            ResetBallPosition();
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            waitTime = 3f;
        }

        if (inGame)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft > 10f) texts[0].text = Mathf.CeilToInt(timeLeft).ToString("00");
            else if (timeLeft <= 10f && timeLeft > 0f) texts[0].text = (Mathf.CeilToInt(timeLeft * 10f) / 10f).ToString("0.0");
            else texts[0].text = "Game Over";
            waitTime = 4f;

        }
        if (overTime && texts[0].text != "Overtime") texts[0].text = "Overtime";
    }

    private void CreateBall()
    {
        Debug.Log("SoccerMode, CreateBall : enter");
        ball = new GameObject("ball", typeof(SpriteRenderer), typeof(CircleCollider2D), typeof(Rigidbody2D), typeof(MassicObject), typeof(TrailRenderer));
        ball.transform.SetParent(transform);
        ball.transform.position = Vector3.zero;
        ball.transform.localScale = Vector3.one * 0.35f;
        ball.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/ball") as Sprite;
        var trail = ball.GetComponent<TrailRenderer>();
        trail.time = 0.8f;
        trail.endWidth = 0f;
        trail.material = Resources.Load<Material>("Materials/trail") as Material;
        trail.endColor = new Color(0f, 0f, 0f, 0f);
        var massic = ball.GetComponent<MassicObject>();
        massic.SetRadius();
        massic.SetGravity(0.003f);
        massic.SetSize(CelestialObject.Size.Object);
        var col = ball.GetComponent<CircleCollider2D>();
        col.radius = 5f;
        col.sharedMaterial = Resources.Load<PhysicsMaterial2D>("Physics/ballPhysic") as PhysicsMaterial2D;
        var body = ball.GetComponent<Rigidbody2D>();
        body.gravityScale = 0f;
        body.drag = 0.2f;

        ball.layer = 11;

    }
    public void SetTeamArray(int[] array)
    {
        teamArray = array;
    }
    public void RestartKickoff()
    {
        MatchManager.instance.ResetGame();
    }
    public void SetTeamNames(string[] names)
    {
        teamNames = names;
    }
    public void SetTeamColors(Color[] colors)
    {
        teamColors = colors;
    }

    public void SetUI()
    {
        Debug.Log("SoccerMode, SetUI");
        Canvas canvas = (Canvas)FindObjectOfType(typeof(Canvas));

        GameObject timer = new GameObject("timerSoccer", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
        GameObject leftName = new GameObject("leftName", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
        GameObject rightName = new GameObject("rightName", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
        GameObject leftScore = new GameObject("leftScore", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
        GameObject rightScore = new GameObject("rightScore", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));



        texts.Add(timer.GetComponent<Text>());
        texts.Add(leftName.GetComponent<Text>());
        texts.Add(rightName.GetComponent<Text>());
        texts.Add(leftScore.GetComponent<Text>());
        texts.Add(rightScore.GetComponent<Text>());

        timer.transform.SetParent(canvas.transform);
        leftName.transform.SetParent(canvas.transform);
        rightName.transform.SetParent(canvas.transform);
        leftScore.transform.SetParent(canvas.transform);
        rightScore.transform.SetParent(canvas.transform);

        Font thin = Resources.Load<Font>("Fonts/Roboto-Thin") as Font;
        Font regular = Resources.Load<Font>("Fonts/Roboto-Regular") as Font;


        var rect = timer.GetComponent<RectTransform>();

        rect.anchorMax = new Vector2(0.667f, 1);
        rect.anchorMin = new Vector2(0.333f, 0.85f);
        rect.offsetMax = new Vector2(50, 50);
        rect.offsetMin = new Vector2(50, 50);
        rect.anchoredPosition = new Vector2(0, 0);

        rect = leftName.GetComponent<RectTransform>();

        rect.anchorMax = new Vector2(0.333f, 1);
        rect.anchorMin = new Vector2(0, 0.96f);
        rect.offsetMax = new Vector2(10, 10);
        rect.offsetMin = new Vector2(10, 10);
        rect.anchoredPosition = new Vector2(0, 0);

        rect = rightName.GetComponent<RectTransform>();

        rect.anchorMax = new Vector2(1, 1);
        rect.anchorMin = new Vector2(0.667f, 0.96f);
        rect.offsetMax = new Vector2(10, 10);
        rect.offsetMin = new Vector2(10, 10);
        rect.anchoredPosition = new Vector2(0, 0);

        rect = leftScore.GetComponent<RectTransform>();

        rect.anchorMax = new Vector2(0.333f, 0.96f);
        rect.anchorMin = new Vector2(0, 0.85f);
        rect.offsetMax = new Vector2(0, 10);
        rect.offsetMin = new Vector2(50, 10);
        rect.anchoredPosition = new Vector2(0, 0);

        rect = rightScore.GetComponent<RectTransform>();

        rect.anchorMax = new Vector2(1, 0.96f);
        rect.anchorMin = new Vector2(0.667f, 0.85f);
        rect.offsetMax = new Vector2(0, 10);
        rect.offsetMin = new Vector2(50, 10);
        rect.anchoredPosition = new Vector2(0, 0);


        foreach (Text txt in texts)
        {
            txt.font = thin;
            txt.resizeTextMaxSize = 200;
            txt.resizeTextForBestFit = true;
            txt.alignment = TextAnchor.UpperCenter;
        }
        texts[1].font = regular;
        texts[2].font = regular;
        texts[1].text = teamNames[0];
        texts[2].text = teamNames[1];
        texts[3].text = leftTeamScore.ToString();
        texts[4].text = rightTeamScore.ToString();

        if (teamGame)
        {

            texts[1].color = teamColors[0];
            texts[3].color = teamColors[0];
            texts[2].color = teamColors[1];
            texts[4].color = teamColors[1];

            var p0 = PlayerManager.instance.GetPlayerById(0);
            var p1 = PlayerManager.instance.GetPlayerById(1);
            var p2 = PlayerManager.instance.GetPlayerById(2);
            var p3 = PlayerManager.instance.GetPlayerById(3);


            var p0Jersey = new GameObject("p0Jersey", typeof(SpriteRenderer));
            p0Jersey.transform.SetParent(p0.transform);
            p0Jersey.transform.position = p0.transform.position;
            var sprite = p0Jersey.GetComponent<SpriteRenderer>();
            sprite.sprite = Resources.Load<Sprite>("Sprites/jersey") as Sprite;
            if (teamArray[0] == 1) sprite.color = new Color(teamColors[0].r, teamColors[0].g, teamColors[0].b, 0.7f);
            else sprite.color = new Color(teamColors[1].r, teamColors[1].g, teamColors[1].b, 0.7f);
            sprite.sortingOrder = 2;

            if (p1 == null) return;

            var p1Jersey = new GameObject("p1Jersey", typeof(SpriteRenderer));
            p1Jersey.transform.SetParent(p1.transform);
            p1Jersey.transform.position = p1.transform.position;
            sprite = p1Jersey.GetComponent<SpriteRenderer>();
            sprite.sprite = Resources.Load<Sprite>("Sprites/jersey") as Sprite;
            if (teamArray[1] == 1) sprite.color = new Color(teamColors[0].r, teamColors[0].g, teamColors[0].b, 0.7f);
            else sprite.color = new Color(teamColors[1].r, teamColors[1].g, teamColors[1].b, 0.7f);
            sprite.sortingOrder = 2;

            if (p2 == null) return;

            var p2Jersey = new GameObject("p2Jersey", typeof(SpriteRenderer));
            p2Jersey.transform.SetParent(p2.transform);
            p2Jersey.transform.position = p2.transform.position;
            sprite = p2Jersey.GetComponent<SpriteRenderer>();
            sprite.sprite = Resources.Load<Sprite>("Sprites/jersey") as Sprite;
            if (teamArray[2] == 1) sprite.color = new Color(teamColors[0].r, teamColors[0].g, teamColors[0].b, 0.7f);
            else sprite.color = new Color(teamColors[1].r, teamColors[1].g, teamColors[1].b, 0.7f);
            sprite.sortingOrder = 2;

            if (p3 == null) return;

            var p3Jersey = new GameObject("p3Jersey", typeof(SpriteRenderer));
            p3Jersey.transform.SetParent(p3.transform);
            p3Jersey.transform.position = p3.transform.position;
            sprite = p3Jersey.GetComponent<SpriteRenderer>();
            sprite.sprite = Resources.Load<Sprite>("Sprites/jersey") as Sprite;
            if (teamArray[3] == 1) sprite.color = new Color(teamColors[0].r, teamColors[0].g, teamColors[0].b, 0.7f);
            else sprite.color = new Color(teamColors[1].r, teamColors[1].g, teamColors[1].b, 0.7f);
            sprite.sortingOrder = 2;
        }
        else
        {
            texts[1].color = PlayerManager.instance.GetPlayerById(0).controller.GetColor();
            texts[3].color = PlayerManager.instance.GetPlayerById(0).controller.GetColor();
            texts[1].text = PlayerManager.instance.GetPlayerById(0).GetName();
            if (PlayerManager.instance.GetPlayerById(1) == null) return;
            texts[2].color = PlayerManager.instance.GetPlayerById(1).controller.GetColor();
            texts[4].color = PlayerManager.instance.GetPlayerById(1).controller.GetColor();
            texts[2].text = PlayerManager.instance.GetPlayerById(1).GetName();
        }

    }
    public void SetTeamGame(bool value)
    {
        teamGame = value;
    }


    override public void ResetGame()
    {
        Debug.Log("SoccerMode, ResetGame");
        leftTeamScore = 0;
        rightTeamScore = 0;
        overTime = false;
        UpdateScore();
        timeLeft = gameTime;
        if (texts.Count != 0) texts[0].text = Mathf.CeilToInt(timeLeft).ToString();
    }
    public void ResetBallPosition()
    {
        ball.transform.position = Vector3.zero;
        ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        ball.GetComponent<TrailRenderer>().Clear();
    }
    public void DisplayUI(bool value)
    {
        foreach(Text txt in texts)
        {
            txt.enabled = value;
        }
    }
    public bool GetInGame()
    {
        return inGame;
    }
    public void UpdateScore()
    {
        Debug.Log("SoccerMode, UpdateScore");
        if (texts[3] != null && texts[4] != null)
        texts[3].text = leftTeamScore.ToString();
        texts[4].text = rightTeamScore.ToString();
    }
    public void Score(bool isLeft)
    {
        Debug.Log("SoccerMode, Score");
        if (isLeft) leftTeamScore++;
        else rightTeamScore++;
        inGame = false;
        waitTime = 2f;
        ball.GetComponent<Rigidbody2D>().velocity = ball.GetComponent<Rigidbody2D>().velocity / 4f;
        UpdateScore();
        if (overTime) EndGame();
        else
        {
            MatchManager.instance.GetFireworks().transform.position = ball.transform.position;
            MatchManager.instance.GetFireworks().Play();
        }
    }
    public void EndGame()
    {
        Debug.Log("SoccerMode, EndGame");
        DisplayUI(false);
        inGame = false;
        waitTime = 3f;
        gameOver = true;
        GetWinner();
    }
    private void GetWinner()
    {
        if (!teamGame)
        {
            if (leftTeamScore > rightTeamScore) MatchManager.instance.PlayerWin(PlayerManager.instance.GetPlayerById(0));
            else if (leftTeamScore < rightTeamScore) MatchManager.instance.PlayerWin(PlayerManager.instance.GetPlayerById(1));
            else StartOvertime();
        }
        else
        {
            if (leftTeamScore > rightTeamScore) MatchManager.instance.TeamWin(teamNames[0], teamColors[0]);
            else if (leftTeamScore < rightTeamScore) MatchManager.instance.TeamWin(teamNames[1], teamColors[1]);
            else StartOvertime();
        }
    }
    override public void StartGame()
    {
        Debug.Log("SoccerMode, StarGame");
        gameOver = false;
        inGame = true;
        waitTime = 0f;
        ResetBallPosition();
        DisplayUI(true);
    }
    public void StartOvertime()
    {
        overTime = true;
        StartGame();
        texts[0].text = "Overtime";
        inGame = false;
        timeLeft = 20000f;
        ball.GetComponent<Rigidbody2D>().velocity = ball.GetComponent<Rigidbody2D>().velocity / 4f;
    }
    public override void PlayFireworks(ParticleSystem particle)
    {
        particle.transform.position = ball.transform.position;
        base.PlayFireworks(particle);
    }
    public void SetGameTime(float time)
    {
        gameTime = time;
        timeLeft = gameTime;
    }
}
