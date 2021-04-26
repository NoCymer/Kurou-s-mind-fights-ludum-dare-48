using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Image HP;
    public Image Sanity;
	public GameObject HPCanvas;
	public GameObject SanityCanvas;
	GameObject Player;
	GameManager gameManager;
	PlayerManager PlayerManager;
	public Sprite Heart;
	public Sprite HeartBroken;
	public Image Heart1;
	public Image Heart2;
	public Image Heart3;
	public Image Heart4;
	public GameObject Menu;
	public GameObject Options;
	public GameObject DieMenu;
	public GameObject PauseMenu;
	public bool IsPaused;
	private void Start()
	{
	}
	void Update()
    {
		try
		{
			gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
			Player = gameManager.Player;
			PlayerManager = Player.GetComponent<PlayerManager>();
			float HPAmount = (float)PlayerManager.currentHealth / 100;
			HP.fillAmount = HPAmount;

			float SanityAmount = (float)PlayerManager.currentSanity / 100;
			Sanity.fillAmount = SanityAmount;

			if (Input.GetKeyDown(KeyCode.Escape) && !IsPaused)
			{
				Pause();
			}
			else if (Input.GetKeyDown(KeyCode.Escape) && IsPaused)
			{
				Resume();
			}
			UpdateHeart();
		}
		catch
		{
			return;
		}

	}
	public void Pause()
	{
		PauseMenu.SetActive(true);
		Time.timeScale = 0;
		HPCanvas.SetActive(false);
		SanityCanvas.SetActive(false);
		IsPaused = true;
	}
	public void Resume()
	{
		Time.timeScale = 1;
		HPCanvas.SetActive(true);
		SanityCanvas.SetActive(true);
		IsPaused = false;
		ShowMenu();
		PauseMenu.SetActive(false);

	}
	public void UpdateHeart()
	{
		switch(PlayerManager.sanityIndex)
		{
			case 0:
				Heart1.sprite = Heart;
				Heart2.sprite= Heart;
				Heart3.sprite = Heart;
				Heart4.sprite = Heart;
				break;
			case 1:
				Heart1.sprite = HeartBroken;
				Heart2.sprite = Heart;
				Heart3.sprite = Heart;
				Heart4.sprite = Heart;
				break;
			case 2:
				Heart1.sprite = HeartBroken;
				Heart2.sprite = HeartBroken;
				Heart3.sprite = Heart;
				Heart4.sprite = Heart;
				break;
			case 3:
				Heart1.sprite = HeartBroken;
				Heart2.sprite = HeartBroken;
				Heart3.sprite = HeartBroken;
				Heart4.sprite = Heart;
				break;
			case 4:
				Heart1.sprite = HeartBroken;
				Heart2.sprite = HeartBroken;
				Heart3.sprite = HeartBroken;
				Heart4.sprite = HeartBroken;
				break;
		}
	}
	public void ShowMenu()
	{
		Menu.SetActive(true);
		Options.SetActive(false);
	}
	public void ShowOption()
	{
		Menu.SetActive(false);
		Options.SetActive(true);
	}
	public void Quit()
	{
		Application.Quit();
	}
	public void ShowDeathScreen()
	{
		HPCanvas.SetActive(false);
		SanityCanvas.SetActive(false);
		DieMenu.SetActive(true);
	}
	public void MainMenu()
	{
		gameManager.LevelChanger.TransitionToLevel(0);
	}
}
