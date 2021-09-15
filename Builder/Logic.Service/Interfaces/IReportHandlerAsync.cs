
using System.Threading.Tasks;

namespace Questify.Builder.Logic.Service.Interfaces
{

	/// <summary>
	/// This interface can be implemented by report handlers that choose to create an async version for creating reports.
	/// This is typically done when the task is a long running process.
	/// </summary>
	public interface IReportHandlerAsync : IReportHandler
	{

		/// <summary>
		/// Generates the data asynchronous.
		/// </summary>
		/// <returns>Task(Of System.Boolean).</returns>
		Task<bool> GenerateDataAsync();

		/// <summary>
		/// Cancels the report generation.
		/// </summary>
        void CancelReportGeneration();
	}
}
