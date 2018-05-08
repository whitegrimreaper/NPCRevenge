using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable", fileName = "Consumable Name")]
public class Consumable : _Item {
    [Header("Consumable Properties")]
    public Types[] ItemTypes;

    public enum Types
    {
        Healing,
        Stamina,
        Poison_Health,
        Poison_Stamina,
        Speed,
        Damage,
        FireDefense,
        IceDefense,
        EletricDefense,
        NecroticDefense,
        RadiantDefense,
        AcidDefense,
        PhysicalDefense,
        PoisonDefense,
        AbilityDmgDefense,
        Invisibility,
    }

    //refers to amount healed, damage, resisted, etc.
    public int amount;

    [Header("Duration in seconds of effect")]
    public int duration;
}
