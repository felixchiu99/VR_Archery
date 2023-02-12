using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]

public class BowStringRenderer : MonoBehaviour
{
    [Header("Setting")]
    public Gradient pullColour = null;

    [Header("Bowstring anchor")]
    [SerializeField]
    private Transform upperPoint = null;
    [SerializeField]
    private Transform midUpperPoint = null;
    [SerializeField]
    private Transform midLowerPoint = null;
    [SerializeField]
    private Transform lowerPoint = null;

    private LineRenderer lineRenderer = null;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor && !Application.isPlaying)
            UpdatePositions();
    }
    
    private void OnEnable()
    {
        Application.onBeforeRender += UpdatePositions;
    }

    private void OnDisable()
    {
        Application.onBeforeRender -= UpdatePositions;
    }


    private void UpdatePositions()
    {
        Vector3[] positions = new Vector3[] { upperPoint.position, midUpperPoint.position, midLowerPoint.position, lowerPoint.position };
        lineRenderer.SetPositions(positions);
    }

}
