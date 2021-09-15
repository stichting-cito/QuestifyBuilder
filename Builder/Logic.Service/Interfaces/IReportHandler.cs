
namespace Questify.Builder.Logic.Service.Interfaces
{
    public delegate void ReportCompletedEventHandler(object sender, ReportCompletedEventArgs e);

    /// <summary>
    /// This interface defines a default contract for plugins that are capable of creating a report.
    /// </summary>
    public interface IReportHandler : IReportValidationBase
	{
        /// <summary>
		/// Occurs when the report is completed.
		/// </summary>
		event ReportCompletedEventHandler ReportCompleted;
		
		/// <summary>
		/// Give an overview of the selected choice just before the report
		/// will be generated.
		/// </summary>
        string Overview { get; }
		
	    /// <summary>
		/// Gets the validation errors.
		/// </summary>
        string ValidationErrors { get; }
		
	    /// <summary>
		/// The text that will be shown after the report is created.
		/// </summary>
        string ResultText { get; }
		
	    /// <summary>
		/// The Location where the report will be placed
		/// </summary>
		string ExportedReportLocation { get; }

		/// <summary>
		/// There a few default steps that should be taken while creating a report
		/// - select location, extra tab, progress, result. e.g. for the excel report
		///  an extra step is needed to select columns. a wizard step should have
		/// a name and description that will be shown in the wizard. 
		/// </summary>
        string ExtraOptionTask { get; }
		
	    /// <summary>
		/// Gets the extra option task description.
		/// See ExtraOptionTask
		/// </summary>
		string ExtraOptionTaskDescription { get; }

		/// <summary>
		/// Indicates if the extra tab should be shown
		/// </summary>
		/// <value>
		///   <c>true</c> if [show extra options]; otherwise, <c>false</c>.
		/// </value>
        bool ShowExtraOptionsTab { get; }
		
	    /// <summary>
		/// The extra option tab, in some cases needs to collect data.
		/// e.g. to select columns in the export report, the collection will be
		/// searched for unique columns. If true the progress tab will be shown before
		/// the extra option tab
		/// </summary>
		/// <value>
		/// <c>true</c> if [show initialise progress]; otherwise, <c>false</c>.
		/// </value>
        bool ShowInitialiseProgressTab { get; }
		
	    /// <summary>
		/// If true, shows progress when the report is generated.
		/// </summary>
		/// <value>
		/// <c>true</c> if [show generate data progress]; otherwise, <c>false</c>.
		/// </value>
        bool ShowCreateReportProgressTab { get; }
		
	    /// <summary>
		/// Indicates if the SelectLocationTab should be shown
		/// </summary>
        bool ShowSelectLocationTab { get; }
		
	    /// <summary>
		/// Gets a value indicating whether this instance is data generated asynchronous.
		/// </summary>
		/// <value><c>true</c> if this instance is data generated asynchronous; otherwise, <c>false</c>.</value>
		bool IsDataGeneratedAsynchronous { get; }

		bool IsInitDataGeneratedAsynchronous { get; }
		
	    //True if the collection is used as input for the reports
        bool ShouldUseCollectionAsInput { get; }
		
	    /// <summary>
		/// = true when the whole grid should be used as input
		/// </summary>
		/// <value>
		/// <c>true</c> if [should use grid as input]; otherwise, <c>false</c>.
		/// </value>
        bool ShouldUseGridAsInput { get; }
		
	    /// <summary>
		/// For the grid.Export
		/// </summary>
		/// <value>
		/// The grid to export.
		/// </value>

		object GridToExport { set; }
		
	    /// <summary>
		/// Function call when the Extra tab is shown. This should return the usercontrol
		/// that will be shown in the Extra Tab
		/// </summary>
		/// <returns>a user control.</returns>
		System.Windows.Forms.UserControl GetExtraOptionsUI();

		/// <summary>
		/// GetsUi when the select Location Tab is shown.
		/// </summary>
		/// <returns>a user control.</returns>
		System.Windows.Forms.UserControl GetExportLocationUI();

		/// <summary>
		/// Initialises the data. This function is called when the ShowInitialiseProgressTab
		/// is set true. And used to e.g. collect data on which later on a choose should be made
		/// e.g. select column of the excel report. 
		/// </summary>
        void InitialiseData();
		
	    /// <summary>
		/// Generates the data.
		/// </summary>
		bool GenerateData();

		/// <summary>
		/// Clears the errors. Used to clear errors when navigation to previous screen.
		/// </summary>
        void ClearErrors();
	}
}
