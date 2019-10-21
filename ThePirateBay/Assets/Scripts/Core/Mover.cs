using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour, IAction
{
    Animator anim;
    NavMeshAgent agent;
    Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.enabled = !health.isDead;

        UpdateAnimator();   
    }

    public void StartMoveAction(Vector3 destination) {
        GetComponent<Fighter>().Cancel();
        GetComponent<ActionScheduler>().StartAction(this);
        MoveTo(destination);
    }

    public void MoveTo(Vector3 destination) {
        agent.SetDestination(destination);
        agent.isStopped = false;
    }
    public void Cancel() {
        agent.isStopped = true;
    }

    void UpdateAnimator()
    {
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;

        anim.SetFloat("forwardSpeed", speed);
    }
}
