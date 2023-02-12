using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRelease : MonoBehaviour
{
    public GameObject arrowRest;
    public GameObject nocking_Point;
    public Nocking nockingScript;

    protected float releaseForce = 129.0f;

    public float maxDrawDistance = 0.75f;

    void Start()
    {
        Physics.IgnoreLayerCollision(25, 26);
        Physics.IgnoreLayerCollision(25, 29);
    }

    public void OnRelease()
    {
        //find distance
        float drawDistance = Vector3.Distance(arrowRest.transform.position, nocking_Point.transform.position);
        if (nockingScript.nocked && (drawDistance - nockingScript.normalDrawDistance) > 0.2f)
            ReleaseArrow(drawDistance);
    }

    public void ReleaseArrow(float drawDistance)
    {
        //unlink arrow from nock
        nockingScript.UnNockArrow();

        //find distance
        float powerScale = drawDistance / maxDrawDistance;

        //launch arrow
        nockingScript.arrow.transform.position = nockingScript.arrow_rest.position;
        nockingScript.arrow.transform.position += nockingScript.arrow.transform.up * nockingScript.arrow.transform.lossyScale.y * 0.6f; ;
        nockingScript.arrow.GetComponent<Rigidbody>().AddForce(nockingScript.arrow.transform.up* releaseForce * powerScale, ForceMode.Force);
        nockingScript.arrow.GetComponent<ArrowProperties>().shot = true;

        //reset 
        nockingScript.arrow = null;
        nockingScript.nocked = false;
    }
}
