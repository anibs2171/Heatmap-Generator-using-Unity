using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to load map's image as a texture 

public class Maptex : MonoBehaviour
{

    public TextAsset image;
    void Start()
    {

        var texture = new Texture2D(100, 100, TextureFormat.ARGB32, false);
        texture.LoadImage(image.bytes);
        GetComponent<Renderer>().material.mainTexture = texture;

        texture.Apply();

    }



}
