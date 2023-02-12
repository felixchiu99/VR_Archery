using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public List<ScoreBoardLine> lines = new List<ScoreBoardLine>();

    private int index = 0;
    private int total = 0;

    public TextMeshProUGUI totalUI;

    public EquipmentProperties equipment;

    void Start()
    {
        totalUI.text = total.ToString();
    }

    public void AddPoint(int point, GameObject arrow)
    {
        if (point <= 0)
            point = 0;

        if (lines.Count > index)
        {
            lines[index].AddPoint(point, arrow);
            if (lines[index].stored >= 12)
            {
                index++;
            }
        }
        UpdateTotal();
    }

    public void UpdateTotal()
    {
        total = 0;
        foreach (ScoreBoardLine line in lines)
        {
            total += line.subTotal;
        }
        totalUI.text = total.ToString();
    }

    public void ReturnToMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ClearAllLine()
    {
        foreach (ScoreBoardLine line in lines)
        {
            line.ClearLine();
        }
    }

    public void ResetBow()
    {
        equipment.ReturnBow();
    }

    public void Finished()
    {

    }
}
