﻿using UnityEngine;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;

public class EnterController : PoolViewerScript
{

    // Use this for initialization
    public override void StartUp()
    {
        menuItems = MenuHelper.MenuToArray<EnerMenu>();
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
            dragger.realParent = gameObject;
            poolViewer.transform.SetParent(parentCanvas.transform, false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            GameObject contextMenu = (GameObject)Instantiate(Resources.Load("ContextMenu"));
            ContextMenuScript menu = contextMenu.GetComponent<ContextMenuScript>();
            List<string> viewMenu = new List<string>(menuItems);
            menuItems = viewMenu.ToArray();
            menu.SetUpContextMenu(viewMenu);//new List<string>(menuItems));
            menu.clicked += Menu_clicked;
            contextMenu.name = "Pool Viewer Context Menu";
            contextMenu.transform.SetParent(parentCanvas, false);
            contextMenu.transform.position = Input.mousePosition;
        }
    }

    public override void Menu_clicked(string menuName)
    {
        GameObject WixossCard = AddWixossCard(poolOfCards[0]);

        if ( menuName == GetMenuItem(TrashMenu.SendToX) )
        {
            DialogScripts.ShowDropDownDialog(new List<string>(MenuHelper.MenuToArray<SendToMenu>()) , "Memory", "Move your card.", (selectedItem) => { sendToX(selectedItem); }, null);
        }
    }
}

