using Questify.Builder.Security;

namespace Questify.Builder.Logic.Service.Decorators
{

    public abstract class BasePermissionServiceDecorator : IPermissionService
    {

        private IPermissionService decoree;

        public BasePermissionServiceDecorator(IPermissionService decoree)
        {
            this.decoree = decoree;
        }

        public virtual System.Boolean TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget, TestBuilderPermissionNamedTask targettedNamedTask, System.Int32 bankId, System.Int32 entityInstanceId)
        {
            return decoree.TryUserIsPermittedToNamedTask(access, permissionTarget, targettedNamedTask, bankId, entityInstanceId);
        }

        public virtual System.Boolean TryUserIsPermittedTo(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget, System.Int32 bankId)
        {
            return decoree.TryUserIsPermittedTo(access, permissionTarget, bankId);
        }

        public virtual void UserIsPermittedToNamedTask(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget, TestBuilderPermissionNamedTask targettedNamedTask, System.Int32 bankId, System.Int32 entityInstanceId)
        {
            decoree.UserIsPermittedToNamedTask(access, permissionTarget, targettedNamedTask, bankId, entityInstanceId);
        }

        public virtual System.Boolean UserIsPermittedTo(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget, System.Int32 bankId)
        {
            return decoree.UserIsPermittedTo(access, permissionTarget, bankId);
        }

    }
}
