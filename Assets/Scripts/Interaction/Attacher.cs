using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacher
{
    bool CanAttach(string attachBehaviour);
    Transform GetTransform();
}

public class Attacher : MonoBehaviour, IAttacher
{
    public string attachmentName;
    public bool CanAttach(string attachmentName)
    {
        return this.attachmentName == attachmentName;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
