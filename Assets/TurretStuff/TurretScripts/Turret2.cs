using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2 : BaseTurret
{
    public Turret2() : base(4.0f,2.0f,400)
    {
        
    }

    public override void UpgradeTurret()
    {
        if(damageMultiplier < 5) damageMultiplier++;
        base.UpgradeTurret();
    }
}
