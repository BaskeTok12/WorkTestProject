using UnityEngine;

namespace Common.CommonScripts
{
    public abstract class TextureGetter
    {
        public static Texture2D LoadTextureFromFile(string path)
        {
            var fileData = System.IO.File.ReadAllBytes(path);
            
            var texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);

            return texture;
        }
    }
}