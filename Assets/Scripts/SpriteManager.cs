using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager
{
    public Sprite[] sprites;
    public List<Sprite> listElectric = new List<Sprite>();
    public List<Sprite> listNonElectric = new List<Sprite>();
    private static SpriteManager instance = null;
    public static SpriteManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SpriteManager();
            }
            return instance;
        }
       
    }
    private SpriteManager()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites");
        listElectric = GetListSprite("on");
        listNonElectric = GetListSprite("off");
    }

    public List<Sprite> GetListSprite(string name)
    {
        List<Sprite> list = new List<Sprite>();
        if (sprites != null && sprites.Length > 0)
        {
            
            foreach (Sprite sprite in sprites)
            {
                if (sprite.name.ToLower().Contains(name.ToLower()))
                {
                    list.Add(sprite);
                }
            }
        }
        return list;
    }

    public Sprite GetElectricSprite(string name)
    {
        if (listElectric != null && listElectric.Count > 0)
        {

            foreach (Sprite sprite in listElectric)
            {
                if (sprite.name.Equals(name))
                {
                    return sprite;
                }
            }
        }
        return null;
    }

    public Sprite GetNonElectricSprite(string name)
    {
        if (listNonElectric != null && listNonElectric.Count > 0)
        {

            foreach (Sprite sprite in listNonElectric)
            {
                if (sprite.name.Equals(name))
                {
                    return sprite;
                }
            }
        }
        return null;
    }
}