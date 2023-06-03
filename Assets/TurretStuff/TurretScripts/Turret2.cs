using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2 : BaseTurret
{
    public Turret2() : base(8.0f,3.0f,600, 400)
    {
        
    }

    protected override void CustomUpgrades()
    {
        damageMultiplier++;
    }
}
