using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    private Animator characterAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        characterAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            MoveToCursor();
        }

        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            playerAgent.destination = hit.point;
        }
    }
    
    private void UpdateAnimator()
    {
        Vector3 velocity = playerAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float characterSpeed = localVelocity.z;
        characterAnimator.SetFloat("forwardSpeed", characterSpeed);
        
    }
}
