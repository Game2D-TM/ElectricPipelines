using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    double _unsnapMouseDistance = 0.5;
    public string Name { get; set; } = "snappoint";
    public string Zone { get; set; } = "";

    public string Index { get; set; } = "";
    public int ElectricCount { get; set; } = 0;
    public bool IsSnapped { get; set; } = false;
    Vector2 _snapMousePos;
    Transform _snapTarget;
    public DragTarget target;
    // Start is called before the first frame update
    void Start()
    {
        Name = gameObject.name.Split('_')[0];
        Index = gameObject.name.Split('_')[1];
        Zone =  gameObject.name.Split('_')[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (_snapTarget == null || target == null) return;
        if (this._snapTarget != null)
        {
            if (Vector2.Distance(this._snapMousePos, transform.position) <= _unsnapMouseDistance)
            {
                this._snapTarget = null;
                //Here you could also move the object to an appropriate distance regarding the current mouse position so the user feels he got back control of it
                //Also if you have troubles with the trigger being re-triggerer too fast, you could have a add a small timer to prevent it for about half a second...
                if (target != null)
                {
                    IsSnapped = true;
                    target.transform.position = transform.position;
                }
                return;
            }
        }
        IsSnapped = false;
    }

    public void setPos(DragTarget other)
    {
        this._snapTarget = other.transform;
        this._snapMousePos = other.transform.position;
        target = other;
    }
}
