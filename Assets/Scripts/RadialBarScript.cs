using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialBarScript : MonoBehaviour
{

    public AudioSource chargingSound;
    public GameObject cannony;
    public Transform ChargingBar;
    public Transform TextIndicator;

    ProtonFire protonFireY;
    [SerializeField] private float currentAmount;
    [SerializeField] private float speed;
    // Use this for initialization
    void Start()
    {
        protonFireY = (ProtonFire)cannony.GetComponent(typeof(ProtonFire));
    }

    // Update is called once per frame
    void Update()
    {
        if (protonFireY.GetDelay() >= 3.0f)
        {

            if (currentAmount == 0)
            {
                chargingSound.Play();
            }
            else if (currentAmount > 99)
            {
                chargingSound.Stop();
            }

            if (currentAmount < 33)
            {
                ChargingBar.GetComponent<Image>().color = new Color32(57, 62, 236, 255);
            }
            else if (currentAmount >= 33 && currentAmount < 66)
            {
                ChargingBar.GetComponent<Image>().color = new Color32(13, 255, 116, 255);
            }
            else if (currentAmount >= 66 && currentAmount < 100)
            {
                ChargingBar.GetComponent<Image>().color = new Color32(248, 226, 29, 255);
            }
            else
                ChargingBar.GetComponent<Image>().color = new Color32(248, 53, 29, 255);

            //if (protonFireY.GetDelay() >= 3.0f) {
            if (currentAmount < 100)
            {
                currentAmount += speed * Time.deltaTime;
                TextIndicator.GetComponent<Text>().text = ((int)currentAmount).ToString() + "%";
            }
            //}

            ChargingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
        }
    }

    public void Reset()
    {
        currentAmount = 0;
    }

    public float GetCurrentAmount()
    {
        return currentAmount;
    }
}
