using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Game/ItemData")]
public class Item : ScriptableObject
{
    [Header("Item Data")]
    [SerializeField] private int ID = -1;
    [SerializeField] private ItemType type;
    [SerializeField] private Sprite sprite;
    [SerializeField] private bool canStack;

    
}

public enum ItemType
{
    None,
    Weapon,
    Tool,
    Block
}

[CreateAssetMenu(fileName = "Weapon", menuName = "Game/WeaponData")]
public class Weapon : Item
{
    [Header("Weapon Data")]
    [SerializeField] private int dmg;
    [SerializeField] private int speed;
    [SerializeField] private int durability;
    [SerializeField] private int rarity;


}
