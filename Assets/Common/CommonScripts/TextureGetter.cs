using UnityEngine;

namespace Common.CommonScripts
{
    public class TextureGetter
    {
        public static Texture2D LoadTextureFromFile(string path)
        {
            byte[] fileData = System.IO.File.ReadAllBytes(path);
            
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);

            return texture;
        }
    }
}