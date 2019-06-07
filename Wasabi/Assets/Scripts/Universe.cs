using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Universe : MonoBehaviour
{
    private GameObject selected;
    [SerializeField] Text indic;
    [SerializeField] Text tour;
    [SerializeField] Text joueur;
    private bool gameStart = false;
    public bool GameStart { get { return gameStart; } set { gameStart = value; } }
    GameObject clone;
    private int turn = 1;
    private int NbTour = 1;
    GameObject selectedPlayer;

    private int playingTeam;
    public int PlayingTeam { get { return playingTeam; } set { playingTeam = value; } }

    private int wormsToPlay;
    public int WormsToPlay { get { return wormsToPlay; } set { wormsToPlay = value; } }

    private int numWormsMax;
    public int NumWormsMax { get { return numWormsMax; } private set { numWormsMax = value; } }

    private float timeBeforeChange;
    public float TimeBeforeChange { get { return timeBeforeChange; } set { timeBeforeChange = value; } }

    bool AllSoldiersCreated = false;
    public float startTime;
    private float interval;
    private int[] characNum = new int[] { 0, 0 };
    [SerializeField] GameObject charact_ketchup;
    [SerializeField] GameObject charact_wasabi;
    Vector2 mouse;
    GameObject HUD;
    private void Start()
    {
        //Récupération du nombre de soldat et du volume de la musique de la scène précèdente
        NumWormsMax = PlayerPrefs.GetInt("nbSoldiers");
        GameObject.Find("MusicIG").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume");
        HUD = GameObject.Find("HUD");
        indic.text = "Joueur " + turn + "\nCliquez pour poser votre soldat " + (characNum[turn - 1] + 1);
        tour.text = "Phase de déploiement des unités";
    }
    private void Update()
    {
        if(!AllSoldiersCreated)
        {
            joueur.text = "Joueur " + turn;
            indic.text = "Veuillez patienter\n" + Math.Round( (interval - Time.time + 1), 2) + " s"; 
            CreateSoldiers();
        }
        else if (!gameStart && AllSoldiersCreated)
        {
            indic.text = "Veuillez patienter\n" + Math.Round((interval - Time.time + 2), 2) + " s";
            if (Time.time - interval >= 2)
            {
                //réactivation des textes
                HUD.GetComponentInChildren<HealthDisplay>().healthText.enabled = true;
                HUD.GetComponentInChildren<HealthDisplay>().infoEnnemy.enabled = true;
                indic.enabled = false;

                PlayingTeam = 1;
                wormsToPlay = 1;
                TimeBeforeChange = 20;
                gameStart = true;
                //Démarrage du timer du joueur
                startTime = Time.time;
                tour.text = "Début du tour " + NbTour;
                foreach(GameObject character1 in  GameObject.FindGameObjectsWithTag("characters"))
                {
                    foreach (GameObject character2 in GameObject.FindGameObjectsWithTag("characters"))
                    {
                        Physics2D.IgnoreCollision(character1.GetComponent<CapsuleCollider2D>(), character2.GetComponent<CapsuleCollider2D>());
                    }
                }

                foreach (GameObject charact in GameObject.FindGameObjectsWithTag("characters"))
                {
                    if (charact.GetComponent<Char_script>().NumEquipe == PlayingTeam && charact.GetComponent<Char_script>().NumWorms == wormsToPlay)
                    {
                        selectedPlayer = charact;
                        charact.GetComponent<Char_script>().Selected = true;
                        charact.GetComponent<SoldatoControl>().Selected = true;
                    }
                }
                Timer();
                //Met la caméra en vue centrée sur le soldat actuel
                Camera.main.GetComponent<CameraFollow>().Target = selectedPlayer.transform;
                Camera.main.orthographicSize = 15;
                Char_script first = selectedPlayer.GetComponent<Char_script>();
                GameObject.Find("HUD").GetComponentInChildren<HealthDisplay>().SetText(first.NumEquipe, first.NumWorms, first.Hp, first.HpMax);
                
            }            
        }
        else if(gameStart)
        {
            if (Time.time - startTime >= 10)
            {
                selectedPlayer.GetComponent<Char_script>().Selected = false;
                selectedPlayer.GetComponent<SoldatoControl>().Selected = false;
                ChangePlayer();                
                //Redémarrage du timer
                startTime = Time.time;
            }
            Timer();
        }
        //Debug.Log(1.0f/Time.smoothDeltaTime);
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
                NbTour++;
                tour.text = "Début du tour " + NbTour;
            }
        }
        PlayingTeam = NewPlayingTeam;
        foreach (GameObject charact in GameObject.FindGameObjectsWithTag("characters"))
        {
            if (charact.GetComponent<Char_script>().NumEquipe == PlayingTeam && charact.GetComponent<Char_script>().NumWorms == wormsToPlay)
            {
                selectedPlayer = charact;
                charact.GetComponent<Char_script>().Selected = true;
                charact.GetComponent<SoldatoControl>().Selected = true;
            }
        }
        if (selectedPlayer.GetComponent<Char_script>().Shoted)
        {
            selectedPlayer.GetComponent<Char_script>().Shoted = false;
            selectedPlayer.GetComponent<Char_script>().Create = false;
        }
    }

    public void Timer()
    {
         HUD.GetComponentInChildren<HealthDisplay>().SetTime(Time.time - startTime);
    }

    void CreateSoldiers()
    {
        if (Time.time - interval >= 1)
        {
            //Met la caméra en vue globale
            Camera.main.GetComponent<CameraFollow>().Start();
            indic.text = "Joueur " + turn + "\nCliquez pour poser votre soldat " + (characNum[turn - 1] + 1);
            if (Input.GetMouseButton(0))
            {
                //Récupère position de la souris selon la scène
                mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (turn == 1)
                {
                    clone = Instantiate(charact_ketchup, new Vector3(mouse.x, 1), transform.rotation);
                }
                else if(turn == 2)
                {
                    clone = Instantiate(charact_wasabi, new Vector3(mouse.x, 1), transform.rotation);
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

                //Met la caméra en vue proche sur le personnage créé
                Camera.main.GetComponent<CameraFollow>().Target = clone.transform;
                Camera.main.orthographicSize = 25;
            }
        }
    }

    void RemoveSoldier (GameObject sender)
    {
        Debug.Log("EFFECTUÉ");
    }

    public GameObject Selected
    {
        get { return selected; }
        set { selected = value; }
    }    
}
