using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobileInputChecker : inputChecker
{
    public override bool input()
    {
        return Input.GetTouch(0).phase == TouchPhase.Began;
    }
}
