using System.Collections;
using System.Collections.Generic;
using Controllers;
using Descriptors;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Video;

namespace Tests.Runtime
{
    public class InterruptibleVideoTest
    {
        private const string SampleURL = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/SubaruOutbackOnStreetAndDirt.mp4";
        
        private InterruptibleVideoController _testController;
        private InterruptibleVideoDescriptor _testDescriptor;
        private Material _testMaterial;

        [SetUp]
        public void Setup()
        {
            //CREATE CONTROLLER
            _testController = (Object.Instantiate(new GameObject("testController") as Object) as GameObject)
                ?.AddComponent<InterruptibleVideoController>();
            //CREATE DESCRIPTOR
            _testDescriptor = ScriptableObject.CreateInstance<InterruptibleVideoDescriptor>();
            _testDescriptor.interruptions = new List<InterruptionDescriptor>();
            _testDescriptor.video = ScriptableObject.CreateInstance<VideoDescriptor>();
            _testDescriptor.video.url = SampleURL;
            //CREATE MATERIAL
            _testMaterial = new Material(Shader.Find("Standard"));
        }

        [Test]
        public void ObjectHasVideoPlayer()
        {
            Assert.NotNull(_testController.gameObject.GetComponent<VideoPlayer>());
        }

        [UnityTest]
        public IEnumerator VideoStarts()
        {
            _testController.targetMaterial = _testMaterial;
            _testController.SetVideoDescriptorAndStart(_testDescriptor);
            yield return new WaitForSeconds(1);
            var videoPlayer = _testController.GetComponent<VideoPlayer>();
            Assert.True(videoPlayer.isPlaying);
        }
    }
}