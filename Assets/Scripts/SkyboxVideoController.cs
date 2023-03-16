using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Video;


public class SkyboxVideoController : MonoBehaviour
{
    public SkyboxVideoDescriptor skyboxdescriptor;
    private VideoPlayer skyboxcontroller;
    



    private void Start()
    {
        var title = skyboxdescriptor.Title;
        var description = skyboxdescriptor.Description;
        var url = skyboxdescriptor.Url;
        
    }





    private bool CheckVideoUrl(string url)
    {
        Debug.Log($"Searching for the video!");
        
        if (System.IO.File.Exists(url))
        {
            return true;
        }
        else
        {
            Debug.Log($"Not found in memory, searching on the web");
            if (RemoteFileExists(url))
            {
               Debug.Log($"Video found!");
               return true;
            }
            else
            {
                Debug.Log($"Video not found!");
                return false;
            }
        }
    }
    
    private static bool RemoteFileExists(string url)
    {
        try
        {
            //Creating the HttpWebRequest
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "HEAD";
            //Getting the Web Response.
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            response.Close();
            return (response.StatusCode == HttpStatusCode.OK);
        }
        catch
        {
            return false;
        }
    }
}
