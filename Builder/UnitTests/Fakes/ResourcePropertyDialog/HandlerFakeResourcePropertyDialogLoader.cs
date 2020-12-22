using System;
using System.Diagnostics;
using FakeItEasy;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.Interfaces;

namespace Questify.Builder.UnitTests.Fakes.ResourcePropertyDialog
{
    class HandlerFakeResourcePropertyDialogLoader
    {
        private readonly IResourcePropertyDialogObjectFactory _fakeFactory;
        private readonly ResourcePropertyDialogObjectStrategy _strategy;

        public HandlerFakeResourcePropertyDialogLoader(IResourcePropertyDialogObjectFactory fakeFactory, FakeResourcePropertyDialogObjectFactoryBehaviorAttribute attribute)
        {
            _fakeFactory = fakeFactory;
            _strategy = attribute.Strategy;

            InitCallRewire();
        }

        private void InitCallRewire()
        {
            var CallToGetReqObjs = A.CallTo(() => _fakeFactory.GetRequiredObjectsForPropertyEntityWithId(A<Guid>.Ignored, A<Type>.Ignored));

            switch (_strategy)
            {
                case ResourcePropertyDialogObjectStrategy.ReturnNull:
                    CallToGetReqObjs.ReturnsLazily((arg) => ReturnNull(arg.GetArgument<Guid>(0), arg.GetArgument<Type>(1)));
                    break;
                case ResourcePropertyDialogObjectStrategy.GiveException:
                    CallToGetReqObjs.ReturnsLazily((arg) => GiveException(arg.GetArgument<Guid>(0), arg.GetArgument<Type>(1)));
                    break;
                case ResourcePropertyDialogObjectStrategy.DefaultObjects:
                    CallToGetReqObjs.ReturnsLazily((arg) => DefaultObjects(arg.GetArgument<Guid>(0), arg.GetArgument<Type>(1)));
                    break;
                case ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum:
                    CallToGetReqObjs.ReturnsLazily((arg) => AbsoluteValidMinimum(arg.GetArgument<Guid>(0), arg.GetArgument<Type>(1)));
                    break;
                default:
                    Debug.Assert(false, "Not handled strategy!!");
                    break;
            }
        }

        private IPropertyEntity ReturnNull(Guid id, Type type)
        {
            return null;
        }

        private IPropertyEntity GiveException(Guid id, Type type)
        {
            throw new Exception();
        }

        private IPropertyEntity DefaultObjects(Guid id, Type type)
        {
            var instance = Activator.CreateInstance(type);

            if (instance is IPropertyEntity)
            {
                IPropertyEntity propertyEntity = (IPropertyEntity)instance;
                propertyEntity.Id = id;
                return propertyEntity;
            }

            return null;
        }

        private IPropertyEntity AbsoluteValidMinimum(Guid id, Type type)
        {
            var instance = Activator.CreateInstance(type);

            if (instance is IPropertyEntity)
            {
                IPropertyEntity propertyEntity = (IPropertyEntity)instance;
                propertyEntity.Id = id;
                return propertyEntity;
            }

            return null;
        }
    }
}
