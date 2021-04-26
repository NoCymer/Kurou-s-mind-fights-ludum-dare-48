using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	public GameObject Canvas;
	public GameManager gameManager;
	public Image fill;
	Ennemy ennemy;
	private void Start()
	{
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		ennemy = GetComponent<Ennemy>();
	}
	void Update()
    {
		float HPAmount = (float)ennemy.currentHealth / 100;
		fill.fillAmount = HPAmount;
		if (ennemy.currentHealth == 0)
		{
			Canvas.SetActive(false);
		} 
    }
}
