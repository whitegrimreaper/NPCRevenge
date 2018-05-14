using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Trap/Generic", fileName = "Generic File Name")]
public class Trap : ScriptableObject {
    [Header("General Properties")]
    public string Name = "New Trap";

    [Multiline(3)]
    public string ItemDescription = "Trap Description";

    public Types TrapType;                           
    public enum Types
    {
        Magic_Ranged,
        Magic_Melee,
        Ranged,
        Melee
    }

    public int range;

    public Texture2D Sprite;

    [Header("In-Game value of trap")]
    public int value;

    [Header("Health of Trap")]
    public float health;

    public int damageReduction;

    [Header("Time in seconds for trap reset. -1 for manual reset")]
    public int resetTime;

    [Header("Damage Dealt")]
    public int damage;
}
