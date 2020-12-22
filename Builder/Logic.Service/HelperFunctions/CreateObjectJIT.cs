using System;

namespace Questify.Builder.Logic.Service.HelperFunctions
{

    public class CreateObjectJIT<T>
    {
        private readonly Func<T> _makeValue;


        public CreateObjectJIT(T currentValue, Func<T> makeValue)
        {
            CurrentValue = currentValue;
            _makeValue = makeValue;
            ValueIsEnsured = !Equals(currentValue, default(T));
        }

        public CreateObjectJIT(Func<T> makeValue)
        {
            _makeValue = makeValue;
            ValueIsEnsured = false;
        }

        public T CurrentValue { get; private set; }


        public T GetEnsuredValue()
        {

            Ensure();
            return CurrentValue;

        }

        public bool ValueIsEnsured { get; private set; }

        public void Ensure()
        {
            if (ValueIsEnsured)
            {
                return;
            }
            CurrentValue = _makeValue();

            ValueIsEnsured = true;
        }

    }
}
