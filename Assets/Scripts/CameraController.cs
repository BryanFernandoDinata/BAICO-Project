using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Cinemachine;
public class CameraController : MonoBehaviour 
{

    public Transform target;

    // public Tilemap theMap;
    // private Vector3 bottomLeftLimit;
    // private Vector3 topRightLimit;

    // private float halfHeight;
    // private float halfWidth;

    public int musicToPlay;
    private bool musicStarted;
    public CinemachineConfiner2D cinemachineVCamConfiner;
    public CinemachineVirtualCamera cmVCam;

	// Use this for initialization
	void Start () 
    {
        if(cinemachineVCamConfiner.m_BoundingShape2D == null)
        {
            //cinemachineVCamConfiner.InvalidateCache();
            cinemachineVCamConfiner.m_BoundingShape2D = ConfinerManager.instance.confiner;
        }
        
        cmVCam.m_Follow = PlayerController.instance.transform;
	}
	
	// LateUpdate is called once per frame after Update
	void LateUpdate () 
    {
        if(!musicStarted)
        {
            musicStarted = true;
            AudioManager.instance.PlayBGM(musicToPlay);
        }
	}
}
