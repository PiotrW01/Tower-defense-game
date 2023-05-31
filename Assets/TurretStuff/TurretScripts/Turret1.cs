using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1 : BaseTurret
{

    public Turret1() : base() { }

    public override void UpgradeTurret()
    {
        if (cooldownTime >= 0.2f) cooldownTime -= 0.1f;
        base.UpgradeTurret();
    }
}
