using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Task/Generic", fileName = "Generic File Name")]
public class Task : ScriptableObject
{
    [Header("General Properties")]
    public string Name = "New Task";

    [Multiline(3)]
    public string NPCDescription = "Task Description";

    public string[] dialouge;

    [Range(1, 50)]
    public int reputationRequired;

    public enum TaskType
    {
       Crafting,
       Healing,
       Training,
       Combat,
       Information
    }

    public int amount;
}
