using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Utils;
using UnityEngine.EventSystems;
using Photon;
using Assets.Game_Controls.Scripts.Enums;
using System;
using CodeBureau;

public class PoolViewerScript : PunBehaviour{

    public Transform parentCanvas;
    private Vector3 startMousePosition;
    private Vector3 startPosition;
    public List<WixossCard> poolOfCards = new List<WixossCard>();

    public CardController cardController;
    public Location location;

    public string[] menuItems = new string[] { "Send To Waiting Room", "Send To" };

    public bool isOp = false;

    [HideInInspector]
    public List<GameObject> poolOfGameObjects
    {
        get {
            List<GameObject> tempObjects = new List<GameObject>();

            foreach (var item in poolOfCards)
            {
                tempObjects.Add(AddWixossCard(item));
            }

            return tempObjects;
        }
    }

    [HideInInspector]
    public List<string> getCardIds
    {
        get
        {
            List<string> cardIds = new List<string>();

            foreach ( var WixossCard in poolOfCards )
            {
                cardIds.Add(WixossCard.CardId);
            }

            return cardIds;
        }
    }

    public bool canBeViewed = true;

    // Use this for initialization
    void Start () {
        StartUp();
    }
    
    // Update is called once per frame
    void Update () {
        UpdateScript();
    }

    public virtual void StartUp()
    {

    }
    public virtual void UpdateScript()
    {

    }

    public void ShufflePool()
    {
        poolOfCards.Shuffle();
    }

    public virtual void MenuClicked()
    {
        if (Input.GetMouseButtonDown(0) && canBeViewed)
        {
            GameObject poolViewer = (GameObject)Instantiate(Resources.Load("PoolViewer"));
            poolViewer.name = gameObject.GetComponent<DropZone>().zoneType.ToString() + " Viewer";
            PanelDragger dragger = poolViewer.GetComponent<PanelDragger>();
            dragger.poolOfCards = new List<WixossCard>(poolOfCards);
            //poolViewer.AddComponent<CanvasRenderer>();
            dragger.realParent = gameObject;
            poolViewer.transform.SetParent(parentCanvas.transform, false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            GameObject contextMenu = (GameObject)Instantiate(Resources.Load("ContextMenu"));
            ContextMenuScript menu = contextMenu.GetComponent<ContextMenuScript>();
            menu.SetUpContextMenu(new List<string>(menuItems));
            menu.clicked += Menu_clicked;
            contextMenu.name = "Pool Viewer Context Menu";
            //poolViewer.AddComponent<CanvasRenderer>();
            contextMenu.transform.SetParent(parentCanvas, false);
            contextMenu.transform.position = Input.mousePosition;
        }
    }

    /// <summary>
    /// Turn WixossCard into a gameObject,Comes
    /// </summary>
    /// <param name="card">The Card being turned into a gameObject</param>
    /// <returns>The gameObject, it contains an WixossCardComponent. Parent is tag 'Main Field'</returns>
    public static GameObject AddWixossCard(WixossCard card)
    {
        return AddWixossCard(card, true);
    }

    /// <summary>
    /// Turn WixossCard into a gameObject
    /// </summary>
    /// <param name="card">The Card being turned into a gameObject</param>
    /// <param name="destoryObject">removed the gameObject from main field. Just return an gameObject</param>
    /// <returns>The gameObject, it contains an WixossCardComponent. Parent is tag 'Main Field'</returns>
    public static GameObject AddWixossCard(WixossCard card , bool destoryObject)
    {
        WWW www = null;
        if ( !card.FaceUp )
            www = new WWW("file://" + Constants.cardBack);
        else
            www = new WWW("file://" + card.CardImagePath);

        GameObject go = (GameObject)Instantiate(Resources.Load("Card"));
        go.name = card.CardSet + "-" + card.CardNumberInSet;
        Image rend = go.GetComponent<Image>();
        WixCardComponent tempComponent = go.GetComponent<WixCardComponent>();
        tempComponent.Card = card;
        Sprite tempTexture = Sprite.Create(www.texture , new Rect(0 , 0 , www.texture.width , www.texture.height) , new Vector2(0.5f , 0.5f));


        rend.sprite = tempTexture;

        Transform trans = GameObject.FindGameObjectsWithTag("Main Field")[0].transform;
        go.transform.SetParent(trans , false);
        go.transform.position = new Vector3(-100 , -100 , 0);
        CardController.SetSize(go , 100 , 100); //By default, the size is 0... for some reason
        if ( destoryObject )
            DestroyObject(go);//we just want the object not show in editor
        return go;
    }

    /// <summary>
    /// Turn WixossCard into a gameObject
    /// </summary>
    /// <param name="card">The Card being turned into a gameObject</param>
    /// <param name="parentTag">GameObject with this tag is the parent</param>
    /// <param name="destoryObject">removed the gameObject from main field. Just return an gameObject</param>
    /// <returns>The gameObject, it contains an WixossCardComponent. Parent is tag 'parentTag'</returns>
    public static GameObject AddWixossCard(WixossCard card, string parentTag , bool destoryObject)
    {
        if ( parentTag == "Untagged" )
            parentTag = "Main Field";

        WWW www = null;
        if ( !card.FaceUp )
            www = new WWW("file://" + Constants.cardBack);
        else
            www = new WWW("file://" + card.CardImagePath);

        GameObject go = (GameObject)Instantiate(Resources.Load("Card"));
        go.name = card.CardSet + "-" + card.CardNumberInSet;
        Image rend = go.GetComponent<Image>();
        WixCardComponent tempComponent = go.GetComponent<WixCardComponent>();
        tempComponent.Card = card;
        Sprite tempTexture = Sprite.Create(www.texture , new Rect(0 , 0 , www.texture.width , www.texture.height) , new Vector2(0.5f , 0.5f));
        rend.sprite = tempTexture;

        tempComponent.tempSprite = tempTexture;
        Transform trans = GameObject.FindGameObjectsWithTag(parentTag)[0].transform;
        go.transform.SetParent(trans, false);
        go.transform.position = new Vector3(-100, -100, 0);

        CardController.SetSize(go, 100, 100); //By default, the size is 0... for some reason
        if (destoryObject)
            DestroyObject(go);//we just want the object not show in editor

        return go;
    }

    /// <summary>
    /// Finds the card that is directly under the mouse. MoveCard
    /// </summary>
    /// <returns>The card. If nothing found will return null</returns>
    public GameObject CardUnderMouse()
    {
        GameObject cardUnderTheMouse = null;

        PointerEventData pe = new PointerEventData(EventSystem.current);
        pe.position = Input.mousePosition;

        List<RaycastResult> hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pe, hits);
        bool hit = false;
        GameObject hgo = null;
        foreach (RaycastResult h in hits)
        {
            GameObject g = h.gameObject;
            hit = (g.tag == "Card");
                   
            if (hit)
            {
                cardUnderTheMouse = g;
                break;
            }
        }

        return cardUnderTheMouse;
    }

