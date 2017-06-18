using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon;
using Assets.Game_Controls.Scripts.Enums;
using Assets.Utils;
using Assets.Game_Controls.Scripts;
using System;
using UnityEngine.UI;

public class CardController : PunBehaviour
{

    /// <summary>
    /// Destory the gameObject after a move is completed
    /// </summary>
    [HideInInspector]
    public bool destoryCardAfterMove = true;


    /// <summary>
    /// How fast is do we want to move the card, default is 4f
    /// </summary>
    [HideInInspector]
    public float cardSpeed = 4f;


    /// <summary>
    /// Do we want to send RPC commands?
    /// </summary>
    [Tooltip("Do we want to send RPC commands?")]
    public bool sendRPC = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    /// <summary>
    /// This is used to to move a card and then show it onced added. An example, of this is the hand.
    /// </summary>
    /// <param name="cardBeingMoved">The card that is being moved</param>
    /// <param name="initalObject">Where is the card coming from?</param>
    /// <param name="targetObject">Where is the card going?</param>
    public void MoveCardShowCard(WixossCard cardBeingMoved , GameObject initalObject , GameObject targetObject, int siblingIndex)
    {
        GameObject wixossCard = PoolViewerScript.AddWixossCard(WixossCard.Clone(cardBeingMoved) , targetObject.tag , false);

        Vector3 startingPos = initalObject.transform.position;
        Vector3 endingPos = targetObject.transform.position;

        bool oldRPC = sendRPC;
        sendRPC = false;
        destoryCardAfterMove = false;

        MoveCard(wixossCard , initalObject , targetObject , startingPos , endingPos , cardSpeed, siblingIndex);

        destoryCardAfterMove = true;
        sendRPC = oldRPC;

        if ( sendRPC )
            this.getPhotonView().RPC(Constants.RPC_MoveCardShowCardToX , PhotonTargets.Others , cardBeingMoved.CardId , ControllerHelper.GameObjectToLocation(initalObject) , ControllerHelper.GameObjectToLocation(targetObject));
    }

    /// <summary>
    /// Move the card from one pool to another
    /// </summary>
    /// <param name="cardObject">The card being moved</param>
    /// <param name="startingObject">Where is the card coming from?</param>
    /// <param name="endingObject">Where is the card going?</param>
    public void MoveCard(GameObject cardObject , GameObject startingObject , GameObject endingObject)
    {
        MoveCard(cardObject , startingObject , endingObject , cardSpeed);
    }

    /// <summary>
    /// Move the card from one pool to another
    /// </summary>
    /// <param name="cardObject">The card being moved</param>
    /// <param name="startingObject">Where is the card coming from?</param>
    /// <param name="endingObject">Where is the card going?</param>
    /// <param name="cardSpeed">How fast is the card moving?</param>
    public void MoveCard(GameObject cardObject , GameObject startingObject , GameObject endingObject , float cardSpeed)
    {
        MoveCard(cardObject , startingObject , endingObject , startingObject.transform.position , endingObject.transform.position , cardSpeed);
    }

    /// <summary>
    /// Move the card from one pool to another
    /// </summary>
    /// <param name="cardObject">The card being moved</param>
    /// <param name="startingObject">Where is the card coming from?</param>
    /// <param name="endingObject">Where is the card going?</param>
    /// <param name="cardSpeed">How fast is the card moving?</param>
    /// <param name="startingPos">The starting position of the card</param>
    /// <param name="endingPos">The ending position of the card</param>
    public void MoveCard(GameObject cardObject , GameObject startingObject , GameObject endingObject , Vector3 startingPos , Vector3 endingPos , float cardSpeed, int siblingIndex = 0)
    {
        WixCardComponent cardComponent = cardObject.GetComponent<WixCardComponent>();
        PoolViewerScript startingViewer = startingObject.GetComponent<PoolViewerScript>();
        PoolViewerScript endingViewer = endingObject.GetComponent<PoolViewerScript>();

        if ( cardComponent != null )
        {
            bool flipCardOnMove = false;
            if ( startingViewer != null && endingViewer != null )
            {
                startingViewer.poolOfCards.IndexOf(cardComponent.Card);
                startingViewer.poolOfCards.Remove(cardComponent.Card);
                endingViewer.poolOfCards.Add(WixossCard.Clone(cardComponent.Card));

                //handle who can view the cards, dont show op hand for example
                if ( startingViewer.isOp )
                {
                    if (endingViewer.location == Location.Hand )
                    {
                        cardComponent.FlipCard();
                    }

                } else
                {
                    //Don't show cards going to hand, but show hand cards
                    if ( startingViewer.location == Location.Deck )
                    {
                        cardComponent.FlipCard();

                        if ( endingViewer.location == Location.Hand )
                            flipCardOnMove = true;
                    }
                }

            }

            cardComponent.CurrentPos = startingPos;
            cardComponent.TargetPos = endingPos;
            cardComponent.cardSpeed = cardSpeed;
            cardComponent.transform.SetSiblingIndex(siblingIndex);
            cardComponent.StartMove(destoryCardAfterMove, flipCardOnMove , cardSpeed);

            if(sendRPC)
                this.getPhotonView().RPC(Constants.RPC_MoveCardToX , PhotonTargets.Others , cardComponent.Card.CardId , ControllerHelper.GameObjectToLocation(startingObject) , ControllerHelper.GameObjectToLocation(endingObject));

        } else
        {
            throw new UnityException("Card Object Null");
        }

    }

