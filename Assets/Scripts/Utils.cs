using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Utils
    {
        public static int RandomeIntEvenNumber(int min, int max)
        {
            while(true)
            {
                int randomNumber = UnityEngine.Random.RandomRange(min, max);
                if (randomNumber % 2 == 0) return randomNumber;
            }
        }

        public static List<Transform> getChilderenZone(GameObject gameObject, string zoneNumber)
        {
            if (gameObject == null) return null;
            List<Transform> zoneChild = new List<Transform>();
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform child = gameObject.transform.GetChild(i);
                string zone = "";
                try
                {
                    zone = child.name.Split('_')[2];

                }
                catch (Exception e)
                {
                    continue;
                }

                if (zone != null && zone.Equals(zoneNumber))
                {
                    zoneChild.Add(child);
                }
            }
            return zoneChild;
        }

        public static List<Transform> getChilderensByName(GameObject gameObject, string name)
        {
            if (gameObject == null) return null;
            List<Transform> zoneChild = new List<Transform>();
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform child = gameObject.transform.GetChild(i);
                string nameChild = "";
                try
                {
                    nameChild = child.name.Split('_')[0];
                }
                catch (Exception e)
                {
                    continue;
                }

                if (nameChild != null && nameChild.ToLower().Contains(name.ToLower()))
                {
                    zoneChild.Add(child);
                }
            }
            return zoneChild;
        }

        public static List<Transform> getChilderenByName(GameObject gameObject, string name)
        {
            if (gameObject == null) return null;
            List<Transform> transforms = new List<Transform>();
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform child = gameObject.transform.GetChild(i);
                string nameChild = "";
                try
                {
                    nameChild = child.name.Split('_')[0];

                }
                catch (Exception e)
                {
                    continue;
                }

                if (nameChild != null && nameChild.Equals(name))
                {
                    transforms.Add(child);
                }
            }
            return transforms;
        }

        public static Transform getChilderen(GameObject gameObject, string name)
        {
            if (gameObject == null) return null;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform child = gameObject.transform.GetChild(i);
                if (child.name != null && child.name.Equals(name))
                {
                    return child;
                }
            }
            return null;
        }
    }
}
