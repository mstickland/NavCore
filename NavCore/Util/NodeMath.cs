using NavCore.DistanceNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NavCore.Util {

    public static class NodeMath {


        /// <summary>
        /// Gets the distance the Distance between the two points, squared for faster speed
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetDistanceSq(NavPoint p1, NavPoint p2) {

            double dx = (p2.X - p1.X);
            double dy = (p2.Y - p1.Y);

            return (dx * dx) + (dy * dy);
        }


        /// <summary>
        /// Gets the distance the Distance between the two points
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetDistance(NavPoint p1, NavPoint p2) {

            return Math.Sqrt(GetDistanceSq(p1, p2));
        }

    }
}
