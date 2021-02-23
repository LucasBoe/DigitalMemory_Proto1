using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionCircle : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f, upDownSpeed = 0.5f;
    [SerializeField] AnimationCurve upDownCurve;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, rotationSpeed * Time.time ,0f);

        transform.localPosition = new Vector3(0, upDownCurve.Evaluate(Time.time * upDownSpeed), 0);
    }
}
