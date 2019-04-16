using UnityEngine;
using System.Collections;

public class baseTest1 : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    public Transform target;
    // Use this for initialization
    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        SetWakeLayer();
    }

    private bool open = false;
    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.position);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //agent.Stop();
            open = !open;
            SetWakeLayer();
        }
    }
    void SetWakeLayer()
    {
        Debug.Log("open = " + open.ToString());
        if (open)
        {
            agent.areaMask = 8;//（n-1）
        }
        else
        {
            agent.areaMask = 0;
        }
        SetDestination();
    }
    void SetDestination()
    {
        agent.SetDestination(target.position);
    }

}
