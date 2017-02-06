using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;
using System.Threading;

public class ContextMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public List<string> menuItems;
    bool mouseOver = false;
    public event ClickHandler clicked;
    public delegate void ClickHandler(String menuName);
    // Use this for initialization
    void Start () {
        foreach (Transform child in this.transform.parent)
        {
            if (child.tag == "Context Menu" && child != transform)
                Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// This will create a new ContextMenu
    /// </summary>
    /// <param name="menuItems">The choices a user can pick</param>
    public void SetUpContextMenu(List<string> menuItems)
    {
        foreach (string menuName in menuItems)
        {
            GameObject newObject = (GameObject)Instantiate(Resources.Load("ContextButton"));
            Button newButton = newObject.GetComponent<Button>();
            String tempString = menuName.Clone().ToString();
            newButton.onClick.AddListener(delegate {ButtonClicked(tempString); });
            newButton.GetComponentInChildren<Text>().text = menuName;
            newButton.transform.SetParent(this.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!mouseOver)
            {
                removeContextMenu();
            }
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

    public void ButtonClicked(string menuName)
    {
        if (clicked != null)
        {
            clicked.Invoke(menuName);
            removeContextMenu();
        }
    }

    private void removeContextMenu()
    {
        foreach (Transform child in this.transform.parent)
        {
            if (child.tag == "Context Menu" && child == transform)
                Destroy(child.gameObject);
        }
    }
}