    /// <summary>
    /// Finds the WixossCard that is directly under the mouse. MoveCardShowCard
    /// </summary>
    /// <returns>The WixossCard. If nothing found will return null</returns>
    public WixossCard WixossCardUnderMouse() 
    {
        GameObject gameObject = CardUnderMouse();

        WixCardComponent cardComponent = gameObject.GetComponent<WixCardComponent>();
        WixossCard card = null;

        if ( cardComponent != null )
            card = cardComponent.Card;

        return card;
    }

    /// <summary>
    /// Given a gameObject with WixossCardComponent, retunr pos in poolOfCards
    /// </summary>
    /// <param name="cardObject">object with WixossCardComponent</param>
    /// <returns>card pos in poolOfCards</returns>
    public int CardToPos(GameObject cardObject)
    {
        int pos = -1;
        if (cardObject != null)
        {
            WixCardComponent cardComponent = cardObject.GetComponent<WixCardComponent>();

            if (cardComponent != null)
            {
                pos = CardToPos(cardComponent.Card);
            }
        }
        return pos;
    }

    /// <summary>
    /// Find WixossCard in poolOfCards 
    /// </summary>
    /// <param name="card">Card that your want found</param>
    /// <returns></returns>
    public int CardToPos(WixossCard card)
    {
        int pos = -1;
        for (int i = 0; i < poolOfCards.Count; i++)
        {
            if (card.Equals(poolOfCards[i]))
            {
                pos = i;
                break;
            }
        }

        return pos;
    }

    public void sendToX(string selectedItem)//will be inherited by most contollers
    {
        if ( selectedItem == GetMenuItem(SendToMenu.SendToTrash) )
        {
            cardController.MoveCard(CardUnderMouse(), cardController.locationToGameObject(location), cardController.WaitingRoom);
        }
        if ( selectedItem == GetMenuItem(SendToMenu.ShuffleIntoDeck) )
        {
            cardController.MoveCard(CardUnderMouse(), cardController.locationToGameObject(location), cardController.PlayerDeck);
            cardController.ShufflePlayerDeck();
        }
        if ( selectedItem == GetMenuItem(SendToMenu.SendToTopOfDeck) )
        {
            cardController.MoveCard(CardUnderMouse(), cardController.locationToGameObject(location), cardController.PlayerDeck);
            List<WixossCard> WixossCards = cardController.PlayerDeck.GetComponent<PoolViewerScript>().poolOfCards;
            WixossCard temp = WixossCards[WixossCards.Count - 1];
            WixossCards.Insert(0, temp);
            WixossCards.RemoveAt(WixossCards.Count - 1);
            cardController.SyncDeckWithOp();
        }
        if ( selectedItem == GetMenuItem(SendToMenu.SendToBottomOfDeck) )
        {
            cardController.MoveCard(CardUnderMouse(), cardController.locationToGameObject(location), cardController.PlayerDeck);
        }
        if ( selectedItem == GetMenuItem(SendToMenu.SendToHand) )
        {
            cardController.MoveCardShowCard(WixossCardUnderMouse(), cardController.locationToGameObject(location), cardController.PlayerHand, 0);
        }
    }
    public virtual void Menu_clicked(string menuName)
    {
    }

    public string GetMenuItem(Enum menuItem)
    {
        return StringEnum.GetStringValue(menuItem);
    }
}
