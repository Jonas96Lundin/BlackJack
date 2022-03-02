using System;

namespace UtilitiesLib
{
    public static class Parser
    {
        /// <summary>
        /// Parse a given string to an int
        /// </summary>
        /// <param name="str">Input string to convert to an int</param>
        /// <returns>Returns the given value if able to parse, otherwise return -1</returns>
        public static int StringToInt(string str)
        {
            int result;
            if (int.TryParse(str, out result))
            {
                return result;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Parse a given string to an int, within a given range
        /// </summary>
        /// <param name="str">Input string to convert to an int</param>
        /// <param name="lowLimit">Lower limit of range</param>
        /// <param name="highLimit">Higher limit of range</param>
        /// <returns>Returns the given value if able to parse and if value is within the limits, otherwise return -1</returns>
        public static int StringToInt(string str, int lowLimit, int highLimit)
        {
            int result;
            if (int.TryParse(str, out result))
            {
                if (result > lowLimit && result < highLimit)
                {
                    return result;
                }
            }
            return -1;
        }

        /// <summary>
        /// Parse a given string to an decimal
        /// </summary>
        /// <param name="str">Input string to convert to an decimal</param>
        /// <returns>Returns the given value if able to parse, otherwise return -1</returns>
        public static decimal StringToDecimal(string str)
        {
            decimal result;
            if (decimal.TryParse(str, out result))
            {
                return result;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Parse a given string to an decimal, within a given range
        /// </summary>
        /// <param name="str">Input string to convert to an decimal</param>
        /// <param name="lowLimit">Lower limit of range</param>
        /// <param name="highLimit">Higher limit of range</param>
        /// <returns>Returns the given value if able to parse and if value is within the limits, otherwise return -1</returns>
        public static decimal StringToDecimal(string str, int lowLimit, int highLimit)
        {
            decimal result;
            if (decimal.TryParse(str, out result))
            {
                if (result > lowLimit && result < highLimit)
                {
                    return result;
                }
            }
            return -1;
        }
    }
}
