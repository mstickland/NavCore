using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Util {


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
        /// <param name="x"></param>
        /// <param name="y"></param>
        public NavPoint(float x = 0, float y = 0) {
            X = x;
            Y = y;
        }

    }

}
