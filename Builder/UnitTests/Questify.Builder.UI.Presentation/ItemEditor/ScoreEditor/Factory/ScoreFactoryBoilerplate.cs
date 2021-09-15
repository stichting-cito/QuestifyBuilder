
using System;
using System.Diagnostics;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    /// <summary>
    /// This class defines some basic behavior. THis class is internal since objects are not supposed to be public in real implementations.
    /// </summary>
    /// <typeparam name="TFactory">The type of the t factory.</typeparam>
    /// <typeparam name="TScoreParam">The type of the t score parameter.</typeparam>
    /// <typeparam name="TScoringViewModel">The type of the t scoring view model.</typeparam>
    internal class ScoreFactoryBoilerplate<TFactory, TScoreParam, TScoringViewModel> : IBaseScoreFactoryTest 
        where TFactory : IScoringParameterWorkspaceFactory, new() 
        where TScoreParam : ScoringParameter, new()
        where TScoringViewModel : ScoringViewModel<TScoreParam>
    {
        
        public void Can_Handle_SpecificScoringParameterParameter()
        {
            //Arrange
            var fact = new TFactory();
            //Act
            var result = fact.CanHandle(new TScoreParam());
            //Assert
            Assert.IsTrue(result);
        }

        
        public void CanNot_Handle_someOtherScoreParam()
        {
            //Arrange
            var fact = new TFactory();
            //Act
            var result = fact.CanHandle(new someOtherScoreParam());
            //Assert
            Assert.IsFalse(result);
        }

        public void CanNot_Handle_NULL_ScoringParameter()
        {
            //Arrange
            var fact = new TFactory();
            //Act
            var result = fact.CanHandle(null);
            //Assert
            Assert.Fail();//Should not come here.
        }

        
        public void WorkspaceData_IsUsableByViewModel()
        {
            //Arrange
            var fakeVAS = new Cinch.TestViewAwareStatus();

            Debug.Assert(CreateViewModel != null, "Please set this lambda!");

            TScoringViewModel MCVM = CreateViewModel(fakeVAS);
            var fact = new TFactory();
            var workspaceData = fact.Create(new TScoreParam() { ControllerId = "test" }, new Solution());
            var fakeView = A.Fake<Cinch.IWorkSpaceAware>();
            fakeView.WorkSpaceContextualData.DataValue = workspaceData.DataValue;
            fakeVAS.View = fakeView;
            //Act
            fakeVAS.SimulateViewIsLoadedEvent();
            //Assert
            Assert.IsNotNull(MCVM.ScoreParameter, "Scoring parameter should not be null");
        }

        public Func<Cinch.TestViewAwareStatus, TScoringViewModel> CreateViewModel { get; set; }
      

        class someOtherScoreParam : ScoringParameter
        {
            public override int? AlternativesCount
            {
                get { return null; }
            }
        }


        Func<Cinch.TestViewAwareStatus, object> IBaseScoreFactoryTest.CreateViewModel
        {
            get
            {
                return CreateViewModel;
            }
            set
            {
                CreateViewModel = cinch => (TScoringViewModel)value(cinch);
            }
        }
    }
}
