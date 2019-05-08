using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System;
public class TakePicture : MonoBehaviour
{
    //自動載入 tag 為 canvas的物體
    private GameObject canvas;
    //public GameObject bullet;
    //public Transform shootPlace;
    private void Start()
    {
        //shootPlace = transform.GetChild(0);
        canvas = GameObject.FindWithTag("Canvas");
    }
    public void ClickPictureButton()
    {
        //GameObject bulletClone = Instantiate(bullet,shootPlace.position, Quaternion.identity);
        //tex.Pause();
        closeCanvas(false);

        //FL_Start();
        StartCoroutine(getTexture());
        //ScreenCapture.CaptureScreenshot("Screenshot.png");
    }
    public IEnumerator getTexture()
    {
        
        yield return new WaitForEndOfFrame();
        Texture2D t = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24, false);
        t.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        t.Apply();
        string name = "GhoustHunter" + DateTime.Now.Month.ToString() + 
                                       DateTime.Now.Day.ToString() +
                                       DateTime.Now.Hour.ToString() +
                                       DateTime.Now.Minute.ToString() +
                                       DateTime.Now.Second.ToString() +".png";
        Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(t, "DCIM/MudanPhotos/", name));
        //File.WriteAllBytes("/Users/zhanminxiang/Unity3D Project/AR_GhostHunter/Assets/Photoes/"  + Time.time + ".jpg", byt);
        closeCanvas(true);
        Destroy(t);

        //FL_Stop();

    }
    void closeCanvas(bool close)
    {
        canvas.gameObject.SetActive(close);
    }

    /// <summary>
    /// flash light funtion 目前沒用到 ，會導致程式crash
    /// </summary>
    private bool Active;
    private AndroidJavaObject camera1;
    /// <summary>
    /// open flash light
    /// </summary>
    void FL_Start()
    {
        AndroidJavaClass cameraClass = new AndroidJavaClass("android.hardware.Camera");
        WebCamDevice[] devices = WebCamTexture.devices;

        int camID = 0;
        camera1 = cameraClass.CallStatic<AndroidJavaObject>("open", camID);

        if (camera1 != null)
        {
            AndroidJavaObject cameraParameters = camera1.Call<AndroidJavaObject>("getParameters");
            cameraParameters.Call("setFlashMode", "torch");
            camera1.Call("setParameters", cameraParameters);
            camera1.Call("startPreview");

            Active = true;
        }
        else
        {
            Debug.LogError("[CameraParametersAndroid] Camera not available");
        }

    }

    void OnDestroy()
    {
        FL_Stop();
    }
    /// <summary>
    /// close flash light 
    /// </summary>
    void FL_Stop()
    {

        if (camera1 != null)
        {
            camera1.Call("stopPreview");
            camera1.Call("release");
            Active = false;
        }
        else
        {
            Debug.LogError("[CameraParametersAndroid] Camera not available");
        }

    }
    /*
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.3f, Screen.height * 0.1f));
        if (!Active)
        {
            if (GUILayout.Button("ENABLE FLASHLIGHT"))
                FL_Start();
        }
        else
        {
            if (GUILayout.Button("DISABLE FLASHLIGHT"))
                FL_Stop();
        }
        GUILayout.EndArea();
    }
    */

}