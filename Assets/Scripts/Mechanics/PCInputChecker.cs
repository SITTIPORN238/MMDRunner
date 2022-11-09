using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInputChecker : inputChecker
{
    public override bool input()
    {
        return Input.GetButtonUp("Jump");
    }
}
