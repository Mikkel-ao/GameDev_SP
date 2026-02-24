using System;
using UnityEngine;

/// <summary>
/// Holds the stat values for a single item.
/// </summary>
[Serializable]
public class ItemStats
{
    [Min(0)] public int Strength;
    [Min(0)] public int Intelligence;
    [Min(0)] public int Willpower;

    public ItemStats(int strength = 0, int intelligence = 0, int willpower = 0)
    {
        Strength    = strength;
        Intelligence = intelligence;
        Willpower   = willpower;
    }
}
