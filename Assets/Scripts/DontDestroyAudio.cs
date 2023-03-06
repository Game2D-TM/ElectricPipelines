using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudio : MonoBehaviour
{
    public bool CanDestroy { get; set; } = false;
    private void Awake()
    {
        if (!CanDestroy)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
