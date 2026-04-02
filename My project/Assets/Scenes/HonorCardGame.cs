using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HonorCardGame : MonoBehaviour
{
    private Stack<string> _DeckStack;
    private List<string> _HandList;
    [SerializeField] private int _deckLimit = 16;
    [SerializeField] private int _deckSuits = 4;
    [SerializeField] private int _startingHand = 4;
    [SerializeField] private int _winCondition = 3;

    void Start()
    {
        _DeckStack = new Stack<string>();
        _HandList = new List<string>();

        // Creates a new deck with the specified parameters.
        CreateDeck(_deckLimit, _deckSuits);

        // Helper method to shuffle the deck.
        ShuffleDeck();

        // Starting hand for the player.
        DrawHand(_startingHand);
    }

    void Update()
    {
        // While loop runs until the player either wins the game or the deck runs out of cards.
        while (!WonGame() && _DeckStack.Count != 0)
        {
            DiscardDraw();
        }
    }

    // Helper method to create deck.
    private void CreateDeck(int deckLimit, int suitAmount)
    {
        // Creates a string array that contains the symbol for each suit in the deck.
        string[] suits = new string[]
        { "\u2663", "\u2660", "\u2665", "\u2666" }; // Clubs, Spades, Hearts, Diamonds


        // The array populates the DeckStack since each value is repeated with a new suit string.
        for (int i = 0; i < deckLimit / suitAmount; i++)
        {
            _DeckStack.Push("K" + suits[i]);
            _DeckStack.Push("Q" + suits[i]);
            _DeckStack.Push("J" + suits[i]);
            _DeckStack.Push("A" + suits[i]);
        }
    }

    // To ensure that memory is not continuously occupied by the temporary array, this helper method exists.
    private void ShuffleDeck()
    {
        // Shuffles the DeckStack after it has been created, clears the stack, and then re-adds the shuffled values.
        string[] tempArray = _DeckStack.OrderBy(x => Random.value).ToArray();
        _DeckStack.Clear();

        for (int i = 0; i < tempArray.Length; i++)
        {
            _DeckStack.Push(tempArray[i]);
        }
    }

    // Helper method to create the starting hand for the player.
    private void DrawHand(int handCount)
    {
        for (int i = 0; i < handCount; i++)
        {
            string card = _DeckStack.Pop();
            _HandList.Add(card);
        }

        Debug.Log("I made the initial deck and draw. My hand is: ");
        for (int i = 0; i < _HandList.Count; i++)
        {
            Debug.Log("- " + _HandList[i]);
        }
    }

    // Helper method to evaluate if the game has been won yet.
    private bool WonGame()
    {
        // Used to store how many of each suit is in the player's hand.
        int[] suitCount = new int[_deckSuits]; 

        // Checks each string to see how many of each suit it contains, storing the amount in suitCount.
        foreach (string card in _HandList)
        {
            if (card.Contains("\u2660")) // Spades
            {
                suitCount[0]++;
            }
            else if (card.Contains("\u2663")) // Clubs
            {
                suitCount[1]++;
            }
            else if (card.Contains("\u2665")) // Hearts
            {
                suitCount[2]++;
            }
            else // Diamonds
            {
                suitCount[3]++;
            }
        }

        // Checks suit count to see if the player has reached the win condition. If yes, return true.
        foreach (int suit in suitCount)
        {
            if (suit == _winCondition)
            {
                return true;
            }
        }

        // If the win condition was not reached, return false.
        return false;
    }

    // Helper method to discard and draw a new card in hand and in the deck respectively. Evaluates if the game is won as well.
    private void DiscardDraw()
    {
        // Selects a random card in hand to discard.
        int discarded = Random.Range(0, _HandList.Count);

        // Stores the discarded and drawn cards for printing.
        string discardCard = _HandList[discarded];
        string drawCard = _DeckStack.Pop();
        _HandList[discarded] = drawCard;

        // Determines if the player has won based on their hand. It is stored in a bool so WonGame() is not repeatedly invoked.
        bool hasWon = WonGame();

        // Evaluates if the game has been won and acts accordingly.
        if (!hasWon)
        {
            Debug.Log("I discarded " + discardCard + " and drew " + drawCard + ". ");
            Debug.Log("My hand is: ");
            for (int i = 0; i < _HandList.Count; i++)
            {
                Debug.Log("- " + _HandList[i]);
            }
            Debug.Log("This is not a winning hand. I will attempt to play another round.");
        }
        else if (hasWon)
        {
            Debug.Log("I discarded " + discardCard + " and drew " + drawCard + ". ");
            Debug.Log("My hand is: ");
            for (int i = 0; i < _HandList.Count; i++)
            {
                Debug.Log("- " + _HandList[i]);
            }
            Debug.Log("The game is WON!");
        }
        else if (_DeckStack.Count == 0)
        {
            Debug.Log("The deck is empty. The game is LOST.");
        }
    }
}
