using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class DragTarget : MonoBehaviour
{
    public string Name { get; set; } = "snaptarget";
    public string Zone { get; set; } = "";
    public string Index { get; set; } = "";
    public int ElectricCount { get; set; } = 0;
    Snap[] snaps;
    SpriteRenderer spriteR;
    Sprite sprite;
    public bool inSnap = false;
    public bool isElectric = false;
    // Start is called before the first frame update
    void Start()
    {
        Index = gameObject.name.Split('_')[1];
        Name = gameObject.name.Split('_')[0];
        Zone = gameObject.name.Split('_')[2];
        GameObject goZone = GameObject.FindGameObjectWithTag("Zone_" + Zone);
        if (Name != null && Name.Length > 0)
        {
            if (Name.ToLower().Equals("prevent"))
            {
                List<Transform> list = Utils.getChilderenByName(goZone, "preventSnap");
                if(list != null && list.Count > 0)
                {
                    snaps = new Snap[list.Count];
                    int i = 0;
                    foreach(Transform transform in list)
                    {
                        snaps[i] = transform.GetComponent<Snap>();
                        i++;
                    }
                }
            }
            else
            {
                Transform snapInZone = goZone.transform.Find("snap_" + Index + "_" + Zone);
                if (snapInZone == null) Debug.LogError("Snap not found in zone: " + Zone);
                snaps = new Snap[1];
                snaps[0] = snapInZone.GetComponent<Snap>();
            }
        }
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        sprite = spriteR.sprite;
        LoadElectricFromPipe();
        Debug.Log($"Pipe:{Name}: {ElectricCount} ");
    }

    void FindSnap()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (snaps != null && snaps.Length > 0)
        {
            bool haveSnap = false;
            foreach (Snap snap in snaps)
            {
                snap.setPos(this);
                if (snap.IsSnapped)
                {
                    haveSnap = true;
                    snap.ElectricCount = ElectricCount;
                }
                else
                {
                    snap.ElectricCount = 0;
                }
            }
           inSnap = haveSnap; 
        }
        //if(!inSnap || !isElectric) SetSprite();
    }

    public void LoadElectricFromPipe()
    {
        ElectricCount = Utils.RandomeIntEvenNumber(100, 150);
        //if (Name.ToLower().Contains("corner"))
        //{
        //    ElectricCount = 90;
        //}
        //if (Name.ToLower().Contains("straight"))
        //{
        //    ElectricCount = 100;
        //}
        //if (Name.ToLower().Contains("tpipe"))
        //{
        //    ElectricCount = 70;
        //}
        //if (Name.ToLower().Contains("cross"))
        //{
        //    ElectricCount = 50;
        //}
        //if (Name.ToLower().Contains("circlecorner"))
        //{
        //    ElectricCount = 40;
        //}
        if (Name.ToLower().Contains("prevent"))
        {
            ElectricCount = SceneScript.DEFAULT_PREVENTELEC_POINT;
        }
    }

    public void SetSprite()
    {
        if (inSnap && isElectric)
        {
            //Sprite newSprite = null;
            //if (Name.ToLower().Contains("corner"))
            //{
            //    newSprite = SpriteManager.Instance.GetElectricSprite("on_corner");
            //}
            //if (Name.ToLower().Contains("straight"))
            //{
            //    newSprite = SpriteManager.Instance.GetElectricSprite("on_straight");
            //}
            //if (Name.ToLower().Contains("tpipe"))
            //{
            //    newSprite = SpriteManager.Instance.GetElectricSprite("on_tpipe");
            //}
            //if (Name.ToLower().Contains("cross"))
            //{
            //    newSprite = SpriteManager.Instance.GetElectricSprite("on_cross");
            //}
            //spriteR.sprite = newSprite;
        }
        else
        {
            Sprite newSprite = null;
            if (Name.ToLower().Contains("corner"))
            {
                newSprite = SpriteManager.Instance.GetNonElectricSprite("off_corner");
            }
            if (Name.ToLower().Contains("straight"))
            {
                newSprite = SpriteManager.Instance.GetNonElectricSprite("off_straight");
            }
            if (Name.ToLower().Contains("tpipe"))
            {
                newSprite = SpriteManager.Instance.GetNonElectricSprite("off_tpipe");
            }
            if (Name.ToLower().Contains("cross"))
            {
                newSprite = SpriteManager.Instance.GetNonElectricSprite("off_cross");
            }
            if (Name.ToLower().Contains("circlecorner"))
            {
                newSprite = SpriteManager.Instance.GetNonElectricSprite("off_circlecorner");
            }
            spriteR.sprite = newSprite;
        }
    }
}
