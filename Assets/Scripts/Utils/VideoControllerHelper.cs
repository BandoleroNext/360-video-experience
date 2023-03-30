using System.IO;
using System.Net;
using UnityEngine;

namespace Utils
{
    public class VideoControllerHelper
    {
        public static string GeneratePathToVideo(string url)
        {
            var persistentPath = Path.Combine(Application.persistentDataPath, url);
            if (File.Exists(persistentPath))
                return persistentPath;
            var streamingPath = Path.Combine(Application.streamingAssetsPath, url);
            if (File.Exists(streamingPath))
                return streamingPath;
            if (File.Exists(url)||RemoteFileExists(url))
                return url;
            return "";
        }

        private static bool RemoteFileExists(string url)
        {
            try
            {
                if (WebRequest.Create(url) is not HttpWebRequest request) return false;
                request.Method = "HEAD";
                if (request.GetResponse() is not HttpWebResponse response) return false;
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                Debug.LogError($"Video not found!");
                return false;
            }
        }
    }
    
    

}
