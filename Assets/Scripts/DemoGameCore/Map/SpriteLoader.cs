using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    /// <summary>
    /// 精灵贴图加载器类
    /// </summary>
    public class SpriteLoader : MonoBehaviour
    {
        public static Sprite[] field;         // 地面纹理
        public static Sprite[] forest;        // 森林纹理
        public static Sprite[] factory;       // 工厂纹理
        public static Sprite desert;          // 沙漠纹理
        public static Sprite lake;            // 湖纹理

        public void SpriteLoad()
        {
            field = new Sprite[2];
            forest = new Sprite[2];
            factory = new Sprite[3];

            var sprites = Resources.LoadAll("game/CellIcons");
            Debug.Log(sprites.Length);
            
            field[0] = (Sprite)sprites[1];
            field[1] = (Sprite)sprites[2];
            forest[0] = (Sprite)sprites[5];
            forest[1] = (Sprite)sprites[6];
            factory[0] = (Sprite)sprites[7];
            factory[1] = (Sprite)sprites[8];
            factory[2] = (Sprite)sprites[9];
            desert = (Sprite)sprites[3];
            lake = (Sprite)sprites[4];

        }
    }
}