    /// <summary>
    /// Move alot of card(will show a max of 5 at once)
    /// </summary>
    /// <param name="cardObjects">The cards being moved</param>
    /// <param name="startingObject">The starting object, used for position</param>
    /// <param name="endingObject">The ending object, used for position</param>
    /// <param name="dir">What direction is the card moving?
    ///  1 Up
    ///  2 Down
    ///  3 Right
    ///  4 Left 
    ///  </param>
    public void MoveCards(List<GameObject> cardObjects , GameObject startingObject , GameObject endingObject , int dir)
    {
        int maxCardsShown = 5;
        int count = 0;
        Vector3 startingPos = startingObject.transform.position;
        Vector3 endingPos = endingObject.transform.position;

        foreach ( GameObject cardObject in cardObjects )
        {
            MoveCard(cardObject , startingObject , endingObject , startingPos , endingPos , cardSpeed);

            if ( maxCardsShown < count )
            {
                switch ( dir )
                {
                    case 1: //Up
                        startingPos.y -= 10;
                        break;
                    case 2: //Down
                        startingPos.y += 10;
                        break;
                    case 3: //Right
                        startingPos.x -= 10;
                        break;
                    case 4: //Left
                        startingPos.x += 10;
                        break;
                }
            }

            count++;
        }
    }

    /// <summary>
    /// Check to see if the deck needs to be "Refreshed", This will show the cards being moved
    /// </summary>
    public void RefreshDeck() /// LOOK AT
    {
        DeckController playersDeck = ControllerHelper.FindGameObject(Location.Deck).GetComponent<DeckController>();
        TrashContoller trashZone = ControllerHelper.FindGameObject(Location.TrashZone).GetComponent<TrashContoller>();

        if ( playersDeck.poolOfCards.Count == 0 ) // check to make sure theres no cards
        {
            trashZone.ShufflePool();
            MoveCards(trashZone.poolOfGameObjects , ControllerHelper.FindGameObject(Location.TrashZone) , ControllerHelper.FindGameObject(Location.Hand) , 1);
            MoveCard(PoolViewerScript.AddWixossCard(playersDeck.poolOfCards[0] , false) , ControllerHelper.FindGameObject(Location.Deck) , ControllerHelper.FindGameObject(Location.Deck) , 3f);//Change AddWixossCard to take tag parameter, this should alow you to see card
        }
    }

    /// <summary>
    /// Shuffle's player1's deck
    /// </summary>
    public void ShufflePlayerDeck()
    {
        DeckController playersDeck = ControllerHelper.FindGameObject(Location.Deck).GetComponent<DeckController>();
        playersDeck.ShufflePool();

        SyncDeckWithOp();
    }

    /// <summary>
    /// Get Deck Controller, for easier access. MIGHT REMOVE
    /// </summary>
    public DeckController getPlayerDeckController()
    {
        DeckController playersDeck = ControllerHelper.FindGameObject(Location.Deck).GetComponent<DeckController>();
        return playersDeck;
    }

    /// <summary>
    /// Used to size a gameObject
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static void SetSize(GameObject gameObject , float width , float height)
    {
        if ( gameObject != null )
        {
            var rectTransform = gameObject.GetComponent<RectTransform>();
            if ( rectTransform != null )
            {
                rectTransform.sizeDelta = new Vector2(width , height);
            }
        }
    }

    /// <summary>
    /// Get all the cards on my field
    ///  [0][1][2]
    ///   [3][4]
    /// </summary>
    /// <returns></returns>
    public List<WixCardComponent> GetCardsOnMyField()
    {
        List<WixCardComponent> WixossCardComponents = new List<WixCardComponent>();
        WixossCardComponents.Add(ControllerHelper.FindGameObject(Location.SIGNI_Left).GetComponentInChildren<WixCardComponent>());
        WixossCardComponents.Add(ControllerHelper.FindGameObject(Location.SIGNI_Center).GetComponentInChildren<WixCardComponent>());
        WixossCardComponents.Add(ControllerHelper.FindGameObject(Location.SIGNI_Right).GetComponentInChildren<WixCardComponent>());
        return WixossCardComponents;
    }

