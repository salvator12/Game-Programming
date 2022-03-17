using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MovementController : MonoBehaviour
{
    NavMeshAgent agent;
    bool isMove = false;
    Vector2 lastClickedPos;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMove = true;
        }
        if (isMove && (Vector2)transform.position != lastClickedPos)
        {
            MoveToPoint(lastClickedPos);
        }        
    }
    public void MoveToPoint(Vector2 point)
    {
        agent.SetDestination(point);
    }
}
