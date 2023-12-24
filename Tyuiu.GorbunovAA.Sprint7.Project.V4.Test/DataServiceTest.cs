using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using System.IO;

using Tyuiu.GorbunovAA.Sprint7.Project.V4.Lib;

namespace Tyuiu.GorbunovAA.Sprint7.Project.V4.Test
{
    [TestClass]
    public class DataServiceTest
    {
        [TestMethod]
        public void SearchInPutFile()
        {
            string path = @"C:\Users\BobaBibkov\source\repos\Tyuiu.GorbunovAA.Sprint7\Tyuiu.GorbunovAA.Sprint7.Project.V4\Baza.csv";
            FileInfo info = new FileInfo(path);
            bool res = info.Exists;
            bool wait = true;
            Assert.AreEqual(wait, res);
        }
    }
}
