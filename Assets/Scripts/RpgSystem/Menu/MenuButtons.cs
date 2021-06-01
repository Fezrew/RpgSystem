using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public GameObject backGround;
    Color buttonColour;
    // Start is called before the first frame update
    void Start()
    {
        buttonColour = gameObject.GetComponent<Image>().color;
    }

    public void ChangeColour()
    {
        backGround.GetComponent<Image>().color = buttonColour;
    }
}
