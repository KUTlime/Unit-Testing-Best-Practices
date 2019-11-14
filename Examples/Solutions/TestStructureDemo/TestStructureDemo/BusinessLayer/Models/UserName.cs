using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace TestStructureDemo.BusinessLayer.Models

{
    /// <summary>
    /// Provides a functionality related to the user name and its business rules.
    /// </summary>
    public class UserName
    {
        /// <summary>
        /// Gets or sets a name value of the user name.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Initialize a new instance of the UserName class from the input string.
        /// </summary>
        /// <param name="name"></param>
        public UserName(string name)
        {
            Name = ValidateAllBusinessRules(name) ? name : "Invalid name.";
        }

        /// <summary>
        /// Validates all business rules which are applied to the user name. Returns true if the input string pass-through all tests, false otherwise.
        /// </summary>
        /// <param name="name">An input string which must be validated.</param>
        /// <returns>True if the input string pass-through all tests, false otherwise.</returns>
        /// <remarks>This approach is based on the premise that business rules implementation will not be shared. Advantage of this approach is that all validation methods can be somehow enforced during class's life cycle.
        /// <remarks>A disadvantage is that business rules implementation can't be shared.</remarks>
        /// <para>Alternative approach would be to make a public ValidationMethod class a do a cherry picking with validation methods within this method.</para>
        /// </remarks>
        public bool ValidateAllBusinessRules(string name)
        {
            bool status = true;
            foreach (var method in new ValidationMethods().GetType().GetMethods().TakeWhile(m => m.Name.Contains("Validate")))
            {
                status &= (bool)method.Invoke(null, new object[] { name });
            }
            return status;
        }

        /// <summary>
        /// Serves as an container for all business rules methods related to the user name.
        /// <para>All class must be static start with "Validate" in order to ensure that will be called in the parent class.</para>
        /// </summary>
        private class ValidationMethods
        {
            /// <summary>
            /// Validates if the input string has a valid length.
            /// </summary>
            /// <param name="name">An input string which must be validated.</param>
            /// <returns>True if the input string has sufficient length, false otherwise.</returns>
            public static bool ValidateLength(string name)
            {
                return name?.Length > 2;
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
}
