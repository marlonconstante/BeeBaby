using System;

namespace BeeBaby.Synchronization
{
	public interface ISyncEvent
	{
		/// <summary>
		/// Starteds the synchronizing.
		/// </summary>
		void StartedSynchronizing();

		/// <summary>
		/// Endeds the synchronizing.
		/// </summary>
		void EndedSynchronizing();
	}
}