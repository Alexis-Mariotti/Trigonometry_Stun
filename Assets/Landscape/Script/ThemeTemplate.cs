using NUnit.Framework;
using System;
using UnityEngine;

[System.Serializable]
public class ThemeTemplate : MonoBehaviour
{
    public Texture background;

    public Texture[] deepTextures;
    public Texture[] groundTextures;
    public Texture[] ceilingTextures;

    public Texture[] waterObstacleTextures;
    public Texture[] groundObstacleTextures;
    public Texture[] ceilingObstacleTextures; 
    //public Texture[] flyingObstaccleTextures;

    private void Start()
    {
        Debug.Log(deepTextures.Length);
    }
}
