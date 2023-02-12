using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine.Serialization;
using Autohand;

public class InventoryTest : MonoBehaviour
{
    public List<GameObject> storedList = new List<GameObject>();
    public Transform spawnPoint;

    internal Rigidbody spawnedRigidbody;
    internal GameObject spawnedObject;

    public virtual void SpawnIntoHand(Hand targetHand)
    {
        //If hand is empty put item into it.
        if (targetHand.holdingObj == null)
        {
            //Instantiate item to grab.
            GameObject newItem = Instantiate(storedList[storedList.Count - 1]);
            targetHand.Release();
            StartCoroutine(tryGrab(targetHand, newItem));
        }
    }

    protected IEnumerator tryGrab(Hand targetHand, GameObject newItem)
    {
        yield return null;
        //Find width to offset prefab in front of palm.
        Renderer renderer = newItem.GetComponent<Renderer>();
        if (renderer == null)
        {
            renderer = newItem.GetComponentInChildren<Renderer>();
        }
        float prefabWidth = renderer.bounds.size.x / 2;
        //(targetHand.palmTransform.forward * (prefabWidth + targetHand.reachDistance / 2))
        Vector3 offsetPos = targetHand.palmTransform.position ;
        //Position in front of palm.
        newItem.transform.position = offsetPos;
        newItem.transform.rotation = targetHand.palmTransform.rotation;

        //Release (hopefully resets grabbing and other bools.)
        targetHand.Release();
        targetHand.TryGrab(newItem.GetComponent<Grabbable>());
    }
}
