using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon", fileName = "Weapon Name")]
public class Weapon : _Item {
    [Header("Weapon Properties")]
    public Types ItemType;

    public enum Types
    {
        Ranged,
        Melee
    }

    //0 for most melee, > 0 for reach weapons. 0 > for ranged
    public int range;
    
    [Range(1,2)]
    public int handedness;

    public int damage;

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
        Poison,
        None
    }
 }
