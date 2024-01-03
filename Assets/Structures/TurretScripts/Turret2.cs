using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2 : BaseTurret
{
    public Turret2() : base(10.0f,1.6f, 350, 200, new bool[] { true, true, false })
    {
        
    }

    protected override void CustomUpgrades()
    {
        damageMultiplier += 0.3f;
        cooldownTime -= 0.2f;
    }
}
