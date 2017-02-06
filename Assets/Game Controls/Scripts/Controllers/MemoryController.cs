using UnityEngine;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using Assets.Game_Controls.Scripts.Enums;

public class MemoryController : PoolViewerScript {

    // Use this for initialization
    public override void StartUp() {
        menuItems = Constants.memoryMenu;
    }

    // Update is called once per frame
    public override void UpdateScript() {

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
            List<string> viewMenu = new List<string>(Constants.memoryMenu);
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


        if (menuName == menuItems[0]) //This is send to X
        {
            DialogScripts.ShowDropDownDialog(new List<string>(Constants.sendToMenu), "Memory", "Move your card.", (selectedItem) => { sendToX(selectedItem); }, null);
        }
    }

}
