using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FogBugzApi;
using System.Configuration;

namespace FogBugzApiTests
{
	[TestFixture]
	public class IntervalTests
	{
		/// <summary>
		/// Api
		/// </summary>
		static ApiWrapper apiWrapper;

		[TestFixtureSetUp]
		public static void Init()
		{
			string url = ConfigurationManager.AppSettings["FogBugzUrl"];
			string token = ConfigurationManager.AppSettings["ApiToken"];
			apiWrapper = new ApiWrapper(url, token);
		}

		[Test]
		public void TestBasicIntervals()
		{
			IEnumerable<FogBugzInterval> intervals = apiWrapper.GetIntervals( DateTime.Today.AddDays( -14 ), DateTime.Today, true );
			Assert.IsNotNull( intervals );
			Assert.AreNotEqual( 0, intervals.Count() );
			Assert.IsFalse( intervals.All( i => string.IsNullOrEmpty( i.CaseTitle ) ) );
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void FutureDatas()
		{
			var intervals = apiWrapper.GetIntervals(DateTime.Today, DateTime.Today.AddDays(1), false);
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void MixedUpStartEndDates()
		{
			var intervals = apiWrapper.GetIntervals(DateTime.Today.AddDays(-14), DateTime.Today.AddDays(-21), false);
		}


		[Test]
		public void StartDateOnly()
		{
			DateTime startDate = DateTime.Today.AddDays(-120);
			var intervals = apiWrapper.GetIntervals(startDate, null, false);
			Assert.IsNotNull(intervals);
			Assert.AreNotEqual(0, intervals.Count());
		}

		[Test]
		public void EndDateOnly()
		{
			DateTime endDate = DateTime.Today.AddDays(-365);
			var intervals = apiWrapper.GetIntervals(null, endDate, false);
			Assert.IsNotNull(intervals);
			Assert.AreNotEqual(0, intervals.Count());
		}
	}
}
