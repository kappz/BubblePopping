using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerMed : MonoBehaviour, ISingleplayerView
{

    private PreferencesManager prefManager;
    private PlayGamesClient playGamesClient;

    public AudioSource batterySound;
    public GameObject cannonx;
    public GameObject cannony;
    public GameObject slider;
    public GameObject insect;
    public GameObject explosion;
    public GameObject eBall;
    public GameObject scoreText;

    private bool touchStatus = false;
    private float numOfCharges;

    RadialBarScript sliderScript;
    SystemSpawn spawning;
    InputDetection inputDetection;
    GScore score;
    ProtonFire protonFireX;
    ProtonFire protonFireY;

    void Start()
    {
        prefManager = new PreferencesManager();

        spawning = new SystemSpawn();
        inputDetection = new InputDetection();
        score = new GScore(0);
 
        sliderScript = (RadialBarScript)slider.GetComponent(typeof(RadialBarScript));
        protonFireX = (ProtonFire)cannonx.GetComponent(typeof(ProtonFire));
        protonFireY = (ProtonFire)cannony.GetComponent(typeof(ProtonFire));

        playGamesClient.AttachSingleplayerView(this);
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

                        score.UpdateScore(distance, numOfCharges);

                        prefManager.UpdateBubblesPopped();
                    }
                    else
                    {
                        Instantiate(eBall, new Vector3(inputDetection.GetX(), inputDetection.GetY(), -0.3f), Quaternion.identity);
                        protonFireY.StartDelay();
                    }

                    scoreText.GetComponent<Text>().text = "Score: " + score.GetScore();
                    sliderScript.Reset();
                } //end if inputDetection

            }// end touchCount
        } // end if sliderScript.GetCurrentAmout() > 32

        if (Time.timeScale <= 0.0f)
        {
            prefManager.UpdateBubblePopHighScore(score.GetScore());
            prefManager.UpdateGamesPlayed();
        }
    }

    public bool GetTouchStatus()
    {
        return touchStatus;
    }

    public void DefaultTouchStatus()
    {
        touchStatus = false;
    }

}

