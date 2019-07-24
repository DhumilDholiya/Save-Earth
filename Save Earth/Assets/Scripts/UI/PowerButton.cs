using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerButton : MonoBehaviour
{
    public static event Action OnUsePowerButton = delegate { };
    public static event Action OnPoweredTimeUp = delegate { };

    public static bool isPowerButtonActive;

    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private Button powerButton;
    [SerializeField]
    private float poweredUpTime = 3f;
    [SerializeField]
    private Animator anim;

    private bool isPoweredUp = false;
    private float poweredUpTimer;

    private float powerFill;
    private PowerCube[] powerCubes;

    private void Start()
    {
        powerFill = 0f;
    }
    private void OnEnable()
    {
        poweredUpTimer = 0f;
        CollisionDetection.OnCollectingCube += OnCollectingPowerCube;
    }
    private void OnDisable()
    {
        CollisionDetection.OnCollectingCube -= OnCollectingPowerCube;
    }
    private void Update()
    {
        if(fillImage.fillAmount == 1)
        {
            powerButton.interactable = true;
            isPowerButtonActive = true;
            anim.SetBool("IsPowerActive", true);
        }
        else
        {
            powerButton.interactable = false;
            isPowerButtonActive = false;
            anim.SetBool("IsPowerActive", false);
        }
        if (isPoweredUp)
        {
            poweredUpTimer += Time.deltaTime;
            if (poweredUpTimer > poweredUpTime)
            {
                OnPoweredTimeUp();
                poweredUpTimer = 0f;
                isPoweredUp = false;
            }
            if(poweredUpTimer > poweredUpTime - 0.5f)
            {
                Debug.Log("fading");
                anim.SetBool("PowerFading",true);
            }
        }
        if(!isPoweredUp)
        {
            Debug.Log("faded complete.");
            anim.SetBool("PowerFading", false);
        }
    }

    private void OnCollectingPowerCube()
    {
        //powerfill increment.
        powerFill += 0.13f;
        Mathf.Clamp01(powerFill);
        fillImage.fillAmount = powerFill;
    }

    public void OnPowerButtonClick()
    {
        fillImage.fillAmount = 0;
        powerFill = 0f;
        isPoweredUp = true;

        //make callback to player script and use cool effect.
        OnUsePowerButton();
    }
}
