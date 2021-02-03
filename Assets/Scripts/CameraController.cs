using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 basePosition, baseRotation;
    [SerializeField] AnimationCurve xPosChangeByMouseX, zPosChangeByMouseY, xRotChangeByMouseY, zRotChangeByMouseX;

    // Update is called once per frame
    void Update()
    {
        var x = Input.mousePosition.x / Screen.width;
        var y = Input.mousePosition.y / Screen.height;

        transform.position = basePosition + xPosChangeByMouseX.Evaluate(x) * Vector3.right + zPosChangeByMouseY.Evaluate(y) * Vector3.forward;
        transform.rotation = Quaternion.Euler(baseRotation.x + xRotChangeByMouseY.Evaluate(y), baseRotation.y + zRotChangeByMouseX.Evaluate(x), baseRotation.z);
    }
}
