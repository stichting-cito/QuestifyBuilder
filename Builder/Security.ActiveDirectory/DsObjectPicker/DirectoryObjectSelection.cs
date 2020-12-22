
namespace Questify.Builder.Security.ActiveDirectory.DsObjectPicker
{

    public class DirectoryObjectSelection
    {
        private readonly string _fullName;
        private readonly string _userName;

        public string FullName
        {
            get { return _fullName; }
        }

        public string UserName
        {
            get { return _userName; }
        }

        internal DirectoryObjectSelection(string userName, string fullName)
        {
            _userName = userName;
            _fullName = fullName;
        }
    }
}
