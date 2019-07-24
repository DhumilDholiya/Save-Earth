using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool isPowerdUp;

    [SerializeField]
    private GameObject powerUpModel;
    [SerializeField]
    private GameObject normalModel;
  //  [SerializeField]
  //  private GameObject earthPowerTrail;

    private void OnEnable()
    {
        PowerButton.OnUsePowerButton += UsePowerUpModel;
        isPowerdUp = false;
        PowerButton.OnPoweredTimeUp += PowerTimeUp;
    }
    private void OnDisable()
    {
        PowerButton.OnUsePowerButton -= UsePowerUpModel;
        PowerButton.OnPoweredTimeUp -= PowerTimeUp;
    }
    private void UsePowerUpModel()
    {
        isPowerdUp = true;
        normalModel.SetActive(false);
        powerUpModel.SetActive(true);
       // earthPowerTrail.SetActive(true);
    }

    private void PowerTimeUp()
    {
        isPowerdUp = false;
        normalModel.SetActive(true);
        powerUpModel.SetActive(false);
     //   earthPowerTrail.SetActive(false);
    }
}
