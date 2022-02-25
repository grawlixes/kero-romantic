using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator frogAnimator;

    public void ToggleBool(string name, bool var)
    {
        frogAnimator.SetBool(name, var);
    }
}
