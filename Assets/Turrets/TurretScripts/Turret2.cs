using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2 : BaseTurret
{
    public Turret2() : base(8.0f,1.6f, 350, 400)
    {
        
    }

    protected override void CustomUpgrades()
    {
        damageMultiplier += 0.3f;
    }
}
