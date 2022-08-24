using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] List<Card> cards;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddCard(Card newCard)
    {
        cards.Add(newCard);
    }
    public void PlayCard()
    {
        if(cards.Count!=0)
        {
            cards[0].Play();
            cards.RemoveAt(0);
        }
    }
}
