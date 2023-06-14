using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1 : BaseTurret
{

    public Turret1() : base(3.5f, 0.95f, 180, 100, new bool[] { false, true, false }) { }

    protected override void CustomUpgrades()
    {
        cooldownTime -= 0.15f;
    }
}
