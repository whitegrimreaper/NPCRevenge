using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
   
[CreateAssetMenu(menuName = "Item/Armor", fileName = "Armor Name")]
public class Armor : _Item {
    [Header("Armor Type")]
    public Types ItemType;
    public enum Types 
    {
        Head,
        Chest,
        Belt,
        Feet,
        Hands,
        Cloak,
        Ring
    }

    public int protection;

    [Header("Resistance Types")]
    public DmgType[] dmgTypes;

    public enum DmgType
    {
        Fire,
        Ice,
        Eletricity,
        Necrotic,
        Radiant,
        Acid,
        Physical,
        Poison,
        None
    } 
}
