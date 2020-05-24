using Mashup.Core.Exceptions;
using System;

namespace Mashup.Core.Ids
{
    public abstract class StringIdBase
    {
        private string _value;

        public string Value
        {
            get { return _value; }
            set
            {
                try
                {
                    _value = Sanitize(value);
                }
                catch (Exception error)
                {
                    throw new ValidationException($"Failed to validate string \"{value}\" as type {GetType().Name}", error);
                }
            }
        }

        public StringIdBase(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Shall try to sanitize a potentially unsafe value and shall
        /// fail with FormatException if unable.
        /// </summary>
        /// <param name="value">The unsafe value.</param>
        /// <returns>The safe value.</returns>
        protected abstract string Sanitize(string value);
    }
}
