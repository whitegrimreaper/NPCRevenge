using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {
    public bool dropItems_;
    public bool dropEquipment_;
    public int onDeathDamage_;              //0 if no damage, >0 for damage
    public int onDeathDamageRange_;         //range for damage burst on damage
}
