using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class TrapController_Melee : TrapController {
    public int dealDamage() {
        if (needsToBeReset)
            return 0;

        if (trapStats.resetTime > 0) {
            resetTimer.Interval = trapStats.resetTime * 1000;
            resetTimer.Enabled = true;
        }

        needsToBeReset = true;
        return trapStats.damage;
    }
}
