using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTurret : BaseTurret
{

    public CannonTurret(): base(2.5f, 1.5f, 525, 350) { }
    protected override void CustomUpgrades()
    {
        damageMultiplier += 0.5f;
        cooldownTime -= 0.1f;
    }
}
