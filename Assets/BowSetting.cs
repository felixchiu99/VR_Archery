using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BowGripType
{
    Left,
    Right
}

public class BowSetting : MonoBehaviour
{
    public BowGripType gripType = BowGripType.Right;

    public GameObject leftGrip;
    public GameObject rightGrip;

    public GameObject leftNockingPoint;
    public GameObject rightNockingPoint;

    public Transform bowBody;

    private Nocking leftNocking;
    private Nocking rightNocking;

    // Start is called before the first frame update
    void Start()
    {
        SetHand();
    }

    public void SetHand()
    {
        Autohand.GrabbablePose nockLeftGrabbablePose = leftNockingPoint.transform.Find("Gripping_Point/Pose_bow").gameObject.GetComponent<Autohand.GrabbablePose>();
        Autohand.GrabbablePose leftGrabbablePose = leftGrip.GetComponent<Autohand.GrabbablePose>();
        Autohand.GrabbablePose nockRightGrabbablePose = rightNockingPoint.transform.Find("Gripping_Point/Pose_bow").gameObject.GetComponent<Autohand.GrabbablePose>();
        Autohand.GrabbablePose rightGrabbablePose = rightGrip.GetComponent<Autohand.GrabbablePose>();

        leftNocking = leftNockingPoint.transform.Find("Gripping_Point/L Nocking point/Nocking_collider").gameObject.GetComponent<Nocking>();
        rightNocking = leftNockingPoint.transform.Find("Gripping_Point/L Nocking point/Nocking_collider").gameObject.GetComponent<Nocking>();

        if (gripType == BowGripType.Left)
        {
            //flip bow body
            bowBody.localScale = new Vector3(-1, 1, 1);

            //enable left
            leftGrip.SetActive(true);
            leftNockingPoint.SetActive(true);

            nockLeftGrabbablePose.poseEnabled = true;
            leftGrabbablePose.poseEnabled = true;

            //disable Right
            rightGrip.SetActive(false);
            rightNockingPoint.SetActive(false);

            nockRightGrabbablePose.poseEnabled = false;
            rightGrabbablePose.poseEnabled = false;
        }

        if (gripType == BowGripType.Right)
        {
            //flip bow body
            bowBody.localScale = new Vector3(1, 1, 1);

            //disable left
            leftGrip.SetActive(false);
            leftNockingPoint.SetActive(false);

            nockLeftGrabbablePose.poseEnabled = false;
            leftGrabbablePose.poseEnabled = false;

            //enable Right
            rightGrip.SetActive(true);
            rightNockingPoint.SetActive(true);

            nockRightGrabbablePose.poseEnabled = true;
            rightGrabbablePose.poseEnabled = true;

        }
    }
    public Nocking GetNocking()
    {
        if (gripType == BowGripType.Left)
        {
            return leftNocking;
        }
        if (gripType == BowGripType.Right)
        {
            return rightNocking;
        }
        return leftNocking;
    }
}
