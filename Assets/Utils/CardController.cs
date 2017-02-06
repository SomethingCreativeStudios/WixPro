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

    public GameObject Stock;
    public GameObject PlayerDeck;
    public GameObject PlayerHand;
    public GameObject WaitingRoom;
    public GameObject Memory;
    public GameObject Level;
    public GameObject Clock;
    public GameObject EventZone;
    public GameObject Climax;
    public GameObject FrontStageLeft;
    public GameObject FrontStageCenter;
    public GameObject FrontStageRight;
    public GameObject BackStageLeft;
    public GameObject BackStageRight;


    public GameObject Other_Stock;
    public GameObject Other_PlayerDeck;
    public GameObject Other_PlayerHand;
    public GameObject Other_WaitingRoom;
    public GameObject Other_Memory;
    public GameObject Other_Level;
    public GameObject Other_Clock;
    public GameObject Other_EventZone;
    public GameObject Other_Climax;
    public GameObject Other_FrontStageLeft;
    public GameObject Other_FrontStageCenter;
    public GameObject Other_FrontStageRight;
    public GameObject Other_BackStageLeft;
    public GameObject Other_BackStageRight;

    public GameObject PlayerField;
    public GameObject OtherPlayerField;

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
            this.getPhotonView().RPC(Constants.RPC_MoveCardShowCardToX , PhotonTargets.Others , cardBeingMoved.CardId , gameObjectToLocation(initalObject) , gameObjectToLocation(targetObject));
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
                    if (endingViewer.location == Location.PlayerHand )
                    {
                        cardComponent.FlipCard();
                    }

                } else
                {
                    //Don't show cards going to hand, but show hand cards
                    if ( startingViewer.location == Location.PlayerDeck )
                    {
                        cardComponent.FlipCard();

                        if ( endingViewer.location == Location.PlayerHand )
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
                this.getPhotonView().RPC(Constants.RPC_MoveCardToX , PhotonTargets.Others , cardComponent.Card.CardId , gameObjectToLocation(startingObject) , gameObjectToLocation(endingObject));

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
    public void RefreshDeck()
    {
        DeckController playersDeck = PlayerDeck.GetComponent<DeckController>();
        WaitRoomContoller waitingRoom = WaitingRoom.GetComponent<WaitRoomContoller>();

        if ( playersDeck.poolOfCards.Count == 0 ) // check to make sure theres no cards
        {
            waitingRoom.ShufflePool();
            MoveCards(waitingRoom.poolOfGameObjects , WaitingRoom , PlayerDeck , 1);
            MoveCard(PoolViewerScript.AddWixossCard(playersDeck.poolOfCards[0] , false) , PlayerDeck , Clock , 3f);//Change AddWixossCard to take tag parameter, this should alow you to see card
        }
    }

    /// <summary>
    /// Shuffle's player1's deck
    /// </summary>
    public void ShufflePlayerDeck()
    {
        DeckController playersDeck = PlayerDeck.GetComponent<DeckController>();
        playersDeck.ShufflePool();

        SyncDeckWithOp();
    }

    /// <summary>
    /// Get Deck Controller, for easier access. MIGHT REMOVE
    /// </summary>
    public DeckController getPlayerDeckController()
    {
        DeckController playersDeck = PlayerDeck.GetComponent<DeckController>();
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
        WixossCardComponents.Add(FrontStageLeft.GetComponentInChildren<WixCardComponent>());
        WixossCardComponents.Add(FrontStageCenter.GetComponentInChildren<WixCardComponent>());
        WixossCardComponents.Add(FrontStageRight.GetComponentInChildren<WixCardComponent>());

        WixossCardComponents.Add(BackStageLeft.GetComponentInChildren<WixCardComponent>());
        WixossCardComponents.Add(BackStageRight.GetComponentInChildren<WixCardComponent>());
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

        WixossCardComponents.Add(Other_FrontStageLeft.GetComponentInChildren<WixCardComponent>());
        WixossCardComponents.Add(Other_FrontStageCenter.GetComponentInChildren<WixCardComponent>());
        WixossCardComponents.Add(Other_FrontStageRight.GetComponentInChildren<WixCardComponent>());

        WixossCardComponents.Add(Other_BackStageLeft.GetComponentInChildren<WixCardComponent>());
        WixossCardComponents.Add(Other_BackStageRight.GetComponentInChildren<WixCardComponent>());

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
    /// Update the card's power value, also sync to Op
    /// </summary>
    /// <param name="WixossCardComponent"></param>
    /// <param name="attackPowerModValue"></param>
    public void ModCardPower(WixCardComponent WixossCardComponent, int attackPowerModValue)
    {
        WixossCardComponent.Card.PowerBoost = WixossCardComponent.Card.PowerBoost + attackPowerModValue;

        if ( WixossCardComponent.GetComponentInParent<PoolViewerScript>() != null )
        {
            Location cardLocation = WixossCardComponent.GetComponentInParent<PoolViewerScript>().location;

            if ( sendRPC )
                this.getPhotonView().RPC(Constants.RPC_ModCardPower , PhotonTargets.Others , attackPowerModValue , cardLocation);
        }
    }

    /// <summary>
    /// Update the card's soul value, also sync to Op
    /// </summary>
    /// <param name="WixossCardComponent"></param>
    /// <param name="attackPowerModValue"></param>
    public void ModCardSoul(WixCardComponent WixossCardComponent , int soulModValue)
    {
        WixossCardComponent.Card.SoulBoost = WixossCardComponent.Card.SoulBoost + soulModValue;

        if ( WixossCardComponent.GetComponentInParent<PoolViewerScript>() != null )
        {
            Location cardLocation = WixossCardComponent.GetComponentInParent<PoolViewerScript>().location;

            if ( sendRPC )
                this.getPhotonView().RPC(Constants.RPC_ModCardSoul , PhotonTargets.Others , soulModValue , cardLocation);
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
            this.getPhotonView().RPC(Constants.RPC_SetOPDeck, PhotonTargets.Others, PlayerDeck.GetComponent<PoolViewerScript>().getCardIds.ToArray());
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
        GameObject fromLocationObject = locationToGameObjectOP(fromLocation);
        if (cardBeingMoved != null )
        {
            
            GameObject cardObject = PoolViewerScript.AddWixossCard(cardBeingMoved, false);
            MoveCard(cardObject , fromLocationObject , locationToGameObjectOP(toLocation));
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
        GameObject fromLocationObject = locationToGameObjectOP(fromLocation);
        GameObject toLocationObject = locationToGameObjectOP(toLocation);
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

        MoveCards(cardsBeingMoved , locationToGameObjectOP(fromLocation) , locationToGameObjectOP(toLocation) , dir);

        sendRPC = oldSendRPC;
    }

    [PunRPC]
    public void RPCSetOPDeck(string[] cardIds)
    {
        bool oldSendRPC = sendRPC;
        sendRPC = false;

        PoolViewerScript opDeckViewer = Other_PlayerDeck.GetComponent<PoolViewerScript>();
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

        Debug.Log("Changeing flag: " + flagName + " to value " + flagValue);
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

    [PunRPC]
    public void RPCModCardPower(int attackModValue , Location cardLocation)
    {
        foreach ( WixCardComponent WixossCardComponent in GetCardsOnMyOpField() )
        {
            if ( WixossCardComponent != null )
            {
                if ( WixossCardComponent.GetComponentInParent<PoolViewerScript>() != null )
                {
                    if ( cardLocation == WixossCardComponent.GetComponentInParent<PoolViewerScript>().location )
                    {
                        WixossCardComponent.Card.PowerBoost = WixossCardComponent.Card.PowerBoost + attackModValue;
                        break;
                    }
                }
            }
        }
    }

    [PunRPC]
    public void RPCModCardSoul(int soulModValue , Location cardLocation)
    {
        foreach ( WixCardComponent WixossCardComponent in GetCardsOnMyOpField() )
        {
            if ( WixossCardComponent != null )
            {
                if ( WixossCardComponent.GetComponentInParent<PoolViewerScript>() != null )
                {
                    if ( cardLocation == WixossCardComponent.GetComponentInParent<PoolViewerScript>().location )
                    {
                        WixossCardComponent.Card.SoulBoost = WixossCardComponent.Card.SoulBoost + soulModValue ;
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
    /// Given gameObject will return the location enum
    /// </summary>
    /// <param name="gameObject">Object that has a poolscriptviewer</param>
    /// <returns></returns>
    public Location gameObjectToLocation(GameObject gameObject)
    {
        Location location = Location.Level;

        if(gameObject.GetComponent<PoolViewerScript>() != null )
        {
            location = gameObject.GetComponent<PoolViewerScript>().location;
        }

        return location;
    }

    /// <summary>
    /// Based on location return the gameObject. This is for player 1 fields
    /// </summary>
    /// <param name="location">The location you want</param>
    /// <returns></returns>
    public GameObject locationToGameObject(Location location)
    {
        GameObject gameObject = null;
        switch ( location )
        {
            case Location.Stock:
                gameObject = Stock;
                break;
            case Location.PlayerDeck:
                gameObject = PlayerDeck;
                break;
            case Location.PlayerHand:
                gameObject = PlayerHand;
                break;
            case Location.WaitingRoom:
                gameObject = WaitingRoom;
                break;
            case Location.Memory:
                gameObject = Memory;
                break;
            case Location.Level:
                gameObject = Level;
                break;
            case Location.Clock:
                gameObject = Clock;
                break;
            case Location.Event:
                gameObject = EventZone;
                break;
            case Location.Climax:
                gameObject = Climax;
                break;
            case Location.FrontStageCenter:
                gameObject = FrontStageCenter;
                break;
            case Location.FrontStageLeft:
                gameObject = FrontStageLeft;
                break;
            case Location.FrontStageRight:
                gameObject = FrontStageRight;
                break;
            case Location.BackStageLeft:
                gameObject = BackStageLeft;
                break;
            case Location.BackStageRight:
                gameObject = BackStageRight;
                break;
            default:
                break;
        }

        return gameObject;
    }

    /// <summary>
    /// Based on location return the gameObject. This is for player 2 fields
    /// </summary>
    /// <param name="location">The location you want</param>
    /// <returns></returns>
    public GameObject locationToGameObjectOP(Location location)
    {
        GameObject gameObject = null;
        switch ( location )
        {
            case Location.Stock:
                gameObject = Other_Stock;
                break;
            case Location.PlayerDeck:
                gameObject = Other_PlayerDeck;
                break;
            case Location.PlayerHand:
                gameObject = Other_PlayerHand;
                break;
            case Location.WaitingRoom:
                gameObject = Other_WaitingRoom;
                break;
            case Location.Memory:
                gameObject = Other_Memory;
                break;
            case Location.Level:
                gameObject = Other_Level;
                break;
            case Location.Clock:
                gameObject = Other_Clock;
                break;
            case Location.Event:
                gameObject = Other_EventZone;
                break;
            case Location.Climax:
                gameObject = Other_Climax;
                break;
            case Location.FrontStageCenter:
                gameObject = Other_FrontStageCenter;
                break;
            case Location.FrontStageLeft:
                gameObject = Other_FrontStageLeft;
                break;
            case Location.FrontStageRight:
                gameObject = Other_FrontStageRight;
                break;
            case Location.BackStageLeft:
                gameObject = Other_BackStageLeft;
                break;
            case Location.BackStageRight:
                gameObject = Other_BackStageRight;
                break;
            default:
                break;
        }

        return gameObject;
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


