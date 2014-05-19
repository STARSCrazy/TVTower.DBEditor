using System.Collections.Generic;

namespace TVTower.Entities
{
	public class TVTNewsThread
	{
		public TVTNewsTrigger Trigger;
		public int FixStartYear;
		public int YearRangeBegin;
		public int YearRangeEnd;
		public int AfterXDaysIngame;

		public List<TVTNews> News;

		public TVTPersonFunction InvolvedPerson1Function;
		public TVTPersonGender InvolvedPerson1Gender;

		public TVTPersonFunction InvolvedPerson2Function;
		public TVTPersonGender InvolvedPerson2Gender;

		public TVTPersonFunction InvolvedPerson3Function;
		public TVTPersonGender InvolvedPerson3Gender;
	}
}
