using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Autohand;

public enum QuiverType
{
    Left,
    Right
}

public class QuiverProperties : InventoryTest
{
    public QuiverType gripType = QuiverType.Right;

    public float arrowLength = 0.7f;

    public float xOffset = 0.3f;

    public float yOffset = -0.6f;

    public float zOffset = 0.01f;

    void LateUpdate()
    {
        transform.position = transform.parent.position + new Vector3(0, yOffset, 0);
        if (gripType == QuiverType.Left)
        {
            transform.localPosition += new Vector3(-xOffset, 0, zOffset);
        }
        if (gripType == QuiverType.Right)
        {
            transform.localPosition += new Vector3(xOffset, 0, zOffset);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public override void SpawnIntoHand(Hand targetHand)
    {
        //If hand is empty put item into it.
        if (targetHand.holdingObj == null)
        {
            //Instantiate item to grab.
            GameObject newItem = Instantiate(storedList[storedList.Count - 1]);
            ArrowProperties a = newItem.GetComponent<ArrowProperties>();
            a.shaftLength = arrowLength;
            a.initalArrow();

            targetHand.Release();
            StartCoroutine(tryGrab(targetHand, newItem));
        }
    }
}
