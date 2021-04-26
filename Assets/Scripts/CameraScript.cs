using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    CinemachineVirtualCamera VC;
    Transform player;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        VC = GetComponent<CinemachineVirtualCamera>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        try
		{
            player = gameManager.Player.GetComponent<Transform>();
            VC.Follow = player;
            VC.LookAt = player;
		}
        catch
		{
            return;
		}
    }
}
