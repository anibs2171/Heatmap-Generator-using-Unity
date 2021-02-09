using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Hits : MonoBehaviour
{

    Color green = new Color(0f, 1f, 0f, 1f);
    Color yellow = new Color(1f, 1f, 0f, 1f);
    Color red = new Color(1f, 0f, 0f, 1f);
    Color def = new Color(0.804f, 0.804f, 0.804f, 0.804f);

    //List<Vector2> NormPoints;


    void Update()
    {

        Color[] intensity = new Color[3];
        intensity[0] = green;
        intensity[1] = yellow;
        intensity[2] = red;


        List<Vector2> Points;
        Points = new List<Vector2>();


        //TEST POINTS
        Points.Add(new Vector2(10, 10));
        Points.Add(new Vector2(10, 10));
        Points.Add(new Vector2(10, 10));
        Points.Add(new Vector2(12, 10));
        Points.Add(new Vector2(13, 10));
        Points.Add(new Vector2(13, 10));
        Points.Add(new Vector2(20, 10));
        Points.Add(new Vector2(50, 10));
        Points.Add(new Vector2(80, 10));
        Points.Add(new Vector2(10, 30));
        Points.Add(new Vector2(10, 30));
        Points.Add(new Vector2(10, 40));
        Points.Add(new Vector2(10, 53));
        Points.Add(new Vector2(10, 30));
        Points.Add(new Vector2(10, 40));
        Points.Add(new Vector2(10, 53));
        Points.Add(new Vector2(8, 53));
        Points.Add(new Vector2(8, 53));
        
        //Finding max and min (for normalization)
        /*
        float max = Points[0].x;
        float min = Points[0].x;
       
        for (int i= 0; i < Points.Count; i++)
        {
            if (Points[i].x > max)
            {
                max = Points[i].x;
            }
            else if (Points[i].y > max)
            {
                max = Points[i].y;
            }
            else if (Points[i].x < min)
            {
                min = Points[i].x;
            }
            else if (Points[i].y < min)
            {
                min = Points[i].y;
            }
            else
            {
                continue;
            }
        }
        Debug.Log(max + "and" + min);
        */

        var texture = new Texture2D(100, 100, TextureFormat.ARGB32, false);

        for(int i = 0; i < 18 ; i++)
        {
            float x = Points[i].x;
            float y = Points[i].y;
            if (x >= 5 && x <= 97 && y >= 5 && y <= 95)
            {
                draw((int)x, (int)y, texture);
            }
            //draw((int)x, (int)y, texture);
        }
        
     
        GetComponent<Renderer>().material.mainTexture = texture;
        texture.Apply();
        
    }

    void draw(int x, int y, Texture2D tex)
    {

        Color[] colors_b = new Color[25];
        for (var i = 0; i < 25; i++)
        {
            colors_b[i] = green;
        }
        Color[] colors_y = new Color[9];
        for (var i = 0; i < 9; i++)
        {
            colors_y[i] = yellow;
        }
        Color[] colors_r = new Color[1];
        for (var i = 0; i < 1; i++)
        {
            colors_r[i] = red;
        }


        Color pixc = tex.GetPixel(x, y);
        if (pixc == green)
        {
                
            //tex.SetPixels(x-1, y-1, 3, 3, colors_b, 0);
            tex.SetPixel(x, y, yellow);
            for(int i = -1; i <= 1; i++)
            {
                for(int j = -1; j <= 1; j++)
                {
                    Color Yelgreen = tex.GetPixel(x + i, y + j);
                    if (Yelgreen!=yellow && Yelgreen!=green && Yelgreen!=red)
                    {
                        tex.SetPixel(x + i, y + j, green);
                    }

                    else if (Yelgreen == green)
                    {
                        tex.SetPixel(x + i, y + j, yellow);
                    }
                }
                
            }

            

        }
        else if (pixc == yellow)
        {

            tex.SetPixel(x, y, red);
            for (int i = -2; i <= 2; i++)
             {
                for (int j = -2; j <= 2; j++)
                {
                    Color Redyel = tex.GetPixel(x + i, y + j);
                    if (Redyel != yellow && Redyel != green && Redyel != red)
                    {
                        tex.SetPixel(x + i, y + j, green);
                    }
                }
            }
            
            
            for (int i = -1; i <= 1; i++)
            {
                for(int j = -1; j <= 1; j++)
                {
                    Color Redyel = tex.GetPixel(x + i, y + j);
                    if (Redyel == green)
                    {
                        tex.SetPixel(x + i, y + j, yellow);
                    }

                    
                }
            }


        }
        else if (pixc == red)
        {
            ;
        }
        else
        {
            tex.SetPixel(x,y ,green);
        }

    }


    //If Normalizing is required
    /*
    void normalize(int x, int y, int min, int max)
    {
        x = (100) * (x - min) / (max - min);
        y = (100) * (y - min) / (max - min);
        NormPoints.Add(new Vector2(x, y));
    }
    */
    




}
