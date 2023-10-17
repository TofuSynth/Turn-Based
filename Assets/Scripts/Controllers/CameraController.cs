using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Services;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] GameObject cameraHandler;
    [SerializeField] private float m_cameraDistance;
    [SerializeField] float m_rotateSpeed;
    [SerializeField] private int m_damping;
    [SerializeField] private LayerMask worldLayer;
    private GameStateService m_gameStateService;

    private void Start()
    {
        m_gameStateService = ServiceLocator.GetService<GameStateService>();
        CursorSetup();
    }

    void CursorSetup()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        cameraHandler.transform.Rotate(0, 0, 0);
    }

    private void Update()
    {
        if (m_gameStateService.GetState() == GameStateService.GameState.Normal)
        {
            Rotation();
        }
        ObstructionCheck();
    }

    void Rotation()
    {
        Vector3 angles = cameraHandler.transform.rotation.eulerAngles;

        float horizontal = (angles.y + 180f) % 360f - 180f;
        horizontal += Input.GetAxis("Mouse X") * m_rotateSpeed;

        float vertical = (angles.x + 180f) % 360f - 180f;
        vertical -= Input.GetAxis("Mouse Y") * m_rotateSpeed;
        vertical = Mathf.Clamp(vertical, -5f, 89f);
            
        cameraHandler.transform.rotation = Quaternion.Euler(new Vector3(vertical, horizontal, angles.z));
        cameraHandler.transform.position = player.transform.position;
    }
    
    void ObstructionCheck()
    {
        Ray cameraObstruction = new Ray(player.transform.position,transform.position - player.transform.position);
        Vector3 cameraToPlayer = player.transform.position - transform.position;
        Vector3 cameraAdjustment = transform.localPosition;
        if (Physics.Raycast(cameraObstruction, cameraToPlayer.magnitude - 1, worldLayer))
        {
            cameraAdjustment.z += (m_damping * Time.deltaTime);
        }
        else if (transform.localPosition.z > m_cameraDistance)
        {
            cameraAdjustment.z -= (m_damping * Time.deltaTime);
        }
        transform.localPosition = cameraAdjustment;
    }
}
