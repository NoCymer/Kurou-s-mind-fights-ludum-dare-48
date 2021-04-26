using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
	public GameObject Menu;
	public GameObject Options;
	public GameObject Levels;
	public Button lvlOne;
	public Button lvlTwo;
	public Button lvlThree;
	public Button lvlFour;
	public Button lvlFive;
	public LevelChanger levelChanger;
	private void Update()
	{
		Time.timeScale = 1;
		switch (PlayerPrefs.GetInt("maxLevelReached", 69))
		{
			case 1:
				lvlOne.interactable = true;
				break;
			case 2:
				lvlOne.interactable = true;
				lvlTwo.interactable = true;
				break;
			case 3:
				lvlOne.interactable = true;
				lvlTwo.interactable = true;
				lvlThree.interactable = true;
				break;
			case 4:
				lvlOne.interactable = true;
				lvlTwo.interactable = true;
				lvlThree.interactable = true;
				lvlFour.interactable = true;
				break;
			default:
				break;
		}
	}
	public void ShowMenu()
	{
		Menu.SetActive(true);
		Options.SetActive(false);
		Levels.SetActive(false);
	}
	public void ShowOption()
	{
		Menu.SetActive(false);
		Options.SetActive(true);
		Levels.SetActive(false);
	}
	public void ShowLevels()
	{
		Menu.SetActive(false);
		Options.SetActive(false);
		Levels.SetActive(true);
	}
	public void Quit()
	{
		Application.Quit();
	}

	public void LoadLevel1()
	{
		Time.timeScale = 1;
		levelChanger.TransitionToLevel(1);
	}
	public void LoadLevel2()
	{
		Time.timeScale = 1;
		levelChanger.TransitionToLevel(2);
	}
	public void LoadLevel3()
	{
		Time.timeScale = 1;
		levelChanger.TransitionToLevel(3);
	}
	public void LoadLevel4()
	{
		Time.timeScale = 1;
		levelChanger.TransitionToLevel(4);
	}
	public void Resume()
	{
		Time.timeScale = 1;
		levelChanger.TransitionToLevel(PlayerPrefs.GetInt("maxLevelReached", 1));
	}
}
