using System.IO;
using UnityEngine;

namespace Utils
{
    public static class VideoControllerHelper
    {
        public static string GeneratePathToVideo(string url)
        {
            Debug.Log($"ABSOLUTE PATH OR URL - {url}");
            if (File.Exists(url) || url.StartsWith("http"))
                return url;
            var persistentPath = Path.Combine(Application.persistentDataPath, url);
            Debug.Log($"PERSISTENT - {persistentPath}");
            if (File.Exists(persistentPath))
                return persistentPath;
            var streamingPath = Path.Combine(Application.streamingAssetsPath, url);
            Debug.Log($"STREAMING - {streamingPath}");
            return File.Exists(streamingPath) ? streamingPath : "";
        }
    }
}