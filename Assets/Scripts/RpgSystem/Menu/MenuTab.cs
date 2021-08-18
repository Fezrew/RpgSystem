using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGSystem;

public class MenuTab : MonoBehaviour
{
    /// <summary>
    /// Allows derived classes to fill their object tabs differently
    /// </summary>
    public virtual void FillTab(Character player) { }

    /// <summary>
    /// Allows derived classes to update their object tabs differently
    /// </summary>
    public virtual void UpdateTab(Character player) { }
}
