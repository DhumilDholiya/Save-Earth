using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    [SerializeField]
    private Text UIscore;

    private void Update()
    {
        UIscore.text = "Score : " + (int)GameManager.Instance.score;
    }
}
