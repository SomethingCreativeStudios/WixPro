using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class PanelDragger : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isDraggable;
    bool mouseOver;
    List<WixossCard> cardsToLoad = new List<WixossCard>();
    Vector3 startMousePosition, startPosition;
    public List<WixossCard> poolOfCards = new List<WixossCard>();
    public GameObject realParent;
    // Use this for initialization
    void Start()
    {
        
        foreach (Transform child in transform.parent)
        {
            if (child.tag == "Viewer" && child != transform)
                Destroy(child.gameObject);
        }

        foreach (WixossCard card in poolOfCards)
        {
            AddWixossCard(card);
        }
    }
    public void AddWixossCard(WixossCard card)
    {
        string pathTemp = "sets/setimages/" + card.CardId;
        GameObject go = (GameObject)Instantiate(Resources.Load("Card"));
        Image rend = go.GetComponent<Image>();
        WixCardComponent tempComponent = go.GetComponent<WixCardComponent>();
        tempComponent.Card = card;
        if (card.FaceUp)
            rend.sprite = Resources.Load<Sprite>(pathTemp);
        else
            rend.sprite = tempComponent.cardBack;
        Transform trans = GameObject.FindGameObjectsWithTag("Viewer")[0].transform;
        go.transform.SetParent(trans, false);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !mouseOver)
        {
            foreach (Transform child in transform.parent)
            {
                if (child.tag == "Viewer" && child == transform)
                    Destroy(child.gameObject);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            Vector3 currentPosition = Input.mousePosition;

            Vector3 diff = currentPosition - startMousePosition;

            Vector3 pos = startPosition + diff;

            this.transform.position = pos;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}

