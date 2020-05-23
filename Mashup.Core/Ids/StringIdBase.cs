
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
                // FIXME Throw a validation exception
                _value = Sanitize(value);
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
