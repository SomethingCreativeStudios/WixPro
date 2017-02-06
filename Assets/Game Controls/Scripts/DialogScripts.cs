using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public enum DialogType
{
    MessageDialog,
    DropDownDialog,
    CardDialog,
    NumDialog
}
public class DialogScripts : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{ 

    public List<string> dropDownOptions;
    bool mouseOver = false;
    public event ClickHandler okClicked;
    public event ClickHandler canceledClicked;

    public event CardClickHandler okCardClicked;
    public event CardClickHandler canceledCardClicked;

    public delegate void ClickHandler(string selectedItem);
    public delegate void CardClickHandler(List<WixossCard> selectedItem);
    public DialogType typeOfDialog = DialogType.DropDownDialog;

    public int increment = 1;

    Vector3 startMousePosition, startPosition;

    // Use this for initialization
    void Start () {
	    
	}

    private static GameObject SetUpDropDownMenu(List<string> dropDownItems, string title, string message)
    {
        GameObject dialogBox = (GameObject)Instantiate(Resources.Load("DropdownDialog"));
        Dropdown dropDown = dialogBox.GetComponentInChildren<Dropdown>();
        Text[] textObjects = dialogBox.GetComponentsInChildren<Text>();

        dropDown.options.Clear();

        Text text = dropDown.GetComponentInChildren<Text>();
        text.text = dropDownItems[0];

        foreach (var dropDownItem in dropDownItems)
        {
            dropDown.options.Add(new Dropdown.OptionData(dropDownItem));
        }

        foreach (var item in textObjects)
        {
            if(item.name == "Title")
            {
                item.text = title;
            }

            if(item.name == "Message")
            {
                item.text = message;
            }
        }

        return dialogBox;
    }

    /// <summary>
    /// This will present a dialog with a drop box to the user
    /// Ex: DialogScripts.ShowDropDownDialog(new List<string>(new string[] { "TEST", "TESTER" }), "Title Test", "Message Test", (selectedItem) => { Debug.Log("TEST " + selectedItem);  }, null);
    /// </summary>
    /// <param name="dropDownItems">What's in the drop down box</param>
    /// <param name="title">The title of the message</param>
    /// <param name="message">The message itself</param>
    /// <param name="okClicked">The method that is called when ok is pressed. Can be null</param>
    /// <param name="canceledClicked">The method that is called when cancel is pressed. Can be null</param>
    public static void ShowDropDownDialog(List<string> dropDownItems, string title, string message, ClickHandler okClicked, ClickHandler canceledClicked)
    {
        GameObject dialogBox = SetUpDropDownMenu(dropDownItems, title, message);
        GameObject mainField = GameObject.FindGameObjectWithTag("Main Field");
        DialogScripts dds = dialogBox.GetComponent<DialogScripts>();

        dds.canceledClicked += canceledClicked;
        dds.okClicked += okClicked;

        dialogBox.transform.position = new Vector3(mainField.transform.position.x / 2, mainField.transform.position.y / 2, 0);
        dialogBox.transform.SetParent(mainField.transform, false);

        // How to use (in one line)
        // DialogScripts.ShowDropDownDialog(new List<string>(new string[] { "TEST", "TESTER" }), "Title Test", "Message Test", (selectedItem) => { Debug.Log("TEST " + selectedItem);  }, null);
    }

    /// <summary>
    /// This will present a message dialog to the user
    /// </summary>
    /// <param name="title">The title of the message</param>
    /// <param name="message">The message itself</param>
    public static void ShowMessageDialog(string title, string message)
    {
        GameObject dialogBox = (GameObject)Instantiate(Resources.Load("MessageDialog"));
        GameObject mainField = GameObject.FindGameObjectWithTag("Main Field");
        Text[] textObjects = dialogBox.GetComponentsInChildren<Text>();

        foreach (var item in textObjects)
        {
            if (item.name == "Title")
            {
                item.text = title;
            }

            if (item.name == "Message")
            {
                item.text = message;
            }
        }

        dialogBox.transform.position = new Vector3(mainField.transform.position.x / 2, mainField.transform.position.y / 2, 0);
        dialogBox.transform.SetParent(mainField.transform, false);
    }

