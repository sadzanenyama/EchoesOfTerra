using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed;
    public enum RotateDirs {X_axis, Y_axis, Z_axis};
    public enum RotateBases {Global, Local};

    public RotateDirs rotateDir = RotateDirs.Y_axis;
    public RotateBases rotateBase = RotateBases.Global;

    private Vector3 rotateVector;

    private void Start()
    {
        if (rotateBase == RotateBases.Global)
        {
            if (rotateDir == RotateDirs.X_axis)
            {
                rotateVector = Vector3.right;
            }
            else if (rotateDir == RotateDirs.Y_axis)
            {
                rotateVector = Vector3.up;
            }
            else if (rotateDir == RotateDirs.Z_axis)
            {
                rotateVector = Vector3.forward;
            }
        }
        else
        {
            if (rotateDir == RotateDirs.X_axis)
            {
                rotateVector = transform.right;
            }
            else if (rotateDir == RotateDirs.Y_axis)
            {
                rotateVector = transform.up;
            }
            else if (rotateDir == RotateDirs.Z_axis)
            {
                rotateVector = transform.forward;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateVector, rotationSpeed * Time.deltaTime);
    }
}
