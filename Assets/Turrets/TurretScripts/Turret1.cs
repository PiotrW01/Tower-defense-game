using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1 : BaseTurret
{

    public Turret1() : base(2f, 1f, 180, 200) { }

    protected override void CustomUpgrades()
    {
        cooldownTime -= 0.1f;
    }
}
