using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Trap/Lethal", fileName = "Lethal Trap Name")]
public class LethalTrap : Trap {
    
    [Header("Damage Types")]
    public DmgType[] dmgTypes;

    public enum DmgType
    {
        Fire,
        Ice,
        Eletricity,
        Necrotic,
        Radiant,
        Acid,
        PoisonHealth,
        None
    }

    [Header("Duration in seconds of effect")]
    public int duration;
}
