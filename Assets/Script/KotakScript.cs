using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotakScript : MonoBehaviour, IClicked
{
    public void OnClickAction()
    {
        Destroy(gameObject);
    }
}
