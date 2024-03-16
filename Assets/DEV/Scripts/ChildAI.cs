using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildAI : MonoBehaviour
{
    [SerializeField]
    private GameObject destinationObj;

    private GameObject firstStepPos;
    private GameObject secondStepPos;
    private GameObject thirdStepPos;
    private GameObject fourthStepPos;

    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(destinationObj.transform.position);
    }

    public void firstStep()
    {
        agent.SetDestination(firstStepPos.transform.position);
    }
    public void secondStep()
    {
        agent.SetDestination(secondStepPos.transform.position);
    }
    public void thirdStep()
    {
        agent.SetDestination(thirdStepPos.transform.position);
    }
    public void fourthStep()
    {
        agent.SetDestination(fourthStepPos.transform.position);
    }
}
