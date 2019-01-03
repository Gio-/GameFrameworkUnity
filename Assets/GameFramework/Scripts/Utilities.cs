///
/// GameFramework 
/// 

/// A list of Utility you can use and exapnd as you
/// want.

namespace GameFramework
{
    public class Utilities 
    {
        /// <summary>
        /// Convert bitmask to layer int. 
        /// </summary>
        /// <param name="bitmask">bitmask to convert</param>
        /// <returns>Return the correct layer int.</returns>
        public static int ToLayer(int bitmask)
        {
            int result = bitmask > 0 ? 0 : 31;
            while (bitmask > 1)
            {
                bitmask = bitmask >> 1;
                result++;
            }
            return result;
        }
    }
}