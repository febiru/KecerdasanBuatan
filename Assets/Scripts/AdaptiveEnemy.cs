using UnityEngine;
using UnityEngine.AI;

public class AdaptiveEnemy : MonoBehaviour
{
    public Transform player;

    private NavMeshAgent agent;
    private QLearning qLearning;

    private string currentState;
    private string currentAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        qLearning = GetComponent<QLearning>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > 10f)
        {
            currentState = "PlayerFar";
        }
        else if (distance > 3f)
        {
            currentState = "PlayerNear";
        }
        else
        {
            currentState = "PlayerClose";
        }

        currentAction = qLearning.ChooseAction(currentState);

        ExecuteAction(currentAction);
    }

    void ExecuteAction(string action)
    {
        switch (action)
        {
            case "Patrol":
                Patrol();
                break;
            case "Chase":
                Chase();
                break;
            case "Attack":
                Attack();
                break;
        }
    }

    void Patrol()
    {
        agent.isStopped = false;
    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        agent.isStopped = true;

        float reward = 10;

        qLearning.UpdateQValue(currentState, currentAction, reward, currentState);

        Debug.Log("Adaptive Attack");
    }
}
