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
        /// Gets the distance the Distance between the two points, squared for faster speed
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double GetDistanceSq(int x1, int y1, int x2, int y2)
        {

            double dx = (x2 - x1);
            double dy = (y2 - y1);

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


        /// <summary>
        /// Gets the distance the Distance between the two points
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double GetDistance(int x1, int y1, int x2, int y2)
        {

            return Math.Sqrt(GetDistanceSq(x1, y1, x2, y2));
        }


    }
}
