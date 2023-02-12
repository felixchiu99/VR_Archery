using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Left,
    Right
}

public class EquipmentProperties : MonoBehaviour
{
    public EquipmentType equipmentType = EquipmentType.Right;
    public QuiverProperties quiver;
    public GameObject bowPrefab;

    public Transform bowSpawnPt;

    private GameObject bow;

    private BowSetting bowSetting;
    
    void Start()
    {
        if(PlayerControllerProperties.CrossSceneInformation == "Right")
        {
            equipmentType = EquipmentType.Right;
        }
        if (PlayerControllerProperties.CrossSceneInformation == "Left")
        {
            equipmentType = EquipmentType.Left;
        }
        bow = Instantiate(bowPrefab, transform.position, transform.rotation);
        bowSetting = bow.GetComponent<BowSetting>();
        SetHand();
        ReturnBow();
    }

    void SetHand()
    {
        if (equipmentType == EquipmentType.Right)
        {
            quiver.gripType = QuiverType.Right;
            bowSetting.gripType = BowGripType.Right;
        }
        if (equipmentType == EquipmentType.Left)
        {
            quiver.gripType = QuiverType.Left;
            bowSetting.gripType = BowGripType.Left;
        }
        bowSetting.SetHand();
    }

    public void ReturnBow()
    {
        float xOffset = 0.3f;
        float yOffset = -0.6f;
        float zOffset = 0.01f;

        bowSpawnPt.position = transform.position + new Vector3(0, yOffset, 0);
        if (equipmentType == EquipmentType.Left)
        {
            bowSpawnPt.localPosition += new Vector3(xOffset, 0, zOffset);
        }
        if (equipmentType == EquipmentType.Right)
        {
            bowSpawnPt.localPosition += new Vector3(-xOffset, 0, zOffset);
        }

        bow.transform.position = bowSpawnPt.position;
        bow.GetComponent<BowSetting>().GetNocking().UnNockArrow();
    }
}
