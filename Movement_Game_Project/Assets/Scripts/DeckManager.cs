using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] List<Card> cards;
<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        
=======
    [SerializeField] float cooldown=3;
    // Start is called before the first frame update
    void Start()
    {
        cards.Add(new DoubleJump());
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        
=======
        if (cooldown <= 0) { cooldown -= Time.deltaTime; }
>>>>>>> Stashed changes
    }
    public void AddCard(Card newCard)
    {
        cards.Add(newCard);
    }
    public void PlayCard()
    {
<<<<<<< Updated upstream
        if(cards.Count!=0)
        {
            cards[0].Play();
            cards.RemoveAt(0);
=======
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
>>>>>>> Stashed changes
        }
    }
}
