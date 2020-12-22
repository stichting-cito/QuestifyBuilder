
namespace Questify.Builder.Logic.Service.Interfaces
{
    public delegate void ReportCompletedEventHandler(object sender, ReportCompletedEventArgs e);

    public interface IReportHandler : IReportValidationBase
    {
        event ReportCompletedEventHandler ReportCompleted;

        string Overview { get; }

        string ValidationErrors { get; }

        string ResultText { get; }

        string ExportedReportLocation { get; }

        string ExtraOptionTask { get; }

        string ExtraOptionTaskDescription { get; }

        bool ShowExtraOptionsTab { get; }

        bool ShowInitialiseProgressTab { get; }

        bool ShowCreateReportProgressTab { get; }

        bool ShowSelectLocationTab { get; }

        bool IsDataGeneratedAsynchronous { get; }

        bool IsInitDataGeneratedAsynchronous { get; }

        bool ShouldUseCollectionAsInput { get; }

        bool ShouldUseGridAsInput { get; }


        object GridToExport { set; }

        System.Windows.Forms.UserControl GetExtraOptionsUI();

        System.Windows.Forms.UserControl GetExportLocationUI();

        void InitialiseData();

        bool GenerateData();

        void ClearErrors();
    }
}
