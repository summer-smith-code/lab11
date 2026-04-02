using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DuplicateNameDetector : MonoBehaviour
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
    private string[] nameArray = new string[15];
    private HashSet<string> seen = new HashSet<string>();
    private HashSet<string> duplicates = new HashSet<string>();


    private string GetRandomPlayerName()
    {
        string firstName = firstNames[Random.Range(0, firstNames.Length)];
        return firstName;
    }

        // Start is called before the first frame update
        void Start()
    {
        for (int i = 0; i < nameArray.Length; i++)
        {
            nameArray[i] = GetRandomPlayerName();
        }
        // Create a string to display the contents of the name array.
        string arrayAsString = "Created the name array: ";
        arrayAsString += string.Join(", ", nameArray);
        arrayAsString += ".";
        // Print the name array to the console.
        Debug.Log(arrayAsString);

        // Loop through the name array and add each name to the seen set. If a name is already in the seen set, add it to the duplicates set.
        foreach (string name in nameArray)
        {
            if (seen.Add(name) == false)
            {
                duplicates.Add(name);
            }
            else
            {
                seen.Add(name);
            }
        }
        if (duplicates.Count > 0)
        {
            string duplicatesAsString = "The array has duplicate names: ";
            // Use Join to create a string of the duplicate names separated by commas, and add a period at the end.
            duplicatesAsString += string.Join(", ", duplicates);
            duplicatesAsString += ".";
            Debug.Log(duplicatesAsString);
        }
        else
        {
            Debug.Log("No duplicate names detected.");
        }
    }


}
