using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Trap/NonLethal", fileName = "NonLethal Trap Name")]
public class NonLethalTrap : Trap
{
    [Header("Effect Types")]
    public EffectType[] effectTypes;

    public enum EffectType
    {
        Sleep,
        Stun,
        Paralyze,
        Entangle,
        Freeze,
        Frenzy,
        Confuse,
        RandomTeleport,
        DirectedTeleport,
        KnockBack,
        Slow,
        DmgStrength,
        DmgDex,
        PoisonStamina
    }

    [Header("Duration in seconds of effect")]
    public int duration;

    [Header("Amount damaged")]
    public int amount;
}
