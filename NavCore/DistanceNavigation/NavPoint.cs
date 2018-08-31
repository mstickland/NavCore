using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.DistanceNavigation {


    /// <summary>
    /// A platform independant point
    /// </summary>
    public struct NavPoint {
        
        /// <summary>
        /// Horizontal component of position
        /// </summary>
        public float X { get; set; }
        
        /// <summary>
        /// Vertical Component of position
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Constructor. Sets X and Y values. Both values default to 0
        /// </summary>
        /// <param name="x">Horizontal component of the point</param>
        /// <param name="y">Vertical component of the point</param>
        public NavPoint(float x = 0, float y = 0) {
            X = x;
            Y = y;
        }

    }

}
