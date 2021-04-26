using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject PlayerPrefab;
    void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        Instantiate(PlayerPrefab, transform);
        int maxLevelReached = PlayerPrefs.GetInt("maxLevelReached", SceneManager.GetActiveScene().buildIndex);
        if (maxLevelReached <= SceneManager.GetActiveScene().buildIndex)
		{
            PlayerPrefs.SetInt("maxLevelReached", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.GetInt("maxLevelReached", 1);

        }
    }
}
