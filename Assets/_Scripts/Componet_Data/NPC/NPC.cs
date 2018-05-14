using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/Generic", fileName = "Generic File Name")]
public class NPC : ScriptableObject
{
    [Header("General Properties")]
    public string Name = "New NPC";

    [Multiline(3)]
    public string NPCDescription = "NPC Description";

    public Texture2D Sprite;

    public Task[] tasks;

    public string[] dialouge;
}
