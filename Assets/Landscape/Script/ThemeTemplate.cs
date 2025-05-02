using NUnit.Framework;
using System;
using UnityEngine;

[System.Serializable]
public class ThemeTemplate : MonoBehaviour
{
    public Sprite background;

    public Sprite[] deepTextures;
    public Sprite[] groundTextures;
    public Sprite[] ceilingTextures;

    public Sprite[] waterObstacleTextures;
    public Sprite[] groundObstacleTextures;
    public Sprite[] ceilingObstacleTextures;

    public Sprite portalBorderTexture;
    public Sprite portalCenterTexture;



    private void Start()
    {
        Debug.Log(deepTextures.Length);
    }
}
