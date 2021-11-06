using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class UIData : MonoBehaviour
{
    public Text gameModeText;
    public bool isPathComplete;
    public bool isPathConfirmed;
    public bool clearPathRequest;
    public bool buyRequest;
    public bool sellRequest;
    public GameObject confirmButton;
    public GameObject clearButton;
    public GameObject buyButton;
    public GameObject sellButton;
    public Text fuelText;
    public Text durabilityText;
    public Text moneyText;
    public Text cargoText;
    public List<GameObject> playerInfoPanel;
    public  List<GameObject> buttons;
    public static bool IsMouseOverButton(List<GameObject> buttons)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        foreach (var obj in raycastResults)
        {
            foreach (var button in buttons)
            {
                if (button == obj.gameObject)
                {
                    return true;
                }
            }
        }
        return false;
    }




}