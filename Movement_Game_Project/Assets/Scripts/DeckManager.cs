using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] List<Card> cards;
    [SerializeField] float cooldown=3;
    // Start is called before the first frame update
    private void Start()
    {
        cards.Add(new DoubleJump());
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0) { cooldown -= Time.deltaTime; }
    }
    public void AddCard(Card newCard)
    {
        cards.Add(newCard);
    }
    public void PlayCard()
    {
        if (cards.Count != 0)
        {
            cards[0].Play();
            cards.RemoveAt(0);
        }
        if(cards.Count!=0&& cooldown <= 0)
        {
            cards[0].AddToObj(this.gameObject);
            //if it succesfully is used
            if (this.gameObject.GetComponent<Card>().Play())
            {
                cards.Add(cards[0]);
                cards.RemoveAt(0);
                Destroy(this.gameObject.GetComponent<Card>());
            }
        }
    }
}
