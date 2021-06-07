using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card 
{
    public Sprite cardImage;
    public int weight;

    public Card(Card card)
    {
        this.cardImage = card.cardImage;
        this.weight = card.weight;
    }
}
