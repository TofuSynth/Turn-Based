using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraHandler;
    [SerializeField] private float m_cameraDistance;
    [SerializeField] float rotateSpeed;
    [SerializeField] private int damping;
    public LayerMask worldLayer;

    private void Start()
    {
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
        /*if !Paused
        {
        }
        */
        Rotation();
        ObstructionCheck();
    }

    void Rotation()
    {
        Vector3 angles = cameraHandler.transform.rotation.eulerAngles;

        float horizontal = (angles.y + 180f) % 360f - 180f;
        horizontal += Input.GetAxis("Mouse X") * rotateSpeed;

        float vertical = (angles.x + 180f) % 360f - 180f;
        vertical -= Input.GetAxis("Mouse Y") * rotateSpeed;
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
            cameraAdjustment.z += (damping * Time.deltaTime);
        }
        else if (transform.localPosition.z > m_cameraDistance)
        {
            cameraAdjustment.z -= (damping * Time.deltaTime);
        }
        transform.localPosition = cameraAdjustment;
    }
}
