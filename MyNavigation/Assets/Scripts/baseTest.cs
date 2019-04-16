using UnityEngine;
using System.Collections;

public class baseTest : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent man;
    public Transform target;
    // Use this for initialization
    void Start()
    {
        man = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        man.SetDestination(target.position);
    }
}
