
namespace Mashup.Core.Models
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

        /**
         * Shall try to sanitize a potentially unsafe value and shall
         * fail with FormatException if unable.
         */
        protected abstract string Sanitize(string value);
    }
}
