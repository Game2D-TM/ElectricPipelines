using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ElectricTarget : MonoBehaviour
{
    // Start is called before the first frame update
    Snap[] snaps;
    List<int> electricPipe = new List<int>();
    public string Name { get; set; } = "electric";
    public string Zone { get; set; } = "";

    public string Index { get; set; } = "";
    bool isSort = false;
    void Start()
    {
         Name = gameObject.name.Split('_')[0];
        Index = gameObject.name.Split('_')[1];
        Zone = gameObject.name.Split('_')[2];
        GameObject[] go = GameObject.FindGameObjectsWithTag("Snap");
        if (go != null && go.Length > 0)
        {
            snaps = new Snap[go.Length];
            int index = 0;
            for (int i = go.Length - 1; i >= 0; i--)
            {
                Snap snap = go[i].GetComponent<Snap>();
                if (snap == null)
                {
                    Debug.LogError("Snap is null");
                    continue;
                }
                snaps[index] = snap;
                index++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSort)
        {
            isSort = true;
            for (int i = 0; i < snaps.Length - 1; i++)
                for (int j = i + 1; j < snaps.Length; j++)

                    if (int.Parse(snaps[i].Zone).CompareTo(int.Parse(snaps[j].Zone)) > 0 ? true : false)
                    {

                        Snap temp = snaps[i];
                        snaps[i] = snaps[j];
                        snaps[j] = temp;
                    }
        }
        if (snaps != null && snaps.Length > 0)
        {
            for (int i = 0; i < snaps.Length; i++)
            {
                if (snaps[i].IsSnapped)
                {
                    int index = int.Parse(snaps[i].Index);
                    pipeAction(index, true, snaps[i].Zone);
                    electricPipe.Add(i);
                }
                else break;
            }
            for (int i = 0; i < snaps.Length; i++)
            {
                if (!snaps[i].IsSnapped)
                {
                    int index = int.Parse(snaps[i].Index);
                    pipeAction(index, false, snaps[i].Zone);
                }
                if (electricPipe.Count > 1)
                {
                    foreach (int pipeElec in electricPipe.ToArray())
                    {
                        if (!snaps[pipeElec].IsSnapped)
                        {
                            int nextIndex = pipeElec + 1;
                            if (nextIndex >= snaps.Length)
                            {
                                electricPipe.Remove(pipeElec);
                                return;
                            }
                            int dragIndex = int.Parse(snaps[nextIndex].Index);
                            pipeAction(dragIndex, false, snaps[nextIndex].Zone);
                            electricPipe.Remove(pipeElec);
                        }
                    }
                }
            }
        }
    }
    
    void pipeAction(int i, bool isElectric, string zone)
    {
        try
        {
            GameObject goZone = GameObject.FindGameObjectWithTag("Zone_" + zone);
            if (goZone == null) return;
            Transform dragPipe = null;
            dragPipe = goZone.transform.Find("CornerdragPipe_0" + i + "_" + zone);
            if(dragPipe == null)
            {
                dragPipe = goZone.transform.Find("StraightdragPipe_0" + i + "_" + zone);
                if (dragPipe == null)
                {
                    dragPipe = goZone.transform.Find("TpipedragPipe_0" + i + "_" + zone);
                    if(dragPipe == null)
                    {
                        dragPipe = goZone.transform.Find("CrossdragPipe_0" + i + "_" + zone);
                    }
                }
                
            }
            if (dragPipe == null) return;
            DragTarget dragTarget = dragPipe.GetComponent<DragTarget>();
            dragTarget.isElectric = isElectric;
            dragTarget.SetSprite();
            List<Transform> zoneChild = Utils.getChilderensByName(goZone, "pipe");
            if (zoneChild != null && zoneChild.Count > 0)
            {
                for (int j = 0; j < zoneChild.Count; j++)
                {
                    NonElectricTarget p = zoneChild[j].GetComponent<NonElectricTarget>();
                    if(p != null) p.SetSprite(isElectric);
                }
            }
        } catch(AndroidJavaException e)
        {
            Debug.Log(e.ToString());
        }
    }


    
}