    /// <summary>
    /// Get all the cards on OP field
    ///    [3][4]
    ///   [0][1][2]
    /// </summary>
    /// <returns></returns>
    public List<WixCardComponent> GetCardsOnMyOpField()
    {
        List<WixCardComponent> WixossCardComponents = new List<WixCardComponent>();

        WixossCardComponents.Add(ControllerHelper.FindGameObject(Location.SIGNI_Left , true).GetComponentInChildren<WixCardComponent>());
        WixossCardComponents.Add(ControllerHelper.FindGameObject(Location.SIGNI_Center , true).GetComponentInChildren<WixCardComponent>());
        WixossCardComponents.Add(ControllerHelper.FindGameObject(Location.SIGNI_Right , true).GetComponentInChildren<WixCardComponent>());

        return WixossCardComponents;
    }

    /// <summary>
    /// Change the WixossCardComponent State, also Sync to Op
    /// </summary>
    /// <param name="WixossCardComponet"></param>
    /// <param name="cardState"></param>
    public void ChangeCardState(WixCardComponent WixossCardComponet, CardState cardState)
    {
        WixossCardComponet.ChangeState(cardState);
        if(WixossCardComponet.GetComponentInParent<PoolViewerScript>() != null )
        {
            Location cardLocation = WixossCardComponet.GetComponentInParent<PoolViewerScript>().location;

            if ( sendRPC )
                this.getPhotonView().RPC(Constants.RPC_ChangeCardState , PhotonTargets.Others , cardState , cardLocation);
        }
    }
    
    /// <summary>
    /// Change the game phase, and then sync it with both players
    /// </summary>
    /// <param name="newGamePhase">What the new game phase will be</param>
    public void UpdateGamePhase(GamePhase newGamePhase)
    {
        GamePhaseCounter.currentPhase = newGamePhase;
        if (sendRPC)
            this.getPhotonView().RPC(Constants.RPC_UpdateGameState, PhotonTargets.Others, GamePhaseCounter.currentPhase);
    }

    /// <summary>
    /// Sync what your op has for your deck
    /// </summary>
    public void SyncDeckWithOp()
    {
        if (sendRPC)
            this.getPhotonView().RPC(Constants.RPC_SetOPDeck, PhotonTargets.Others, ControllerHelper.FindGameObject(Location.Deck).GetComponent<PoolViewerScript>().getCardIds.ToArray());
    }

    public void SendFlagToOp(string flagName, bool flagValue)
    {
        if (sendRPC)
            this.getPhotonView().RPC(Constants.RPC_SendFlag, PhotonTargets.Others, flagName, flagValue);
    }
    #region Networking Stuff
    [PunRPC]
    public void RPCMoveCardToX(string cardId , Location fromLocation , Location toLocation)
    {
        bool oldSendRPC = sendRPC;
        sendRPC = false;

        WixossCard cardBeingMoved = null;
        CardCollection.cardCollection.TryGetValue(cardId , out cardBeingMoved);
        GameObject fromLocationObject = ControllerHelper.FindGameObject(fromLocation , true);
        if (cardBeingMoved != null )
        {
            
            GameObject cardObject = PoolViewerScript.AddWixossCard(cardBeingMoved, false);
            MoveCard(cardObject , fromLocationObject , ControllerHelper.FindGameObject(toLocation , true));
        }

        //Clean Up
        DropZone dropZone = fromLocationObject.GetComponent<DropZone>();
        if ( dropZone != null )
        {
            if(dropZone.zoneType == DropZone.DropZoneType.Hand || dropZone.zoneType == DropZone.DropZoneType.Field )
            {
                for ( int i = 0; i < fromLocationObject.transform.childCount; i++ )
                {
                   WixCardComponent cardComponent = fromLocationObject.transform.GetChild(i).gameObject.GetComponent<WixCardComponent>();
                    if(cardComponent.Card.CardId == cardId )
                    {
                        DestroyObject(fromLocationObject.transform.GetChild(i).gameObject);
                        break;
                    }
                }
            }
        }

        sendRPC = oldSendRPC;
    }

