using System.Collections.Generic;

namespace TestStructureDemo.Utils
{
    /// <summary>
    /// Serves as an container for all business rules methods related to the user name.
    /// <para>This is an alternative approach to the internal class.</para>
    /// </summary>
    public class ValidationMethods
    {
        /// <summary>
        /// Validates if the input string has a valid length.
        /// </summary>
        /// <param name="name">An input string which must be validated.</param>
        /// <param name="length"></param>
        /// <returns>True if the input string has sufficient length, false otherwise.</returns>
        public static bool ValidateLength(string name, int length)
        {
            return name?.Length > length;
        }

        /// <summary>
        /// Validates if the input string contains an inappropriate words. Returns true if the input string contains an inappropriate word, false otherwise.
        /// </summary>
        /// <param name="name">An input string which must be validated.</param>
        /// <returns>True if the input string has no inappropriate words, false otherwise.</returns>
        public static bool ValidateFuck(string name)
        {
            return !(CheckWords(name, new[] { "fuck", "Fuck", "shit" }));
        }

        /// <summary>
        /// Checks the input string if contains any word which is specified.
        /// </summary>
        /// <param name="name">An input string in which the listed words will be searched.</param>
        /// <param name="wordsToCheck">A collection of words to search within the input string.</param>
        /// <returns>True if the input string contains any of the word listed in the input word collection, false otherwise.</returns>
        private static bool CheckWords(string name, IEnumerable<string> wordsToCheck)
        {
            bool status = true;

            foreach (var word in wordsToCheck)
            {
                status &= name?.Contains(word) ?? false;
            }

            return status;
        }
    }
}