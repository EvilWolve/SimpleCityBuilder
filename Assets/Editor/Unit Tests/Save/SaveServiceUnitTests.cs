using System;
using NUnit.Framework;
using Save;
using UnityEngine;
using System.Collections.Generic;

namespace UnitTests.Save
{
    public class TaskLibraryTests
    {
        ISaveService saveService;

        [SetUp]
        public void Setup()
        {
            this.saveService = new PlayerPrefsSaveService();
        }

        [Test]
        public void TestSaveAndLoadClass()
        {
            TestClass testClass = new TestClass ()
            {
                flag = true,
                number = 1,
                fraction = 1/3f,
                text = "Test text",
                array = new int[] { 2, 4, 8, 16},
                list = new List<int>() { 32, 64, 128}
            };

            string key = "classKey";

            this.saveService.Save(key, testClass);

            TestClass loadedClass = this.saveService.Load<TestClass>(key);

            Assert.IsNotNull (loadedClass);
            Assert.AreEqual (testClass.flag, loadedClass.flag);
            Assert.AreEqual (testClass.number, loadedClass.number);
            Assert.AreEqual (testClass.fraction, loadedClass.fraction);
            Assert.AreEqual (testClass.text, loadedClass.text);
            Assert.AreEqual (testClass.array, loadedClass.array);
        }

        [Test]
        public void TestSaveClearAndLoadClass()
        {
            TestClass testClass = new TestClass ()
            {
                flag = true,
                number = 1,
                fraction = 1/3f,
                text = "Test text",
                array = new int[] { 2, 4, 8, 16},
                list = new List<int>() { 32, 64, 128}
            };

            string key = "classKey";

            this.saveService.Save(key, testClass);
            
            PlayerPrefs.DeleteAll();

            TestClass loadedClass = this.saveService.Load<TestClass>(key);

            Assert.IsNull(loadedClass);
        }

        [Serializable]
        class TestClass
        {
            public bool flag;
            public int number;
            public float fraction;
            public string text;
            public int[] array;
            public List<int> list;
        }
    }
}