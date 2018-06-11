using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Currency", fileName = "Generic Currency Name")]
public class Currency : _Item {
    [Header("Conversion to 1 Gold Piece")]
    public float conversion;
}
