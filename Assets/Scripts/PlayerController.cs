using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Services;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ControlsService m_controlsService;
    [SerializeField] private int m_speed;
    [SerializeField] private GameObject m_cameraHandler;
    private Rigidbody m_playerRigidBody;
    private Animator m_animations;
    void Start()
    {
        m_controlsService = ServiceLocator.GetService<ControlsService>();
        m_playerRigidBody = this.GetComponent<Rigidbody>();
        m_animations = this.GetComponentInChildren<Animator>();
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
        
        inputVector += new Vector3(m_cameraHandler.transform.forward.x, 0, m_cameraHandler.transform.forward.z).normalized
                       * forwardMovement;
        inputVector += new Vector3(m_cameraHandler.transform.right.x, 0, m_cameraHandler.transform.right.z).normalized
                       * sideMovement;
        inputVector = Vector3.Normalize(inputVector);
        
        m_playerRigidBody.AddForce(inputVector * (m_speed * Time.deltaTime));
        
        if (inputVector.x != 0 || inputVector.z != 0)
        {
            if (m_playerRigidBody.velocity.sqrMagnitude > 1)
            {
                transform.rotation = Quaternion.LookRotation(m_playerRigidBody.velocity);
            }
            m_animations.Play("KayKit Animated Character|Walk");
        }
        else
        {
            m_animations.Play(("KayKit Animated Character|Idle"));
        }
    }
}
