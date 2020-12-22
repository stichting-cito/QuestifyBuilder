using Questify.Builder.Model.ContentModel.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Questify.Builder.Logic.Service.Direct
{
    public class ExcludeIncludeFieldsHelper
    {
        private ExcludeIncludeFieldsList _stateExcludedFieldList;

        private ExcludeIncludeFieldsList _userExcludedFieldList;
        private ExcludeIncludeFieldsList _userIncludedFieldList;

        private ExcludeIncludeFieldsList _bankIncludedFieldList;

        private ExcludeIncludeFieldsList _resourceIncludedFieldList;

        public ExcludeIncludeFieldsList GetUserExcludedFieldList()
        {
            if (_userExcludedFieldList != null)
            {
                return _userExcludedFieldList;
            }

            _userExcludedFieldList = new ExcludeIncludeFieldsList();
            _userExcludedFieldList.Add(UserFields.UserSettings);
            return _userExcludedFieldList;
        }

        public ExcludeIncludeFieldsList GetUserIncludedFieldList()
        {
            if (_userIncludedFieldList != null)
            {
                return _userIncludedFieldList;
            }

            _userIncludedFieldList = new ExcludeIncludeFieldsList(false);
            _userIncludedFieldList.Add(UserFields.Id);
            _userIncludedFieldList.Add(UserFields.FullName);
            return _userIncludedFieldList;
        }

        public ExcludeIncludeFieldsList GetResourceIncludedFieldList()
        {
            if (_resourceIncludedFieldList == null)
            {
                _resourceIncludedFieldList = new ExcludeIncludeFieldsList(false);
                _resourceIncludedFieldList.Add(ResourceFields.ResourceId);
            }
            return _resourceIncludedFieldList;
        }

        public ExcludeIncludeFieldsList GetStateExcludedFieldList()
        {
            if (_stateExcludedFieldList == null)
            {
                _stateExcludedFieldList = new ExcludeIncludeFieldsList();
                _stateExcludedFieldList.Add(StateFields.Description);
            }
            return _stateExcludedFieldList;
        }

        public ExcludeIncludeFieldsList GetBankIncludedFieldList()
        {
            if (_bankIncludedFieldList == null)
            {
                _bankIncludedFieldList = new ExcludeIncludeFieldsList(false);
                _bankIncludedFieldList.Add(BankFields.Id);
            }
            return _bankIncludedFieldList;
        }
    }
}