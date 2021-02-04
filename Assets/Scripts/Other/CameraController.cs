using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] Vector3 basePosition, baseRotation;
    [SerializeField] AnimationCurve xPosChangeByMouseX, zPosChangeByMouseY, xRotChangeByMouseX, zRotChangeByMouseY;

    // Update is called once per frame
    void Update()
    {
        var x = Input.mousePosition.x / Screen.width;
        var y = Input.mousePosition.y / Screen.height;

        transform.position = basePosition + xPosChangeByMouseX.Evaluate(x) * Vector3.forward + zPosChangeByMouseY.Evaluate(y) * Vector3.left;
        transform.rotation = Quaternion.Euler(baseRotation.x + xRotChangeByMouseX.Evaluate(x), baseRotation.y, baseRotation.z + zRotChangeByMouseY.Evaluate(y));
    }
}
