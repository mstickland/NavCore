using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavCore.DistanceNavigation;
using NavCore.Navigation;
using NavCore.Navigation.ConnectionFinders;
using NavCore.Navigation.PathWeighters;

namespace NavTests
{
    [TestClass]
    public class NavigationTests
    {

        [TestMethod]
        public void SimpleIntNav()
        {

            var navigator = new Navigator<int>(i => new[] { i + 1 }, i => 1.0);

            var path = navigator.Navigate(1, 10);
            Assert.AreEqual("1-->2-->3-->4-->5-->6-->7-->8-->9-->10", path.ToString());
        }

        [TestMethod]
        public void ComplexIntNav()
        {
            var finder    = new CallbackConnectionFinder<int>(i => new[]{ i + 1, i * 2, i -1 });
            var weighter  = new CallbackWeighter<int>( (c, p, d) => Math.Abs(d - p));
            var navigator = new Navigator<int>(finder, weighter);

            var path = navigator.Navigate(1, 100);
            string expectedPath = "1-->2-->4-->8-->16-->32-->64-->128-->127-->126-->125-->124-->123-->122-->121-->120-->119-->118-->117-->116-->115-->114-->113-->112-->111-->110-->109-->108-->107-->106-->105-->104-->103-->102-->101-->100";
            Assert.AreEqual(expectedPath, path.ToString());
        }

        [TestMethod]
        public void SimplePositionNavigation()
        {
            PositionNode[] nodes = new[] {
                new PositionNode("A", new NavPoint(0, 0)),
                new PositionNode("B1", new NavPoint(100, 300)),
                new PositionNode("B2", new NavPoint(50, 50)),
                new PositionNode("B3", new NavPoint(-50, -50)),
                new PositionNode("C", new NavPoint(100, 100)),
            };
        
            //finds nodes where the starting character of thier name is one greater (e.g B is one greater than A)
            var connectionFinder = new CallbackConnectionFinder<PositionNode>(n => nodes.Where(on => (char)(n.Name[0] + 1) == on.Name[0]));
            var navigator = new Navigator<PositionNode>(connectionFinder, new DistancePathWeighter());
            var path = navigator.Navigate(nodes.First(), nodes.Last());
            Assert.AreEqual("A-->B2-->C", path.ToString());

        }
    }
}
