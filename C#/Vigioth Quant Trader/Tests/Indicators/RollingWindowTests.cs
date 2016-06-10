﻿

using System.Linq;
using NUnit.Framework;
using VigiothCapital.QuantTrader.Indicators;

namespace VigiothCapital.QuantTrader.Tests.Indicators
{
    [TestFixture]
    public class RollingWindowTests
    {
        [Test]
        public void NewWindowIsEmpty()
        {
            var window = new RollingWindow<int>(1);
            Assert.AreEqual(1, window.Size);
            Assert.AreEqual(0, window.Count);
            Assert.AreEqual(0, window.Samples);
            Assert.IsFalse(window.IsReady);
        }

        [Test]
        public void AddsData()
        {
            var window = new RollingWindow<int>(1);
            window.Add(1);
            Assert.AreEqual(1, window.Count);
            Assert.AreEqual(1, window.Samples);
            Assert.AreEqual(1, window.Size);
            Assert.IsFalse(window.IsReady);

            // add one more and the window is ready
            window.Add(2);
            Assert.AreEqual(1, window.Count);
            Assert.AreEqual(2, window.Samples);
            Assert.AreEqual(1, window.Size);
            Assert.IsTrue(window.IsReady);
        }

        [Test]
        public void OldDataFallsOffBackOfWindow()
        {
            var window = new RollingWindow<int>(1);
            window.Add(0);
            Assert.IsFalse(window.IsReady);

            // add one more and the window is ready

            window.Add(1);
            Assert.AreEqual(0, window.MostRecentlyRemoved);
            Assert.AreEqual(1, window.Count);
            Assert.IsTrue(window.IsReady);
        }

        [Test]
        public void IndexingBasedOnReverseInsertedOrder()
        {
            var window = new RollingWindow<int>(3);
            Assert.AreEqual(3, window.Size);

            window.Add(0);
            Assert.AreEqual(1, window.Count);
            Assert.AreEqual(0, window[0]);

            window.Add(1);
            Assert.AreEqual(2, window.Count);
            Assert.AreEqual(0, window[1]);
            Assert.AreEqual(1, window[0]);

            window.Add(2);
            Assert.AreEqual(3, window.Count);
            Assert.AreEqual(0, window[2]);
            Assert.AreEqual(1, window[1]);
            Assert.AreEqual(2, window[0]);

            window.Add(3);
            Assert.AreEqual(3, window.Count);
            Assert.AreEqual(1, window[2]);
            Assert.AreEqual(2, window[1]);
            Assert.AreEqual(3, window[0]);
        }

        [Test]
        public void EnumeratesAsExpected()
        {
            var window = new RollingWindow<int>(3) { 0, 1, 2 };
            var inOrder = window.ToList();
            Assert.AreEqual(2, inOrder[0]);
            Assert.AreEqual(1, inOrder[1]);
            Assert.AreEqual(0, inOrder[2]);
        }

        [Test]
        public void ResetsProperly()
        {
            var window = new RollingWindow<int>(3) { 0, 1, 2 };
            window.Reset();
            Assert.AreEqual(0, window.Samples);
        }
        [Test]
        public void RetievesNonZeroIndexProperlyAfterReset()
        {
            var window = new RollingWindow<int>(3);
            window.Add(0);
            Assert.AreEqual(1, window.Count);
            Assert.AreEqual(0, window[0]);

            window.Add(1);
            Assert.AreEqual(2, window.Count);
            Assert.AreEqual(0, window[1]);
            Assert.AreEqual(1, window[0]);

            window.Add(2);
            Assert.AreEqual(3, window.Count);
            Assert.AreEqual(0, window[2]);
            Assert.AreEqual(1, window[1]);
            Assert.AreEqual(2, window[0]);

            window.Add(3);
            Assert.AreEqual(3, window.Count);
            Assert.AreEqual(1, window[2]);
            Assert.AreEqual(2, window[1]);
            Assert.AreEqual(3, window[0]);

            window.Reset();
            window.Add(0);
            Assert.AreEqual(1, window.Count);
            Assert.AreEqual(0, window[0]);

            window.Add(1);
            Assert.AreEqual(2, window.Count);
            Assert.AreEqual(0, window[1]);
            Assert.AreEqual(1, window[0]);

            window.Add(2);
            Assert.AreEqual(3, window.Count);
            Assert.AreEqual(0, window[2]);
            Assert.AreEqual(1, window[1]);
            Assert.AreEqual(2, window[0]);

            window.Add(3);
            Assert.AreEqual(3, window.Count);
            Assert.AreEqual(1, window[2]);
            Assert.AreEqual(2, window[1]);
            Assert.AreEqual(3, window[0]);
        }
    }
}
