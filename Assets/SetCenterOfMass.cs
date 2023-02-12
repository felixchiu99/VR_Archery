using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class SetCenterOfMass : MonoBehaviour
{
    [SerializeField]
    private Transform centerOfMass = null;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.centerOfMass = centerOfMass.position;
    }
}
