using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Services;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ControlsService m_controlsService;
    [SerializeField] private int m_speed;
    private Rigidbody m_playerRigidBody;
    void Start()
    {
        m_controlsService = ServiceLocator.GetService<ControlsService>();
        m_playerRigidBody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 inputVector = Vector3.zero;
        float forwardMovement = Convert.ToInt32(m_controlsService.isForwardDown) - 
                                Convert.ToInt32(m_controlsService.isBackDown);
        float sideMovement = Convert.ToInt32(m_controlsService.isRightDown) -
                             Convert.ToInt32(m_controlsService.isLeftDown);
        
        inputVector += new Vector3(this.transform.forward.x, 0, this.transform.forward.z).normalized
                       * forwardMovement;
        inputVector += new Vector3(this.transform.right.x, 0, this.transform.right.z).normalized
                       * sideMovement;
        inputVector = Vector3.Normalize(inputVector);
        m_playerRigidBody.AddForce(inputVector * (m_speed * Time.deltaTime));

    }
}
