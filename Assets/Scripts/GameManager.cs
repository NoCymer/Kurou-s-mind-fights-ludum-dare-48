using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject MainCameraBrain;
    public GameObject GlobalVolume;
    public GameObject Player;
    public GameObject UIManager;
    public GameObject PlayerPrefab;
    public GameObject SpawnerLocation;
    public LevelChanger LevelChanger;
    PlayerManager playerManager;
    public int NumberOfDeath = 0;
    public int LifeLeft = 4;
    void Awake()
    {
        LevelChanger = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelChanger>();
        SpawnerLocation = GameObject.FindGameObjectWithTag("Spawner");
        UIManager = GameObject.FindGameObjectWithTag("UIManager");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        MainCameraBrain = GameObject.FindGameObjectWithTag("MainCameraBrain");
        GlobalVolume = GameObject.FindGameObjectWithTag("PostProcess");
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	private void Start()
	{
        NumberOfDeath = PlayerPrefs.GetInt("NumberOfDeath", 0);
    }
	private void Update()
	{
        try
		{
            PlayerPrefs.SetInt("NumberOfDeath", NumberOfDeath);
            NumberOfDeath = PlayerPrefs.GetInt("NumberOfDeath", 0);
            SpawnerLocation = GameObject.FindGameObjectWithTag("Spawner");
            UIManager = GameObject.FindGameObjectWithTag("UIManager");
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            MainCameraBrain = GameObject.FindGameObjectWithTag("MainCameraBrain");
            GlobalVolume = GameObject.FindGameObjectWithTag("PostProcess");
            Player = GameObject.FindGameObjectWithTag("Player");
            playerManager = Player.GetComponent<PlayerManager>();
            switch (NumberOfDeath)
		    {
                case 0:
                    LifeLeft = 4;
                    playerManager.currentSanity = 100;
                    playerManager.sanityIndex = 0;
                    break;
                case 1:
                    LifeLeft = 3;
                    playerManager.currentSanity = 79;
                    playerManager.sanityIndex = 1;
                    break;
                case 2:
                    LifeLeft = 2;
                    playerManager.currentSanity = 59;
                    playerManager.sanityIndex = 2;
                    break;
                case 3:
                    LifeLeft = 1;
                    playerManager.currentSanity = 39;
                    playerManager.sanityIndex = 3;
                    break;
                case 4:
                    LifeLeft = 0;
                    playerManager.currentSanity = 19;
                    playerManager.sanityIndex = 4;
                    break;
            }
		}
        catch
		{
            return;
		}
	}
	public void Restart()
	{
        Time.timeScale = 1;
        LevelChanger.BlankTansition();
        StartCoroutine(WaitAndSpawn());
    }
    IEnumerator WaitAndSpawn()
	{
        yield return new WaitForSeconds(1);
        SpawnerLocation.GetComponent<Spawner>().Spawn();
        Time.timeScale = 1;
	}
    public void DefinitiveDeath()
	{
        Time.timeScale = 1;
        PlayerPrefs.DeleteKey("maxLevelReached");
        StartCoroutine(DeathScreen());
    }
    IEnumerator DeathScreen()
	{
        UIManager.GetComponent<UIManager>().ShowDeathScreen();
        yield return new WaitForSeconds(2);
        NumberOfDeath = 0;
        PlayerPrefs.SetInt("NumberOfDeath", 0);
        LevelChanger.TransitionToLevel(0);
	}
}