    /// <summary>
    /// Show a dialog box with a number combo box control
    /// </summary>
    /// <param name="title">Title of the dialog box</param>
    /// <param name="incrementValue">How much is the default up or down</param>
    /// <param name="okClicked">What happenes when "Ok" is pressed</param>
    public static void ShowNumDialog(string title, int incrementValue, ClickHandler okClicked)
    {
        GameObject dialogBox = (GameObject)Instantiate(Resources.Load("NumDialog"));
        GameObject mainField = GameObject.FindGameObjectWithTag("Main Field");
        Text[] textObjects = dialogBox.GetComponentsInChildren<Text>();

        foreach (var item in textObjects)
        {
            if (item.name == "Title")
            {
                item.text = title;
            }
        }

        DialogScripts dds = dialogBox.GetComponent<DialogScripts>();

        dds.okClicked += okClicked;
        dds.increment = incrementValue;


        dialogBox.transform.position = new Vector3((mainField.transform.position.x / 2) - 250, (mainField.transform.position.y / 2) - 100, 0);
        dialogBox.transform.SetParent(mainField.transform, false);
    }

    /// <summary>
    /// This will present a dialog box to place cards in or out of
    /// </summary>
    /// <param name="title">The title of the message</param>
    /// <param name="message">The message itself</param>
    /// <param name="okClicked">The method that is called when ok is pressed. Can be null</param>
    /// <param name="canceledClicked">The method that is called when cancel is pressed. Can be null</param>
    public static void ShowCardDialog(string title, string message, CardClickHandler okClicked, CardClickHandler canceledClicked)
    {
        GameObject dialogBox = (GameObject)Instantiate(Resources.Load("CardDialogBox"));

        GameObject playerField = GameObject.FindGameObjectWithTag("PlayerField");
        GameObject mainField = GameObject.FindGameObjectWithTag("Main Field");

        Text[] textObjects = dialogBox.GetComponentsInChildren<Text>();

        foreach (var item in textObjects)
        {
            if (item.name == "Title")
            {
                item.text = title;
            }

            if (item.name == "Message")
            {
                item.text = message;
            }
        }

        DialogScripts dds = dialogBox.GetComponent<DialogScripts>();

        dds.canceledCardClicked += canceledClicked;
        dds.okCardClicked += okClicked;

        dialogBox.transform.position = new Vector3(0, 400, 0);
        dialogBox.transform.SetParent(playerField.transform, false);
    }

    public void OkButtonPressed()
    {
        if (okClicked != null)
        {
            string returnMessage = "OkBtn Pressed";

            if (typeOfDialog == DialogType.DropDownDialog)
                returnMessage = this.GetComponentInChildren<Dropdown>().GetComponentInChildren<Text>().text;
            if(typeOfDialog == DialogType.NumDialog)
                returnMessage = this.GetComponentInChildren<InputField>().text;

            okClicked.BeginInvoke(returnMessage, null, null);
        }

        Destroy(this.gameObject);
    }

    public void CanButtonPressed()
    {
        if (canceledClicked != null)
        {
            canceledClicked.BeginInvoke("NONE", null, null);
        }

        Destroy(this.gameObject);
    }

    public void OkCardButtonPressed()
    {
        if (okCardClicked != null)
        {
            List<WixossCard> cardsInBox = new List<WixossCard>();

            WixCardComponent[] cardComponents = gameObject.GetComponentsInChildren<WixCardComponent>();

            foreach (var cardComponent in cardComponents)
            {
                cardsInBox.Add(cardComponent.Card);
            }


            okCardClicked.BeginInvoke(cardsInBox, null, null);
        }

        Destroy(this.gameObject);
    }

    public void CanCardButtonPressed()
    {
        if (canceledCardClicked != null)
        {
            canceledCardClicked.BeginInvoke(null, null, null);
        }

        Destroy(this.gameObject);
    }

    public void UpButtonPressed()
    {
        InputField textValue = this.GetComponentInChildren<InputField>();
        String currentValueStr = textValue.text;
        int currentValue = 0;
        try
        {
            currentValue = Convert.ToInt32(currentValueStr);
            currentValue += increment;
        }
        catch
        {

        }

        textValue.text = currentValue.ToString();
    }

    public void DownButtonPressed()
    {
        InputField textValue = this.GetComponentInChildren<InputField>();
        String currentValueStr = textValue.text;
        int currentValue = 0;
        try
        {
            currentValue = Convert.ToInt32(currentValueStr);
            currentValue -= increment;
        }
        catch
        {

        }

        textValue.text = currentValue.ToString();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentPosition = Input.mousePosition;

        Vector3 diff = currentPosition - startMousePosition;

        Vector3 pos = startPosition + diff;

        this.transform.position = pos;
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
