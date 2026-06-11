using UnityEngine;
using System.Collections.Generic;

public class QLearning : MonoBehaviour
{
    public Dictionary<string, float> qTable = new Dictionary<string, float>();

    public float learningRate = 0.1f;
    public float discountFactor = 0.9f;
    public float explorationRate = 0.2f;

    public string[] actions =
    {
        "Patrol",
        "Chase",
        "Attack"
    };

    public string ChooseAction(string state)
    {
        if (Random.value < explorationRate)
        {
            return actions[Random.Range(0, actions.Length)];
        }

        float maxQ = float.MinValue;
        string bestAction = actions[0];

        foreach (string action in actions)
        {
            string key = state + action;

            if (!qTable.ContainsKey(key))
            {
                qTable[key] = 0f;
            }

            if (qTable[key] > maxQ)
            {
                maxQ = qTable[key];
                bestAction = action;
            }
        }

        return bestAction;
    }

    public void UpdateQValue(string state, string action, float reward, string nextState)
    {
        string key = state + action;

        if (!qTable.ContainsKey(key))
        {
            qTable[key] = 0f;
        }

        float maxQNext = float.MinValue;

        foreach (string nextAction in actions)
        {
            string nextKey = nextState + nextAction;

            if (!qTable.ContainsKey(nextKey))
            {
                qTable[nextKey] = 0f;
            }

            maxQNext = Mathf.Max(maxQNext, qTable[nextKey]);
        }

        qTable[key] += learningRate * (reward + discountFactor * maxQNext - qTable[key]);
    }
}
