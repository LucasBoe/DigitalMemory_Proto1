using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachBehaviour : MonoBehaviour
{
    public string attachment;

    public bool CanAttach(AttachBehaviour attachBehaviour)
    {
        return attachment == attachBehaviour.attachment;
    }
}
