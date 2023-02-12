using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneProperties : MonoBehaviour
{
    public GameObject score;
    public GameObject target;

    public ScoreBoard scoreBoard;
    public Transform targetMidPt;

    void Start()
    {
        targetMidPt = target.transform.Find("Mesh/X_point");
        scoreBoard = score.GetComponent<ScoreBoard>();
    }

}
