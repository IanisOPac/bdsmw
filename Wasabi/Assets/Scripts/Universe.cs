using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Universe : MonoBehaviour
{
    private GameObject selected;
    private bool gameStart = false;    
    public bool GameStart { get { return gameStart; } set { gameStart = value; } }
    GameObject clone;
    private int turn = 1;
    GameObject selectedPlayer;

    private int playingTeam;
    public int PlayingTeam { get { return playingTeam; } set { playingTeam = value; } }

    private int wormsToPlay;
    public int WormsToPlay { get { return wormsToPlay; } set { wormsToPlay = value; } }

    private int numWormsMax = 2;
    public int NumWormsMax { get { return numWormsMax; } private set { numWormsMax = value; } }

    public float startTime;
    public float interval;
    private int[] characNum = new int[] { 0,0 };
    [SerializeField] GameObject charact;
    Vector2 mouse;
    GameObject HUD;
    private void Start()
    {
        //interval = Time.time;
        HUD = GameObject.Find("HUD");
    }
    private void Update()
    {
        if (!(characNum[1] == numWormsMax))
        {
            CreateSoldiers();
        }
        else if (!gameStart && Time.time - interval >= 2)
        {
            PlayingTeam = 1;
            wormsToPlay = 1;
            gameStart = true;
            startTime = Time.time;
            foreach (GameObject charact in GameObject.FindGameObjectsWithTag("characters"))
            {
                if(charact.GetComponent<Char_script>().NumEquipe == PlayingTeam && charact.GetComponent<Char_script>().NumWorms == wormsToPlay)
                {
                    selectedPlayer = charact;
                    charact.GetComponent<Char_script>().Selected = true;
                }
            }
            Timer();
            Camera.main.GetComponent<CameraFollow>().Target = selectedPlayer.transform;
            Camera.main.orthographicSize = 15;
            Char_script first = selectedPlayer.GetComponent<Char_script>();                
            GameObject.Find("HUD").GetComponentInChildren<HealthDisplay>().SetText(first.NumEquipe, first.NumWorms, first.Hp, first.HpMax);
            //charact.GetComponent<Char_script>().startTime = Time.time;
        }
        else if(gameStart)
        {
            Timer();
            if (Time.time - startTime >= 10)
            {
                selectedPlayer.GetComponent<Char_script>().Selected = false;
                
                GameObject.Find("HUD").GetComponentInChildren<HealthDisplay>().SetText(selectedPlayer.GetComponent<Char_script>().NumEquipe, selectedPlayer.GetComponent<Char_script>().NumWorms, selectedPlayer.GetComponent<Char_script>().Hp, selectedPlayer.GetComponent<Char_script>().HpMax);
                startTime = Time.time;
            }
        }
    }

    public void Timer()
   {
       HUD.GetComponentInChildren<HealthDisplay>().SetTime(Time.time - startTime);
   }

    void CreateSoldiers()
    {
        if (Input.GetMouseButton(0) && Time.time - interval >= 1)
        {
            interval = Time.time;
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clone = Instantiate(charact, new Vector3(mouse.x, mouse.y + 50), transform.rotation);
            clone.GetComponent<Char_script>().NumEquipe = turn;
            clone.GetComponent<Char_script>().NumWorms = characNum[turn - 1] + 1;
            clone.GetComponent<Char_script>().name = "Equipe " + turn + " Worms " + (characNum[turn - 1] + 1);
            characNum[turn - 1]++;
            turn = turn == 1 ? 2 : 1;
        }
    }

    public GameObject Selected
    {
        get { return selected; }
        set { selected = value; }
    }    
}
