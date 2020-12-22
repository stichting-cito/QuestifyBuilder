using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows;
using Cinch;
using Questify.Builder.Security;
using Questify.Builder.UI.Wpf.Presentation.Helpers;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.LoginDialog.ViewModels
{
    public class LoginViewModel : ViewModelBase, IDisposable
    {
        private static readonly PropertyChangedEventArgs PasswordChangedArgs = ObservableHelper.CreateArgs<LoginViewModel>(x => x.Password);
        private static readonly PropertyChangedEventArgs UserNameChangedArgs = ObservableHelper.CreateArgs<LoginViewModel>(x => x.UserName);

        private readonly bool _reportCalls;

        private readonly static UserAuthenticator _authenticator = new UserAuthenticator();

        private string _authenthicateError;
        private bool _invalidCredentials;


        public LoginViewModel(bool reportCalls)
        {
            _reportCalls = reportCalls;
            _authenticator.CredentialsNeeded += AuthenticatorOnCredentialsNeeded;

            InitDataWrappers();
            InitCommands();

            this.UserName.DataValue = Environment.UserName;

#if DEBUG
            this.UserName.DataValue = ConfigurationManager.AppSettings.Get("DebugUserName") ?? "administrator";
            this.Password.DataValue = ConfigurationManager.AppSettings.Get("DebugUserPassword") ?? "administrator";
#endif
        }

        public string AuthenthicateError
        {
            get { return _authenthicateError; }
            private set
            {
                _authenthicateError = value;
                NotifyPropertyChanged("AuthenthicateError");
            }
        }

        public SimpleCommand<object, object> CancelCommand
        {
            get;
            set;
        }

        public bool InvalidCredentials
        {
            get { return _invalidCredentials; }
            private set
            {
                _invalidCredentials = value;
                NotifyPropertyChanged("InvalidCredentials");
            }
        }

        public SimpleCommand<object, object> OkCommand
        {
            get;
            set;
        }

        public DataWrapper<string> Password
        {
            get;
            set;
        }

        public DataWrapper<string> UserName
        {
            get;
            set;
        }

        private void AuthenticatorOnCredentialsNeeded(object sender, AuthenticationLoginEventArgs e)
        {
            e.Password = Password.DataValue;
            e.Username = UserName.DataValue;
        }

        private bool CanExecuteOkCommand()
        {
            return true;
        }

        private void ExecuteCancelCommand()
        {
            RaiseCloseRequest(false);
        }

        private void ExecuteOkCommand()
        {
            ValidateCredentials();

            if (!InvalidCredentials)
                RaiseCloseRequest(true);
        }

        public void ValidateCredentials()
        {
            if (string.IsNullOrEmpty(this.UserName.DataValue))
            {
                this.AuthenthicateError = (string)Application.Current.FindResource("Message.EmptyUserName");
                this.InvalidCredentials = true;
            }
            else
            {
                this.Reset();

                var result = LoginViewModel._authenticator.AuthenticateUser(_reportCalls);
                this.InvalidCredentials = !this.VerifyResult(result);
            }
        }

        public void Reset()
        {
            if (!string.IsNullOrEmpty(this.AuthenthicateError))
            {
                this.AuthenthicateError = string.Empty;
            }

            if (this.InvalidCredentials)
            {
                this.InvalidCredentials = false;
            }

            return;
        }

        private void InitCommands()
        {
            OkCommand = new SimpleCommand<object, object>(o => CanExecuteOkCommand(), o => ExecuteOkCommand());
            CancelCommand = new SimpleCommand<object, object>(o => ExecuteCancelCommand());
        }

        private void InitDataWrappers()
        {
            Password = new DataWrapper<string>(this, PasswordChangedArgs);
            UserName = new DataWrapper<string>(this, UserNameChangedArgs);
        }

        private bool VerifyResult(AuthenticationResult result)
        {
            if (result.AuthenticationActionState == (int)AuthenticationActionState.Successful)
                return true;

            AuthenthicateError = (string)Application.Current.FindResource("Message.WrongCredentials");

            return false;
        }

        protected override void OnDispose()
        {
            _authenticator.CredentialsNeeded -= AuthenticatorOnCredentialsNeeded;
            base.OnDispose();
        }
    }
}