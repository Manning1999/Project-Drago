using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowable
{

    Rigidbody _rb { get; set; }
    AnimationClip _clipToPlayBeforeThrowing { get; set; }
    Animator _anim { get; set; }
    bool _isThrowable { get; set; }

   
    IEnumerator PlayAnimation(Vector3 startPosition, Vector3 locationToThrowTo, float throwForce);
    void Throw(Vector3 startPosition, Vector3 locationToThrowTo, float throwForce);

}
