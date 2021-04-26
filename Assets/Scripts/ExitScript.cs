using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
	public List<GameObject> Ennemies;
	private void Update()
	{
		Ennemies.Clear();
		foreach (GameObject ennemy in GameObject.FindGameObjectsWithTag("Ennemy"))
		{
			Ennemies.Add(ennemy);
		}
	}
	private void Start()
	{
		foreach(GameObject ennemy in GameObject.FindGameObjectsWithTag("Ennemy"))
		{
			Ennemies.Add(ennemy);
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			if (AllEnemiesDefeated())
			{
				NextLevel();
			}
		}
	}
	bool AllEnemiesDefeated()
	{
		if(Ennemies.Count <= 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	void NextLevel()
	{
		GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelChanger>().TransitionToNextLevel();
	}
}
