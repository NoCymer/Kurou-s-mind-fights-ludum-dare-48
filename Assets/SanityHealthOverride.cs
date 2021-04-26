using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityHealthOverride : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("NumberOfDeath", 0);
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.NumberOfDeath = PlayerPrefs.GetInt("NumberOfDeath", 0);
    }

}
