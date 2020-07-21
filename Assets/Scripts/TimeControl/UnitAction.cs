using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAction
{
    float time;
    bool isActive;
    Vector3 position;
    int HP;

    /// <summary>
    /// Create a new unit state
    /// </summary>
    /// <param name="time">playback time, float</param>
    /// <param name="isActive">is unit enabled?, bool</param>
    /// <param name="position">Vector3</param>
    /// <param name="HP">integer</param>
    public UnitAction(float time, bool isActive, Vector3 position, int HP) {
        this.time = time;
        this.isActive = isActive;
        this.position = position;
        this.HP = HP;
    }
}
