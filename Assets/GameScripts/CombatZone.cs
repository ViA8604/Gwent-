using System.Collections;
using System.Collections.Generic;
using GwentPro;
using UnityEngine;

public class CombatZone : MonoBehaviour
{
    public List<GameObject> CardsInScene;
    public CardClass.combatype cmbtype;

    // Start is called before the first frame update
    void Start()
    {
        CardsInScene = new List<GameObject>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CardClass card = collision.gameObject.GetComponent<CardClass>();
        if (card != null)
        {
            card.actualzone = gameObject;
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        CardClass card = collision.gameObject.GetComponent<CardClass>();
        if (card != null)
        {
            card.actualzone = gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        CardClass card = collision.gameObject.GetComponent<CardClass>();
        if (card != null)
       card.actualzone = null;
    }


    // Update is called once per frame
    void Update()
    {
        /* foreach(Transform child in gameObject.transform)
         {
             CardsInScene.Add(child.gameObject);
         }
     */
    }
}
