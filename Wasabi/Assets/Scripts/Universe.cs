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

    private int numWormsMax;
    public int NumWormsMax { get { return numWormsMax; } private set { numWormsMax = value; } }


    bool AllSoldiersCreated = false;
    public float startTime;
    public float interval;
    private int[] characNum = new int[] { 0, 0 };
    [SerializeField] GameObject charact_mustard;
    [SerializeField] GameObject charact_wasabi;
    Vector2 mouse;
    GameObject HUD;
    private void Start()
    {
        NumWormsMax = PlayerPrefs.GetInt("nbSoldiers");
        GameObject.Find("MusicIG").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume");
        HUD = GameObject.Find("HUD");
    }
    private void Update()
    {
        //if (!(characNum[1] == numWormsMax))
        if(!AllSoldiersCreated)
        {
            CreateSoldiers();
        }
        else if (!gameStart && Time.time - interval >= 2 && AllSoldiersCreated)
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
        }
        else if(gameStart)
        {
            Timer();
            if (Time.time - startTime >= 10)
            {
                selectedPlayer.GetComponent<Char_script>().Selected = false;
                ChangePlayer();                
                
                startTime = Time.time;
            }
        }
    }

    void ChangePlayer()
    {
        int NewPlayingTeam = PlayingTeam == 1 ? 2 : 1;
        if(NewPlayingTeam == 1 && PlayingTeam == 2)
        {
            WormsToPlay++;
            if(WormsToPlay > NumWormsMax)
            {
                WormsToPlay = 1;
            }
        }
        PlayingTeam = NewPlayingTeam;
        foreach (GameObject charact in GameObject.FindGameObjectsWithTag("characters"))
        {
            if (charact.GetComponent<Char_script>().NumEquipe == PlayingTeam && charact.GetComponent<Char_script>().NumWorms == wormsToPlay)
            {
                selectedPlayer = charact;
                charact.GetComponent<Char_script>().Selected = true;
            }
        }
        if (selectedPlayer.GetComponent<Char_script>().Shoted)
        {
            selectedPlayer.GetComponent<Char_script>().Shoted = false;
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
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (turn == 1)
            {
                clone = Instantiate(charact_mustard, new Vector3(mouse.x, mouse.y + 50), transform.rotation);
            }
            else if(turn == 2)
            {
                clone = Instantiate(charact_wasabi, new Vector3(mouse.x, mouse.y + 50), transform.rotation);
            }
            interval = Time.time;
            
            
            clone.GetComponent<Char_script>().NumEquipe = turn;
            clone.GetComponent<Char_script>().NumWorms = characNum[turn - 1] + 1;
            clone.GetComponent<Char_script>().name = "Equipe " + turn + " Worms " + (characNum[turn - 1] + 1);
            characNum[turn - 1]++;
            if (turn == 2 && clone.GetComponent<Char_script>().NumWorms == numWormsMax)
            {
                AllSoldiersCreated = true;
            }
            turn = turn == 1 ? 2 : 1;
            
        }
    }

    public GameObject Selected
    {
        get { return selected; }
        set { selected = value; }
    }    
}
