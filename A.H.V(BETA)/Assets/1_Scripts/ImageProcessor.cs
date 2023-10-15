using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageProcessor : MonoBehaviour
{
    public void Render_To_Render_Passing(RenderTexture source, RenderTexture target){
        Graphics.Blit(source, target);
    }
}
