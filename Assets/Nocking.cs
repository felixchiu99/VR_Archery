using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Nocking : MonoBehaviour
{
    public Transform arrow_rest;
    public Transform nocking_point;
    public Rigidbody string_rb;
    public Rigidbody bow;

    private ConfigurableJoint stringJoint;
    private ConfigurableJoint bowJoint;

    private Autohand.Grabbable grabbable_object;

    public GameObject arrow;

    public float normalDrawDistance = 0.25f;

    public bool nocked = false;

    private ArrowProperties arrowProp;

    void Start()
    {
        normalDrawDistance = Vector3.Distance(arrow_rest.position, nocking_point.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (other.tag == "Nock" && !nocked)
        {
            NockArrow(other);
        }

    }

    private void NockArrow(Collider other)
    {
        //release hand
        GameObject collideObject = other.gameObject;
        if (other.attachedRigidbody != null)
        {
            collideObject = other.attachedRigidbody.gameObject;
        }
        if (collideObject.TryGetComponent(out grabbable_object))
        {
            grabbable_object.ForceHandsRelease();
            grabbable_object.enabled = false;
        }
        arrow = collideObject;
        collideObject.TryGetComponent(out arrowProp);

        //rotate
        Quaternion rotation = Quaternion.LookRotation(arrow_rest.position - nocking_point.position, Vector3.up);
        collideObject.transform.rotation = rotation;
        collideObject.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);

        //tp to mid place
        collideObject.transform.position = arrow_rest.position;

        //add bow joint
        bowJoint = collideObject.AddComponent<ConfigurableJoint>();
        bowJoint.connectedBody = bow;
        bowJoint.xMotion = ConfigurableJointMotion.Locked;
        bowJoint.yMotion = ConfigurableJointMotion.Limited;
        bowJoint.zMotion = ConfigurableJointMotion.Locked;

        bowJoint.angularXMotion = ConfigurableJointMotion.Locked;
        bowJoint.angularYMotion = ConfigurableJointMotion.Locked;
        bowJoint.angularZMotion = ConfigurableJointMotion.Locked;

        SoftJointLimit linearLimit = new SoftJointLimit();
        linearLimit.limit = arrowProp.shaftLength * 0.5f + 0.2f;
        linearLimit.bounciness = 0.1f;
        bowJoint.linearLimit = linearLimit;

        bowJoint.breakForce = 200;
        bowJoint.breakTorque = 200;

        //tp to nocking position
        float arrowEnd = Vector3.Distance(collideObject.transform.position - collideObject.transform.up * (arrowProp.shaftLength*0.5f), nocking_point.position);
        if((arrowProp.shaftLength * 0.5f) > normalDrawDistance)
            collideObject.transform.position += collideObject.transform.up * arrowEnd;
        else    
            collideObject.transform.position -= collideObject.transform.up * arrowEnd;

        //add string joint
        stringJoint = collideObject.AddComponent<ConfigurableJoint>();
        stringJoint.connectedBody = string_rb;
        stringJoint.xMotion = ConfigurableJointMotion.Locked;
        stringJoint.yMotion = ConfigurableJointMotion.Locked;
        stringJoint.zMotion = ConfigurableJointMotion.Locked;

        stringJoint.breakForce = 200;
        stringJoint.breakTorque = 200;

        nocked = true;

        collideObject.GetComponent<ArrowProperties>().nockingScript = gameObject.GetComponent<Nocking>();
    }

    void Update()
    {
        //check Length
        if (nocked && arrowProp)
        {
            float currentDraw = Vector3.Distance(arrow_rest.position, nocking_point.position);
            if (currentDraw > arrowProp.shaftLength)
            {
                UnNockArrow();
            }
        }
    }

    public void UnNockArrow()
    {
        nocked = false;
        if(stringJoint != null)
        {
            Destroy(stringJoint);
            stringJoint = null;
        }
        if (bowJoint != null)
        {
            Destroy(bowJoint);
            bowJoint = null;
        }
        if (grabbable_object != null)
        {
            grabbable_object.enabled = true;
            grabbable_object = null;
        }
        arrowProp = null;
    }
}
