using System;
using System.IO;
using System.Net;
using DG.Tweening;
using UnityEngine;

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
            if (File.Exists(url) || RemoteFileExists(url))
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

        public static void DoFade(float targetExposure, float fadeTime, int exposureId, Material materialToUpdate)
        {
            DOTween.To(() => materialToUpdate.GetFloat(exposureId),
                (value) => materialToUpdate.SetFloat(exposureId, value), targetExposure, fadeTime);
        }


        public static void DoFadeAndCallCallback(float targetExposure, Action callback, float fadeTime, int exposureId,
            Material materialToUpdate)
        {
            DOTween.To(() => materialToUpdate.GetFloat(exposureId),
                    (value) => materialToUpdate.SetFloat(exposureId, value), targetExposure, fadeTime)
                .OnComplete(() => callback());
        }
    }
}