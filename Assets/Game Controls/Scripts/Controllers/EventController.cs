using UnityEngine;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
public class EventController : PoolViewerScript
{

    // Use this for initialization
    public override void StartUp()
    {
        menuItems = Constants.eventMenu;
    }

    // Update is called once per frame
    public override void UpdateScript()
    {

    }


    public override void MenuClicked()
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
            List<string> viewMenu = new List<string>(Constants.eventMenu);
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
        GameObject WixossCard = AddWixossCard(poolOfCards[0]);

        if (menuName == menuItems[0])
        {
            WixossCard.transform.SetParent(cardController.PlayerField.transform, false); //makes card visable to player
            cardController.MoveCard(WixossCard, cardController.Memory, cardController.Stock);
        }
    }
}
