using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProperties : MonoBehaviour
{
    public bool shot = false;
    public float shaftLength = 1.0f;

    public GameObject fletching;
    public GameObject head;
    public GameObject shaft;

    public Nocking nockingScript; 

    public Transform target;

    public ScoreBoard score;

    public SceneProperties sceneProperties;

    // Start is called before the first frame update
    void Start()
    {
        initalArrow();
    }

    public void initalArrow()
    {
        shaft.transform.localScale = new Vector3(1, 1, shaftLength);
        fletching.transform.localPosition = new Vector3(0, -shaftLength * 0.5f + 0.05f, 0);
        head.transform.localPosition = new Vector3(0, shaftLength * 0.5f, 0);
        sceneProperties = GameObject.Find("SceneProperties").GetComponent<SceneProperties>();
        score = sceneProperties.scoreBoard;
        target = sceneProperties.targetMidPt;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (shot)
        {
            if (collision.gameObject.layer != 26)
                StickToObject(collision);
        }

    }
    void StickToObject(Collision collision)
    {
        //stick to wall
        GameObject wall = collision.gameObject;
        GameObject childOb = new GameObject("scaling");
        childOb.transform.SetParent(wall.transform, true);
        gameObject.transform.SetParent(childOb.transform, true);
        gameObject.transform.localPosition += new Vector3(0.05f, 0, 0);

        gameObject.GetComponent<Autohand.Grabbable>().enabled = false;

        Destroy(gameObject.GetComponent<Rigidbody>());

        Vector3 intersectPoint = gameObject.transform.position + gameObject.transform.up * (shaftLength * 0.5f - 0.05f);
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = intersectPoint;
        sphere.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        Destroy(sphere, 2);

        float dist = Vector3.Distance(intersectPoint, target.position);
        dist *= 20;
        GameObject childObj = new GameObject("scaling");
        childObj.transform.position = intersectPoint;
        childObj.transform.SetParent(wall.transform, true);

        score.AddPoint((10 - (int)dist), gameObject);
    }

    void OnJointBreak(float breakForce)
    {
        if (nockingScript)
        {
            nockingScript.UnNockArrow();
        }
    }
}
