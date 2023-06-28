using System.IO;
using UnityEngine;
using UnityEngine.UI;

//用于加载指定路径的图片到传入的Image组件中
public class ImageLoader
{
    public static void LoadImage(string imgPath, Image img)
    {
        //用字节流的形式读入图片
        byte[] bytes = File.ReadAllBytes(imgPath);
        //将读入的图片保存为2D纹理
        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(bytes))
        {
            //再用保存的2D纹理生成sprite并赋给Image组件
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            img.sprite = sprite;
        }
        else
        {
            Debug.LogError($"图片 {imgPath} 加载失败！");
        }
    }
}
