using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

public class LoginQueueSim : MonoBehaviour
{
    private static string[] firstNames = new string[]
    {
       "Alex",
       "Jordan",
       "Taylor",
       "Morgan",
       "Casey",
       "Riley",
       "Jamie",
       "Cameron",
       "Drew",
       "Avery",
       "Blake",
       "Quinn",
       "Reese",
       "Skyler",
       "Parker",
       "Dakota",
       "Emerson",
       "Finley",
       "Chandler",
       "Summer",
       "Elle",
       "Cami",
       "Lauren",
       "Chase"
    };
    private static string[] lastInitials = new string[]
    {
        "A",
        "B",
        "C",
        "D",
        "E",
        "F",
        "G",
        "H",
        "I",
        "J",
        "K",
        "L",
        "M",
        "N",
        "O",
        "P",
        "Q",
        "R",
        "S",
        "T",
        "U",
        "V",
        "W",
        "X",
        "Y",
        "Z"
    };

    Queue<string> loginQueue = new Queue<string>();

    private string GetRandomPlayerName()
    {
        string firstName = firstNames[Random.Range(0, firstNames.Length)];
        string lastInitial = lastInitials[Random.Range(0, lastInitials.Length)];
        return $"{firstName} {lastInitial}.";
    }

    void Start()
    {
        int loginCount = Random.Range(4, 7);
        for (int i = 0; i < loginCount; i++)
            loginQueue.Enqueue(GetRandomPlayerName());
        string queueMessage = $"Initial login queue created. There are {loginCount} players in the queue: ";
        loginQueue.ToList().ForEach(name => queueMessage += ($"{name}, "));
        // remove end comma and space
        queueMessage = queueMessage.Substring(0, queueMessage.Length - 2);
        // print initial queue
        Debug.Log(queueMessage);
        StartCoroutine(AddPlayer());
        StartCoroutine(LoginPlayer());
    }

    IEnumerator AddPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 6f));
            string playerName = GetRandomPlayerName();
            loginQueue.Enqueue(playerName);
            Debug.Log($"{playerName} is trying to login and added to the login queue.");
        }
    }
    IEnumerator LoginPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            if (loginQueue.Count > 0)
            {
                string playerName = loginQueue.Dequeue();
                Debug.Log($"{playerName} is now inside the game.");
            }
            else
            {
                Debug.Log("Login server is idle. No players are waiting.");
            }
        }
    }
}
