using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Utils
{
    public static class VideoControllerHelper
    {
        public static string GeneratePathToVideo(string url)
        {
            var persistentPath = Path.Combine(Application.persistentDataPath, url);
            Debug.Log($"PERSISTENT - {persistentPath}");
            if (File.Exists(persistentPath))
                return persistentPath;
            var streamingPath = Path.Combine(Application.streamingAssetsPath, url);
            Debug.Log($"STREAMING - {streamingPath}");
            if (File.Exists(streamingPath))
                return streamingPath;
            Debug.Log($"ABSOLUTE PATH OR URL - {url}");
            if(File.Exists(url) || url.StartsWith("http"))
                return url;
            return "";
        }
    }
}