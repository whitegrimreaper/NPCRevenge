using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Generic", fileName = "Generic File Name")]
public class _Item : ScriptableObject {
    [Header("General Properties")]
    public string Name = "New Item";
    
    [Multiline(3)]
    public string ItemDescription = "Item Description";
   
    [Range(1,50)]
    public int minPlayerLevel;
    public Sprite sprite;

    [Header("In-Game value of item")]
    public int value;

    [Header("Float value between 0.0 & 1.0")]
    public float durability;

    [Header("Int value between 1 & 100")]
    public int dropRate;

    [Header("Item Type")]
    public itemType type;
    public enum itemType
    {
        Weapon,
        Armor,
        Currency,
        Consumable,
        Generic
    }
}
