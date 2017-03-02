using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using Assets.Game_Controls.Scripts;
using Assets.Utils;
using Assets.Game_Controls.Scripts.Enums;
using Photon;

public class OnLoad : PunBehaviour
{

    private CardCollection cardCollection;

    public CardController cardControler;

    public static List<WixossCard> currentDeck;
    // Use this for initialization
    void Start()
    {
        if ( !cardControler.sendRPC )
        {
            StartDuel();
        }
            if ( PhotonNetwork.playerList.Length == 2 )
        {
            PhotonView view = PhotonView.Get(this);
            if ( cardControler.sendRPC )
            {
                view.RPC(Constants.RPC_StartDuel , PhotonTargets.All , null); //Lets Duel!!!

                cardControler.SendFlagToOp("isMyTurn" , true); //for now if your second to connect, your always second
                Constants.isMyTurn = false;
            }

        }
    }

    [PunRPC]
    public void StartDuel()
    {
        currentDeck = CardCollection.GetSet("WXD-16");
        GamePhaseCounter.currentPhase = GamePhase.FirstTurn;
        PoolViewerScript deckViewer = GameObject.FindGameObjectWithTag("PlayerDeck").GetComponent<PoolViewerScript>();
        deckViewer.poolOfCards = new List<WixossCard>(currentDeck);
        cardControler.ShufflePlayerDeck();
        cardControler.RefreshDeck();
        for ( int i = 0; i < 5; i++ )
        {
            WixossCard cardBeingMoved = cardControler.getPlayerDeckController().poolOfCards[i];
            cardControler.MoveCardShowCard(cardBeingMoved , ControllerHelper.FindGameObject(Location.Deck) , ControllerHelper.FindGameObject(Location.Hand) , i);
            cardControler.RefreshDeck();
        }
    }

    void Update()
    {
    }
}
