using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game/GameData")]
public class GameData : ScriptableObject
{
    [Range(0f, 1f)]
    [SerializeField] float tile_size;
    // public int score;
    // public int lives;
    // public float volume;
}
