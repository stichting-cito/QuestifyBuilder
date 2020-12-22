using System;

namespace Questify.Builder.Logic.Service.Classes
{
    [Serializable()]
    public class PublicationProperty
    {
        private string _key;

        private string _value;
        public PublicationProperty()
        {
        }

        public PublicationProperty(string key, string value)
        {
            _key = key;
            _value = value;
        }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

    }
}
