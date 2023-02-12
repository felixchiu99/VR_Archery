using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoardLine : MonoBehaviour
{
    public List<GameObject> arrows = new List<GameObject>();

    public TextMeshProUGUI lineUI;
    public TextMeshProUGUI subTotalUI;

    public int subTotal = 0;
    public int stored = 0;

    void Start()
    {
        lineUI = gameObject.transform.Find("Line").gameObject.GetComponent<TextMeshProUGUI>();
        subTotalUI = gameObject.transform.Find("SubTotal").gameObject.GetComponent<TextMeshProUGUI>();

        subTotal = 0;
        lineUI.text = "<mspace=0.6em>";
        subTotalUI.text = subTotal.ToString();
    }

    public void AddPoint(int point, GameObject arrow)
    {
        stored++;

        subTotal += point;
        if (point != 10)
            lineUI.text += " ";
        if (point != 0)
            lineUI.text += " " + point.ToString();
        else
            lineUI.text += " M";

        subTotalUI.text = subTotal.ToString();
        arrows.Add(arrow);
    }
    public void ClearLine()
    {
        stored = 0;
        subTotal = 0;
        lineUI.text = "<mspace=0.6em>";
        subTotalUI.text = subTotal.ToString();
        foreach(GameObject arrow in arrows)
        {
            Destroy(arrow);
        }
        arrows.RemoveAll(s => s == null);
    }
}
