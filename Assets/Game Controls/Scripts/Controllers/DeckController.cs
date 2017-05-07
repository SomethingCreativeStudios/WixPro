using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.Utils;
using System;
using Assets.Game_Controls.Scripts;
using Assets.Game_Controls.Scripts.Enums;
using CodeBureau;

public class DeckController : PoolViewerScript
{

    private bool shuffleDeck = false;
    // Use this for initialization
    public override void StartUp()
    {
        menuItems = MenuHelper.MenuToArray<DeckMenu>();
        ShufflePool();
    }

    // Update is called once per frame
    public override void UpdateScript()
    {
        if (shuffleDeck)
        {
           //Handle Animation to shuffle
        }

        if ( Constants.isMyTurn )
        {
            if(GamePhaseCounter.currentPhase == GamePhase.DrawPhase )
            {
                if ( !Constants.hasDrawnCard )
                {
                    Constants.hasDrawnCard = true;
                    cardController.MoveCardShowCard(poolOfCards[0] , ControllerHelper.FindGameObject(Location.Deck) , ControllerHelper.FindGameObject(Location.Hand) , 0);
                }

                cardController.UpdateGamePhase(GamePhase.ClockPhase);
            }
        }
    }


    public override void MenuClicked()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject contextMenu = (GameObject)Instantiate(Resources.Load("ContextMenu"));
            ContextMenuScript menu = contextMenu.GetComponent<ContextMenuScript>();
            List<string> viewMenu = new List<string>(menuItems);
            menuItems = viewMenu.ToArray();
            menu.SetUpContextMenu(viewMenu);//new List<string>(menuItems));
            menu.clicked += Menu_clicked;
            contextMenu.name = "Pool Viewer Context Menu";
            //poolViewer.AddComponent<CanvasRenderer>();
            contextMenu.transform.SetParent(parentCanvas, false);
            contextMenu.transform.position = Input.mousePosition;
        }
    }

    public override void Menu_clicked(string menuName)
    {
        //GameObject WixossCard = AddWixossCard(poolOfCards[0]);
        WixossCard cardBeingMoved = null;

        if(poolOfCards.Count > 0)
            cardBeingMoved = poolOfCards[0];

        if (menuName == StringEnum.GetStringValue(DeckMenu.DrawCard))
        {
           
            cardController.RefreshDeck();
            cardController.MoveCardShowCard(cardBeingMoved, ControllerHelper.FindGameObject(Location.Deck) , ControllerHelper.FindGameObject(Location.Hand) , 0);
            cardController.RefreshDeck();
        }

        if ( menuName == StringEnum.GetStringValue(DeckMenu.MillTopCard))
          {
            cardController.RefreshDeck();
            cardController.MoveCard(AddWixossCard(cardBeingMoved , false), ControllerHelper.FindGameObject(Location.Deck) , ControllerHelper.FindGameObject(Location.TrashZone));
            cardController.RefreshDeck();
        }

        if ( menuName == StringEnum.GetStringValue(DeckMenu.ShuffleDeck))
        {
            shuffleDeck = true;
            cardController.ShufflePlayerDeck(); //This auto syncs the deck to op
        }

        if ( menuName == StringEnum.GetStringValue(DeckMenu.ViewDeck))
        {
            GameObject poolViewer = (GameObject)Instantiate(Resources.Load("PoolViewer"));
            poolViewer.name = gameObject.GetComponent<DropZone>().zoneType.ToString() + " Viewer";
            PanelDragger dragger = poolViewer.GetComponent<PanelDragger>();
            dragger.poolOfCards = new List<WixossCard>(poolOfCards);
            //poolViewer.AddComponent<CanvasRenderer>();
            dragger.realParent = gameObject;
            poolViewer.transform.SetParent(parentCanvas.transform, false);
        }
    }

   
}
