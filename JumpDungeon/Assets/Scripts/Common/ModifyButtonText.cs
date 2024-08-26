using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModdifyButtonText : MonoBehaviour
{
    public TextMeshProUGUI BtnTxt;

    public void OnClickBtn()
    {
        if (!gameObject.GetComponent<Button>().IsInteractable()) return;
        BtnTxt.transform.localPosition = new Vector3(BtnTxt.transform.localPosition.x, BtnTxt.transform.localPosition.y - 10f, BtnTxt.transform.localPosition.z);
    }

    public void OffClickBtn()
    {
        if (!gameObject.GetComponent<Button>().IsInteractable()) return;
        BtnTxt.transform.localPosition = new Vector3(BtnTxt.transform.localPosition.x, BtnTxt.transform.localPosition.y + 10f, BtnTxt.transform.localPosition.z);
    }
}
