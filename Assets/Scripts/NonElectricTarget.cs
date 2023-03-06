using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NonElectricTarget : MonoBehaviour
{
    public string Name { get; set; } = "nonElectric";
    public string Zone { get; set; } = "";

    public string Index { get; set; } = "";
    SpriteRenderer spriteR;
    Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        sprite = spriteR.sprite;
        Name = gameObject.name.Split('_')[0];
        Index = gameObject.name.Split('_')[1];
        Zone = gameObject.name.Split('_')[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSprite(bool isSnapped)
    {
        if (isSnapped)
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
        if (!isSnapped)
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

            spriteR.sprite = newSprite;
        }
    }
}
