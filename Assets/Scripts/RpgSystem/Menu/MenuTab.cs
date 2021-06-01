using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTab : MonoBehaviour
{
    /// <summary>
    /// References the player holding the required information to fill the tabs
    /// </summary>
    protected Character player;

    void Start()
    {
        player = GetComponentInParent<InGameMenu>().player;
    }

    /// <summary>
    /// Allows derived classes to fill their object tabs differently
    /// </summary>
    public virtual void FillTab() { }
}
