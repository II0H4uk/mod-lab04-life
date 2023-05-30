using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;

namespace cli_life
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Program test = new Program();
            Program.Reset();
            Program.Load("system1.txt");
            Program.Render();
            Assert.IsTrue(Program.countLive == 47);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Program test = new Program();
            Program.Reset();
            Program.Load("system on 72 step.txt");
            Program.Render();
            Assert.IsTrue(Program.countLive == 118);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Program test = new Program();
            Program.Reset();
            Program.Load("system1.txt");
            Program.Render();
            Program.Info();

            string[] expected = File.ReadAllLines("../../../../Life/expected1.txt");
            List<string> expectedList = new List<string>();
            foreach (string a in expected)
                expectedList.Add(a);
            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.AreEqual(expectedList[i], Program.check[i]);
            }
        }

        [TestMethod]
        public void TestMethod4()
        {
            Program test = new Program();
            Program.Reset();
            Program.Load("system2.txt");
            Program.Render();
            Program.Info();

            string[] expected = File.ReadAllLines("../../../../Life/expected2.txt");
            List<string> expectedList = new List<string>();
            foreach (string a in expected)
                expectedList.Add(a);
            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.AreEqual(expectedList[i], Program.check[i]);
            }
        }

        [TestMethod]
        public void TestMethod5()
        {
            Program test = new Program();
            Program.Reset();
            Program.Load("system3.txt");
            Program.Render();
            Program.Info();

            string[] expected = File.ReadAllLines("../../../../Life/expected3.txt");
            List<string> expectedList = new List<string>();
            foreach (string a in expected)
                expectedList.Add(a);
            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.AreEqual(expectedList[i], Program.check[i]);
            }
        }

    }
}
