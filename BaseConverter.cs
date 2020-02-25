using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp {

    /// <summary>
    /// https://stackoverflow.com/questions/923771/quickest-way-to-convert-a-base-10-number-to-any-base-in-net
    /// </summary>
    public static class BaseConverter {

        public static int LargestBase => Digits.Length;

        public static char[] Digits { get; } = new char[] {

            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',

            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z',

            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z'

        };

        public static string Convert(int value, int b) {
            string result = value < 0 ? "-" : "";
            do {
                result = Digits[value % b] + result;
                value /= b;
            } while(value > 0);
            return result;
        }

    }

}
