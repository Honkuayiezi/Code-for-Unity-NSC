using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCamera : MonoBehaviour
{
    private bool camAvailble;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;

    private void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if(devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvailble = false;
            return;
        }
        for(int i  = 0; i < devices.Length; i++)
        {
            if(!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);

            }
        }
        
        if(backCam == null)         
        {
            Debug.Log("Unable to find back camera");
            return;

        }
        backCam.Play();
        background.texture = backCam;

        camAvailble = true;
    }
    
    private void update()
    {
        if (!camAvailble)
            return;

        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f: 1f;
        background.rectTransform.localScale = new Vector3(1f * ratio, scaleY * ratio, 1f * ratio);


        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
}
