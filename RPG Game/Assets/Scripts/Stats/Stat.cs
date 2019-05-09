using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {

    [SerializeField]
    private int baseValue;

    private List<int> modifiers = new List<int>();
    

    // Total stat value
    public int BaseValue () {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }


    // Add stat value
    public void AddModifier (int modifier) {
        if (modifier != 0) {
            modifiers.Add(modifier);
        }
    }


    // Remove stat value
    public void RemoveModifier (int modifier) {
        if (modifier != 0) {
            modifiers.Remove(modifier);
        }
    }
}