    [PunRPC]
    public void RPCMoveCardShowCardToX(string cardId , Location fromLocation , Location toLocation)
    {
        bool oldSendRPC = sendRPC;
        sendRPC = false;

        WixossCard cardBeingMoved = null;
        CardCollection.cardCollection.TryGetValue(cardId , out cardBeingMoved);
        GameObject fromLocationObject = ControllerHelper.FindGameObject(fromLocation , true);
        GameObject toLocationObject = ControllerHelper.FindGameObject(toLocation , true);
        WixCardComponent oldComponent = null;

        if ( isLocationField(fromLocation) )
        {
            oldComponent = fromLocationObject.GetComponentInChildren<WixCardComponent>();
            cardBeingMoved = oldComponent.Card; //keep the mod of old card
        }



        if ( cardBeingMoved != null )
        {
            MoveCardShowCard(cardBeingMoved , fromLocationObject , toLocationObject, 0);

            if ( isLocationField(toLocation) )
                toLocationObject.GetComponentInChildren<WixCardComponent>().ChangeStateOP(cardBeingMoved.StateOfCard);
        }

        //Clean Up
        DropZone dropZone = fromLocationObject.GetComponent<DropZone>();
        if ( dropZone != null )
        {
            if ( dropZone.zoneType == DropZone.DropZoneType.Hand || dropZone.zoneType == DropZone.DropZoneType.Field )
            {
                for ( int i = 0; i < fromLocationObject.transform.childCount; i++ )
                {
                    WixCardComponent cardComponent = fromLocationObject.transform.GetChild(i).gameObject.GetComponent<WixCardComponent>();
                    if ( cardComponent.Card.CardId == cardId )
                    {
                        DestroyObject(fromLocationObject.transform.GetChild(i).gameObject);
                        break;
                    }
                }
            }
        }

        sendRPC = oldSendRPC;
    }

    [PunRPC]
    public void RPCMoveCardsToX(string[] cardIds , int dir , Location fromLocation , Location toLocation)
    {
        bool oldSendRPC = sendRPC;
        sendRPC = false;

        List<GameObject> cardsBeingMoved = new List<GameObject>();
        foreach ( var cardId in cardIds )
        {
            WixossCard cardBeingMoved = null;
            CardCollection.cardCollection.TryGetValue(cardId , out cardBeingMoved);
            if ( cardBeingMoved != null )
                cardsBeingMoved.Add(PoolViewerScript.AddWixossCard(cardBeingMoved , false));
        }

        MoveCards(cardsBeingMoved , ControllerHelper.FindGameObject(fromLocation) , ControllerHelper.FindGameObject(toLocation , true), dir);

        sendRPC = oldSendRPC;
    }

    [PunRPC]
    public void RPCSetOPDeck(string[] cardIds)
    {
        bool oldSendRPC = sendRPC;
        sendRPC = false;

        PoolViewerScript opDeckViewer = ControllerHelper.FindGameObject(Location.Deck , true).GetComponent<PoolViewerScript>();
        opDeckViewer.poolOfCards.Clear();
        foreach ( var cardId in cardIds )
        {
            WixossCard cardBeingAdded = null;
            CardCollection.cardCollection.TryGetValue(cardId , out cardBeingAdded);
            if ( cardBeingAdded != null )
            {
                opDeckViewer.poolOfCards.Add(cardBeingAdded);
            }
        }

        sendRPC = oldSendRPC;
    }

    [PunRPC]
    public void RPCUpdateGameState(GamePhase newGamePhase)
    {
        bool oldSendRPC = sendRPC;
        sendRPC = false;

        GamePhaseCounter.currentPhase = newGamePhase;

        sendRPC = oldSendRPC;
    }

    [PunRPC]
    public void RPCSendFlag(string flagName, bool flagValue)
    {
        switch ( flagName )
        {
            case "OtherPlayerHasMulligend":
                Constants.OtherPlayerHasMulligend = flagValue;
                break;
            case "isMyTurn":
                Constants.isMyTurn = flagValue;
                break;
            default:
                break;
        }

        Debug.Log("Changing flag: " + flagName + " to value " + flagValue);
    }

    [PunRPC]
    public void RPCChangeCardState(CardState newCardState , Location cardLocation)
    {
        foreach ( WixCardComponent WixossCardComponent in GetCardsOnMyOpField() )
        {
            if(WixossCardComponent != null )
            {
                if(WixossCardComponent.GetComponentInParent<PoolViewerScript>() != null )
                {
                    if(cardLocation == WixossCardComponent.GetComponentInParent<PoolViewerScript>().location )
                    {
                        WixossCardComponent.ChangeStateOP(newCardState);
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Quick way to get the photon view for RPC needs
    /// </summary>
    /// <returns></returns>
    public PhotonView getPhotonView()
    {
        PhotonView photonView = PhotonView.Get(this);
        return photonView;
    }

    /// <summary>
    /// Is the location the field
    /// </summary>
    /// <param name="locationToCheck"></param>
    /// <returns>is the location on the field</returns>
    private bool isLocationField(Location locationToCheck)
    {
        return locationToCheck.ToString().Contains("Stage"); //cheap way to find out
    }
    #endregion

}


