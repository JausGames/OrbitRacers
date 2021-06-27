using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaker
{
    SpriteRenderer rend;
    #region Singleton
    private static SpriteMaker instance;

    public static SpriteMaker GetInstance()
    {
        if (instance == null)
        {
            instance = new SpriteMaker();
            return instance;
        }
        else return instance;
    }


    #endregion

    public Sprite AverageSprite(Sprite sprite, FilterMode mode)
    {
        var width = sprite.texture.width;
        var height = sprite.texture.height;
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var closedPixels = new List<Color>();
                closedPixels.Add(sprite.texture.GetPixel(x, y));

                if (x > 0) closedPixels.Add(sprite.texture.GetPixel(x - 1, y));
                if (x < width - 1) closedPixels.Add(sprite.texture.GetPixel(x - 1, y));
                if (y > 0) closedPixels.Add(sprite.texture.GetPixel(x, y - 1));
                if (y < height - 1) closedPixels.Add(sprite.texture.GetPixel(x, y + 1));

                var rValue = 0f;
                var gValue = 0f;
                var bValue = 0f;
                var aValue = 0f;

                foreach (Color color in closedPixels)
                {
                    rValue += color.r;
                    gValue += color.g;
                    bValue += color.b;
                    aValue += color.a;

                }
                var r = rValue / closedPixels.Count;
                var g = gValue / closedPixels.Count;
                var b = bValue / closedPixels.Count;
                var a = aValue / closedPixels.Count;

                texture.SetPixel(x, y, new Color(r, g, b, a));
            }
        }
        texture.filterMode = mode;
        texture.Apply();

        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), Mathf.Max(texture.width, texture.height));

    }
    public Sprite ColorSprite(Sprite sprite, Color color, float intensity, FilterMode mode)
    {
        var importedTexture = sprite.texture;
        var width = importedTexture.width;
        var height = importedTexture.height;

        byte[] textureTmp = importedTexture.GetRawTextureData();


        Texture2D oldTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Texture2D newTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        oldTexture.LoadRawTextureData(textureTmp);

        for (int x = 0; x < newTexture.width; x++)
        {
            for (int y = 0; y < newTexture.height; y++)
            {
                var currColor = oldTexture.GetPixel(x, y);
                var outputColor = (currColor * (1f - intensity) + color * intensity);
                newTexture.SetPixel(x, y, outputColor);

            }
        }

        newTexture.filterMode = mode;
        newTexture.Apply();

        Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.one * 0.5f);

        return newSprite;

    }
    public Sprite ColorSaturateSprite(Sprite sprite, Color color, FilterMode mode)
    {
        if (sprite == null) return Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 4, 4), Vector2.one * 0.5f); ;
        var importedTexture = sprite.texture;
        var width = importedTexture.width;
        var height = importedTexture.height;

        byte[] textureTmp = importedTexture.GetRawTextureData();


        Texture2D oldTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Texture2D newTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        oldTexture.LoadRawTextureData(textureTmp);

        for (int x = 0; x < newTexture.width; x++)
        {
            for (int y = 0; y < newTexture.height; y++)
            {
                var currColor = oldTexture.GetPixel(x, y);
                if (currColor.a == 1f)
                {
                    var h = 0f;
                    var sCurr = 0f;
                    var vCurr = 0f;
                    var s = 0f;
                    var v = 0f;
                    Color.RGBToHSV(currColor, out h, out sCurr, out vCurr);
                    Color.RGBToHSV(color, out h, out s, out v);
                    var outputColor = Color.HSVToRGB(h , sCurr, vCurr);
                    newTexture.SetPixel(x, y, outputColor);
                }
                else newTexture.SetPixel(x, y, new Color(0f, 0f, 0f, 0f));

            }
        }

        newTexture.filterMode = mode;
        newTexture.Apply();

        Sprite newSprite = Sprite.Create(newTexture, sprite.rect, Vector2.one * 0.5f, sprite.pixelsPerUnit);
        return newSprite;

    }

    public IEnumerator ColorSaturateSprite(Sprite sprite, Color color, Color baseColor, FilterMode mode)
    {
        if (sprite == null) yield return Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 4, 4), Vector2.one * 0.5f); ;
        var importedTexture = sprite.texture;
        var width = importedTexture.width;
        var height = importedTexture.height;
        var hbase = 0f;
        var sbase = 0f;
        var vbase = 0f;
        Color.RGBToHSV(baseColor, out hbase, out sbase, out vbase);

        byte[] textureTmp = importedTexture.GetRawTextureData();


        Texture2D oldTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Texture2D newTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        oldTexture.LoadRawTextureData(textureTmp);
        for (int x = 0; x < newTexture.width; x++)
        {
            for (int y = 0; y < newTexture.height; y++)
            {
                var currColor = oldTexture.GetPixel(x, y);
                if (currColor.a == 1f)
                {
                    var h = 0f;
                    var sCurr = 0f;
                    var vCurr = 0f;
                    var s = 0f;
                    var v = 0f;
                    Color.RGBToHSV(currColor, out h, out sCurr, out vCurr);
                    var variance = hbase - h;
                    Color.RGBToHSV(color, out h, out s, out v);
                    var nb = (h + variance) % 1f;
                    if (nb < 0) nb = 1f + nb;
                    var outputColor = Color.HSVToRGB(nb, sCurr, vCurr);
                    newTexture.SetPixel(x, y, outputColor);
                }
                else newTexture.SetPixel(x, y, new Color(0f, 0f, 0f, 0f));
            }

            Debug.Log("SpriteMaker, ColorSaturateSprite : " + (x % newTexture.width / 10));
            if (x % newTexture.width / 10 == 0) yield return false;
        }

        newTexture.filterMode = mode;
        newTexture.Apply();

        Sprite newSprite = Sprite.Create(newTexture, sprite.rect, Vector2.one * 0.5f, sprite.pixelsPerUnit);
        yield return newSprite;

    }

    public Texture2D ColorSaturateTexture(Sprite sprite, Color color, Color baseColor, FilterMode mode)
    {
        var importedTexture = sprite.texture;
        var width = importedTexture.width;
        var height = importedTexture.height;

        byte[] textureTmp = importedTexture.GetRawTextureData();


        Texture2D oldTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Texture2D newTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        newTexture.filterMode = mode;

        oldTexture.LoadRawTextureData(textureTmp);

        for (int x = 0; x < newTexture.width; x++)
        {
            for (int y = 0; y < newTexture.height; y++)
            {
                var currColor = oldTexture.GetPixel(x, y);
                if (currColor.a == 1f)
                {
                    var h = 0f;
                    var sCurr = 0f;
                    var vCurr = 0f;
                    var s = 0f;
                    var v = 0f;
                    Color.RGBToHSV(currColor, out h, out sCurr, out vCurr);
                    Color.RGBToHSV(color, out h, out s, out v);
                    var outputColor = Color.HSVToRGB(h, sCurr, vCurr);
                    newTexture.SetPixel(x, y, outputColor);
                }
                else newTexture.SetPixel(x, y, new Color(0f, 0f, 0f, 0f));

            }
        }

        
        newTexture.Apply();

        return newTexture;

    }
}