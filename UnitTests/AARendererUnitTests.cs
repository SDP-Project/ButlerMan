using NUnit.Framework;
using System;
using MyGame;

namespace UnitTests
{
    [TestFixture]
    public class AARendererUnitTests
    {
        Renderable r;

        [SetUp]
        public void Init()
        {
            r = new Renderable();
        }

        [Test]
        public void TestDefaultRegister()
        {
            Assert.IsTrue(Renderer.Renderables.Contains(r));
        }

        [Test]
        public void TestDeregister()
        {
            r.Deregister();
            Assert.IsFalse(Renderer.Renderables.Contains(r));
        }

        [Test]
        public void TestManualRegister()
        {
            r.Deregister();
            r.Register();
            Assert.IsTrue(Renderer.Renderables.Contains(r));
        }
    }
}