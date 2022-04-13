using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundKeeper : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
