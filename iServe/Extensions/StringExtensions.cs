using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace System {
    public static class StringExtensions {
        /// <summary>
        /// HTML-encodes a string and returns the encoded string.
        /// </summary>
        /// <param name="s">
        /// The text string to encode.
        /// </param>
        /// <returns>
        /// The HTML-encoded text.
        /// </returns>
        public static string HtmlEncode(this string s) {
            return System.Web.HttpUtility.HtmlEncode(s);
        }

        /// <summary>
        /// Determines if a string is empty or contains only whitespace.
        /// </summary>
        /// <param name="s">
        /// The string to check whether it is empty or contains only whitespace.
        /// </param>
        /// <returns>
        /// A boolean determining whether the string is empty or contains only whitespace.
        /// </returns>
        public static bool IsEmpty(this string s) {
            return s == null || Regex.IsMatch(s, @"^\s*$");
        }

        /// <summary>
        /// Truncates a string to the specified number of characters.
        /// </summary>
        /// <param name="length">
        /// The number of characters to truncate the string to.
        /// </param>
        /// <param name="terminator">
        /// A string representing an ellipsis (i.e. "...", "->", etc).
        /// </param>
        /// <returns></returns>
		public static string Truncate(this string s, int length, string terminator) {
			string output = s;
            length -= terminator.Length;
			if (s != null && s.Length > length ) {
				output = s.Substring(0, length) + terminator;
			}

			return output;
		}
    }
}
