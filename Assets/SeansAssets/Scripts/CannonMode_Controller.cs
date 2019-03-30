using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CannonMode_Controller : MonoBehaviour, ISingleplayerView
{
    public GameObject endGame;
    private PreferencesManager prefManager;
    private PlayGamesClient playGamesClient;

    public Slider horizontal_Slider;
    public Slider vertical_Slider;
    private float previousValue;

    public GameObject cannon1;
    public GameObject cannon2;
    public GameObject cannon3;

    private int numTaps;
    public GameObject cannonBallPrefab;
    
    private int powderCount;
    public TextMeshProUGUI powderCountDisplay;

    private int currentScore;  //value of current score
    public TextMeshProUGUI currentScoreDisplay;  //value diplayed in text on screen
    
    public GameObject cannon1_Display;
    public GameObject cannon2_Display;
    public GameObject cannon3_Display;

    public GameObject cannon1_selected;
    public GameObject cannon2_selected;
    public GameObject cannon3_selected;

    public Button cannon1_LoadBtn;
    public Button cannon2_LoadBtn;
    public Button cannon3_LoadBtn;
    
    public Transform cannon1_Emitter;
    public Transform cannon2_Emitter;
    public Transform cannon3_Emitter;

    public Button fireCannon_1;
    public GameObject powderDisplay_1_0;  //Powder display graphic for cannon 1 with zero powder.
    public GameObject powderDisplay_1_1;  //Powder display graphic for cannon 1 with ONE  powder.
    public GameObject powderDisplay_1_2;  //Powder display graphic for cannon 1 with TWO  powder.
    public GameObject powderDisplay_1_3;  //Powder display graphic for cannon 1 with THREE powder.

    public Button fireCannon_2;
    public GameObject powderDisplay_2_0;  //Powder display graphic for cannon 2 with zero powder.
    public GameObject powderDisplay_2_1;  //Powder display graphic for cannon 2 with ONE powder.
    public GameObject powderDisplay_2_2;  //Powder display graphic for cannon 2 with TWO powder.
    public GameObject powderDisplay_2_3;  //Powder display graphic for cannon 2 with THREE powder.

    public Button fireCannon_3;
    public GameObject powderDisplay_3_0;  //Powder display graphic for cannon 3 with zero powder.
    public GameObject powderDisplay_3_1;  //Powder display graphic for cannon 3 with ONE  powder.
    public GameObject powderDisplay_3_2;  //Powder display graphic for cannon 3 with TWO  powder.
    public GameObject powderDisplay_3_3;  //Powder display graphic for cannon 3 with THREE powder.



    //Design to take the score out of the BubbleController_Script and update the display score from here
    //private BubbleController_Script bubbleControllerScript;

    // Start is called before the first frame update
    public void Start()
    {
        powderCount = 20;
        powderRemainingDisplay();
        currentScore = 0;
        prefManager = new PreferencesManager();
        playGamesClient.AttachSingleplayerView(this);
    }

    public void UpdateScore(int score)
    {
        prefManager.UpdateHarpoonPops();
        currentScore += score;
        Debug.Log("Current score is: " + currentScore);
        currentScoreDisplay.text = currentScore.ToString();
    }


    void Update()
    {
        //Select cannons with green ring, clear any previous selections with bool on update
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {


                if (hit.transform.name == "Cannon_1")
                {
                    cannon1_selected.gameObject.SetActive(true);
                    cannon2_selected.gameObject.SetActive(false);
                    cannon3_selected.gameObject.SetActive(false);
                    cannon1_Display.gameObject.SetActive(true);
                    cannon2_Display.gameObject.SetActive(false);
                    cannon3_Display.gameObject.SetActive(false);

                }
                if (hit.transform.name == "Cannon_2")
                {
                    cannon1_selected.gameObject.SetActive(false);
                    cannon2_selected.gameObject.SetActive(true);
                    cannon3_selected.gameObject.SetActive(false);
                    cannon1_Display.gameObject.SetActive(false);
                    cannon2_Display.gameObject.SetActive(true);
                    cannon3_Display.gameObject.SetActive(false);
                }
                if (hit.transform.name == "Cannon_3")
                {
                    cannon1_selected.gameObject.SetActive(false);
                    cannon2_selected.gameObject.SetActive(false);
                    cannon3_selected.gameObject.SetActive(true);
                    cannon1_Display.gameObject.SetActive(false);
                    cannon2_Display.gameObject.SetActive(false);
                    cannon3_Display.gameObject.SetActive(true);
                }
            }
        }

    }


    void Awake_VerticalSlider()
    {
        // Assign a callback for when this slider changes
        this.vertical_Slider.onValueChanged.AddListener(this.OnVerticalSliderChanged);

        // And current value
        this.previousValue = this.vertical_Slider.value;
    }

    void OnVerticalSliderChanged(float value)
    {
        // How much we've changed
        float delta = value - this.previousValue;
        this.cannon1.transform.Rotate(Vector3.left * delta * 65);
        this.cannon2.transform.Rotate(Vector3.left * delta * 65);
        this.cannon3.transform.Rotate(Vector3.left * delta * 65);

        // Set our previous value for the next change
        this.previousValue = value;
    }

    void Awake_HorizontalSlider()
    {
        // Assign a callback for when this slider changes
        this.horizontal_Slider.onValueChanged.AddListener(this.OnHorizontalSliderChanged);

        // And current value
        this.previousValue = this.horizontal_Slider.value;
    }

    void OnHorizontalSliderChanged(float value)
    {
        // How much we've changed
        float delta = value - this.previousValue;
        this.cannon1.transform.Rotate(Vector3.up * delta * 65);
        this.cannon2.transform.Rotate(Vector3.up * delta * 65);
        this.cannon3.transform.Rotate(Vector3.up * delta * 65);
        this.previousValue = value;
    }


    void OnEnable()
    {
        //Register Button Events
        
        fireCannon_1.onClick.AddListener(() => buttonCallBack(fireCannon_1));
        fireCannon_2.onClick.AddListener(() => buttonCallBack(fireCannon_2));
        fireCannon_3.onClick.AddListener(() => buttonCallBack(fireCannon_3));
        cannon1_LoadBtn.onClick.AddListener(() => buttonCallBack(cannon1_LoadBtn));
        cannon2_LoadBtn.onClick.AddListener(() => buttonCallBack(cannon2_LoadBtn));
        cannon3_LoadBtn.onClick.AddListener(() => buttonCallBack(cannon3_LoadBtn));

    }

    void numTapsCounter()
    {
        
        if (numTaps < 3)
        {
           numTaps++;
        }
        else
            Debug.Log("Charges full!");
        
    }

    void powderRemainingDisplay()
    {
        //Displays total charges remaining on user interface
        powderCountDisplay.text = powderCount.ToString();
    }

    void buttonCallBack(Button buttonPressed)
    {
        

        if (buttonPressed == cannon1_LoadBtn)  //determining if LOAD button is pressed, if it is, number of taps will be recorded
        {
            numTapsCounter();
            powderDisplay(numTaps, buttonPressed);  //uses above function to update the powder display with the returned number taps used.
        }
        if (buttonPressed == cannon2_LoadBtn)  //determining if LOAD button is pressed, if it is, number of taps will be recorded
        {
            numTapsCounter();
            powderDisplay(numTaps, buttonPressed);  //uses above function to update the powder display with the returned number taps used. 
        }
        if (buttonPressed == cannon3_LoadBtn)  //determining if LOAD button is pressed, if it is, number of taps will be recorded
        {
            numTapsCounter();
            powderDisplay(numTaps, buttonPressed);  //uses above function to update the powder display with the returned number taps used. 
        }
        if (buttonPressed == fireCannon_1)
        {
            powderCount -= numTaps;
            powderRemainingDisplay();
            CannonBlast(numTaps);
            endGameCheck(powderCount);
            numTaps = 0;                                    //reset measures, numTaps back to 0, resets display to empty.  
            powderDisplay(numTaps, cannon1_LoadBtn);  
            
        }
        if (buttonPressed == fireCannon_2)
        {
            powderCount -= numTaps;
            powderRemainingDisplay();
            CannonBlast(numTaps);
            endGameCheck(powderCount);
            numTaps = 0;                                    //reset measures, numTaps back to 0, resets display to empty.  
            powderDisplay(numTaps, cannon2_LoadBtn);
        }
        if (buttonPressed == fireCannon_3)
        {
            powderCount -= numTaps;
            powderRemainingDisplay();
            CannonBlast(numTaps);
            endGameCheck(powderCount);
            numTaps = 0;                                     //reset measures, numTaps back to 0, resets display to empty.
            powderDisplay(numTaps, cannon3_LoadBtn);
        }

    }

    void CannonBlast(int tapCount)
    {
        int firePower = 0;
        if(tapCount == 0)
        {
            firePower = 00000;
        }
        if (tapCount == 1)
        {
            firePower = 50000;
        }
        if (tapCount == 2)
        {
            firePower = 70000;
        }
        if (tapCount == 3)
        {
            firePower = 90000;
        }
        if (fireCannon_1.gameObject.activeInHierarchy)
        {
           
            GameObject instCannonball = Instantiate(cannonBallPrefab, cannon1_Emitter.transform.position, cannon1_Emitter.transform.rotation) as GameObject;
            instCannonball.GetComponent<Rigidbody>().AddForce(cannon1_Emitter.transform.forward * firePower);
            fireCannon_1.gameObject.SetActive(false);
        }
        if (fireCannon_2.gameObject.activeInHierarchy)
        {
           
            GameObject instCannonball = Instantiate(cannonBallPrefab, cannon2_Emitter.transform.position, cannon2_Emitter.transform.rotation) as GameObject;
            instCannonball.GetComponent<Rigidbody>().AddForce(cannon2_Emitter.transform.forward * firePower);
            fireCannon_2.gameObject.SetActive(false);
        }
        if (fireCannon_3.gameObject.activeInHierarchy)
        {
            Debug.Log("FULL POWER" + firePower);
            GameObject instCannonball = Instantiate(cannonBallPrefab, cannon3_Emitter.transform.position, cannon3_Emitter.transform.rotation) as GameObject;
            instCannonball.GetComponent<Rigidbody>().AddForce(cannon3_Emitter.transform.forward * firePower);
            fireCannon_3.gameObject.SetActive(false);
        }
        if (fireCannon_1.gameObject.activeInHierarchy == false && fireCannon_2.gameObject.activeInHierarchy == false && fireCannon_3.gameObject.activeInHierarchy == false)
        {
           // Debug.Log("No cannon selected");
        }
        
    }
    void powderDisplay(int tapNum, Button btn)  //after numTaps is calculated, this function will use that information to update the powder display
    {
        if (btn == cannon1_LoadBtn)
        {
            if (tapNum == 0)
            {
                powderDisplay_1_0.SetActive(true);
                powderDisplay_1_1.SetActive(false);
                powderDisplay_1_2.SetActive(false);
                powderDisplay_1_3.SetActive(false);
            }
            if (tapNum == 1)
            {
                powderDisplay_1_0.SetActive(false);
                powderDisplay_1_1.SetActive(true);
                powderDisplay_1_2.SetActive(false);
                powderDisplay_1_3.SetActive(false);
            }
            if (tapNum == 2)
            {
                powderDisplay_1_0.SetActive(false);
                powderDisplay_1_1.SetActive(false);
                powderDisplay_1_2.SetActive(true);
                powderDisplay_1_3.SetActive(false);
            }    
            if (tapNum == 3)
            {    
                powderDisplay_1_0.SetActive(false);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_1_1.SetActive(false);
                powderDisplay_1_2.SetActive(false);
                powderDisplay_1_3.SetActive(true);
            }

            
        }
        if (btn == cannon2_LoadBtn)
        {
            if (tapNum == 0)
            {
                powderDisplay_2_0.SetActive(true);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_2_1.SetActive(false);
                powderDisplay_2_2.SetActive(false);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_2_3.SetActive(false);
            }
            if (tapNum == 1)
            {
                powderDisplay_2_0.SetActive(false);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_2_1.SetActive(true);
                powderDisplay_2_2.SetActive(false);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_2_3.SetActive(false);
            }
            if (tapNum == 2)
            {
                powderDisplay_2_0.SetActive(false);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_2_1.SetActive(false);
                powderDisplay_2_2.SetActive(true);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_2_3.SetActive(false);
            }
            if (tapNum == 3)
            {
                powderDisplay_2_0.SetActive(false);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_2_1.SetActive(false);
                powderDisplay_2_2.SetActive(false);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_2_3.SetActive(true);
            }
           
      
        }
        if (btn == cannon3_LoadBtn)
        {

            if (tapNum == 0)
            {
                powderDisplay_3_0.SetActive(true);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_3_1.SetActive(false);
                powderDisplay_3_2.SetActive(false);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_3_3.SetActive(false);
            }                 
            if (tapNum == 1)  
            {                 
                powderDisplay_3_0.SetActive(false);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_3_1.SetActive(true);
                powderDisplay_3_2.SetActive(false);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_3_3.SetActive(false);
            }                 
            if (tapNum == 2)  
            {                 
                powderDisplay_3_0.SetActive(false);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_3_1.SetActive(false);
                powderDisplay_3_2.SetActive(true);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_3_3.SetActive(false);
            }
            if (tapNum == 3)
            {
                powderDisplay_3_0.SetActive(false);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_3_1.SetActive(false);
                powderDisplay_3_2.SetActive(false);  //changes powder display by setting one graphic active and swapping it for correct 
                powderDisplay_3_3.SetActive(true);
            }

          
        }
       
    }

    void endGameCheck(int powder)
    {
        //end game mode when charges have depleted
        if (powder <= 0)
        {
            Debug.Log("Powder = " + powder + "NO MORE CHARGES!!");

            powderCount = 0;
            powderRemainingDisplay();
            //Pauses prior to ending the round so if there is a cannonball in the air, the score still registers
            endGame.SetActive(true);
            prefManager.UpdateHarpoonHighScore(currentScore);
            prefManager.UpdateGamesPlayed();
        }
    }



    void OnDisable()
    {
       
        fireCannon_1.onClick.RemoveAllListeners();
        fireCannon_2.onClick.RemoveAllListeners();
        fireCannon_3.onClick.RemoveAllListeners();
        cannon1_LoadBtn.onClick.RemoveAllListeners();
        cannon2_LoadBtn.onClick.RemoveAllListeners();
        cannon3_LoadBtn.onClick.RemoveAllListeners();

    }  
}

