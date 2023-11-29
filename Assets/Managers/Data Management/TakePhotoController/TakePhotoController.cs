using Common.CommonScripts;
using SFB;
using UnityEngine;
using UnityEngine.UI;

namespace Managers.Data_Management.TakePhotoController
{
    public class TakePhotoController : MonoBehaviour
    {
        [Header("Player Avatar")]
        [SerializeField] private RawImage playerAvatar;
        [SerializeField] private RawImage inMenuAvatar;

        [Header("Picture Settings")] 
        [SerializeField] private int takenPictureSize = 512;
        [Header("Default Avatar")] 
        [SerializeField] private Sprite defaultImage;
        
        private readonly ExtensionFilter[] _extensions = new [] {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
            new ExtensionFilter("All Files", "*" ),
        };
        
        public void SetPhotoFromCamera()
        {
            if (NativeCamera.IsCameraBusy()) return;

            TakePicture(takenPictureSize);
        }

        public void TakePhotoFromFile()
        {
            GetPhotoPath();
        }

        public void SetDefaultAvatar()
        {
            SetAvatarTextures(defaultImage.texture);
        }

        private string GetPhotoPath()
        {
            var paths = StandaloneFileBrowser.OpenFilePanel("Select photo", "", _extensions, false);
            Debug.Log(paths[0]);
            return paths[0];
        }

        private void SetPhotoFromFile()
        {
            SetAvatarTextures(TextureGetter.LoadTextureFromFile(GetPhotoPath()));
        }
        
        private void TakePicture( int maxSize )
        {
            NativeCamera.Permission permission = NativeCamera.TakePicture( ( path ) =>
            {
                Debug.Log( "Image path: " + path );
                if( path != null )
                {
                    // Create a Texture2D from the captured image
                    Texture2D texture = NativeCamera.LoadImageAtPath( path, maxSize );
                    if( texture == null )
                    {
                        Debug.Log( "Couldn't load texture from " + path );
                        return;
                    }

                    SetAvatarTextures(texture);
             
                    Destroy( texture, 5f );
                }
            }, maxSize );

            Debug.Log( "Permission result: " + permission );
        }

        private void SetAvatarTextures(Texture texture)
        {
            playerAvatar.texture = texture;
            inMenuAvatar.texture = texture;
        }
    }
}
