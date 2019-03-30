using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameControllerMult : MonoBehaviour, IMultiplayerView
{

    private bool startGame;
    private PreferencesManager prefManager;
    private PlayGamesClient playGamesClient;

    public AudioSource batterySound;
    public GameObject cannonx;
    public GameObject cannony;
    public GameObject slider;
    public GameObject insect;
    public GameObject explosion;
    public GameObject eBall;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI oScore;

    private bool touchStatus = false;
    private float numOfCharges;

    RadialBarScript sliderScript;
    SystemSpawn spawning;
    InputDetection inputDetection;
    MultScore score;
    ProtonFire protonFireX;
    ProtonFire protonFireY;

    void Start()
    {
        startGame = false;
        playGamesClient = PlayGamesClient.GetInstance();
        playGamesClient.AttachMultiplayerView(this);
    }

    void Update()
    {

        if ((GameObject.FindGameObjectWithTag("Bug") == null) && protonFireY.GetDelay() >= 3.0f)
        {
            spawning.SpawnBug(insect);
        }

        if (sliderScript.GetCurrentAmount() >= 33 && protonFireY.GetDelay() >= 3.0f)
        {
            numOfCharges = sliderScript.GetCurrentAmount();

            if (Input.touchCount == 2)
            {

                if (inputDetection.UserTouches(Input.GetTouch(0), Input.GetTouch(1)))
                {
                    batterySound.Stop();
                    float distance;
                    touchStatus = true;
                    inputDetection.CannonPosition(cannonx, cannony);
                    GameObject go = GameObject.FindGameObjectWithTag("Bug");
                    distance = inputDetection.Distance(go);

                    if (distance >= 0 && distance <= 1.05f)
                    {

                        protonFireY.ResetCounter();
                        protonFireX.ResetCounter();
                        protonFireY.StartDelay();

                        playGamesClient.OnScoreUpdate(score.UpdateScore(distance, numOfCharges));
                    }
                    else
                    {
                        Instantiate(eBall, new Vector3(inputDetection.GetX(), inputDetection.GetY(), -0.3f), Quaternion.identity);
                        protonFireY.StartDelay();
                    }

                    sliderScript.Reset();
                } //end if inputDetection

            }// end touchCount
        } // end if sliderScript.GetCurrentAmout() > 32
    }

    public bool GetTouchStatus()
    {
        return touchStatus;
    }

    public void DefaultTouchStatus()
    {
        touchStatus = false;
    }

    public void GameStart()
    {

        startGame = true;
        prefManager = new PreferencesManager();

        spawning = new SystemSpawn();
        inputDetection = new InputDetection();
        score = new MultScore();

        sliderScript = (RadialBarScript)slider.GetComponent(typeof(RadialBarScript));
        protonFireX = (ProtonFire)cannonx.GetComponent(typeof(ProtonFire));
        protonFireY = (ProtonFire)cannony.GetComponent(typeof(ProtonFire));
    }

    public void StartGame()
    {
        GameStart();
    }

    public void updatePlayerScore(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "You : " + score;
    }
     
    public void ExitGame()
    {
       // SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }
    
    public void updateOpponentScore(int score)
    {
        oScore.GetComponent<TextMeshProUGUI>().text = "Opponent : " + score;
    }

}