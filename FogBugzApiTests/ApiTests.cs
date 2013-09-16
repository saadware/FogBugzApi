using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using FogBugzApi;

namespace FogBugzApiTests
{
    [TestFixture]
    public class ApiTests
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
        public void TestInfo()
        {
            Assert.AreEqual("api.asp?", apiWrapper.ApiInfo.Url);
            Assert.AreEqual("1", apiWrapper.ApiInfo.MinVersion);
            Assert.AreEqual("8", apiWrapper.ApiInfo.Version);
        }

        [Test]
        public void TestProjects()
        {
            var projects = apiWrapper.GetProjects();
            Assert.IsNotNull(projects);
            Assert.AreNotEqual(0, projects.Count());
            Assert.IsFalse(projects.All(proj => string.IsNullOrEmpty(proj.Name)));
        }

        [Test]
        public void TestPeople()
        {
            IEnumerable<FogBugzPerson> people = apiWrapper.GetPeople();
            Assert.IsNotNull(people);
            Assert.AreNotEqual(0, people.Count());
            Assert.IsFalse(people.All(p => string.IsNullOrEmpty(p.Email)));
        }

        [Test]
        public void TestPeopleIntervalsWithThrash()
        {
            var intervals = (from i in apiWrapper.GetIntervals(DateTime.Today.AddDays(-14), DateTime.Today, true)
                             group i by i.PersonId into g1
                             select new
                             {
                                 PersonId = g1.Key,
                                 TotalTimes = g1.Sum(t => t.End.Subtract(t.Start).TotalHours),
                                 Cases = (from c in g1.ToList()
                                          group c by c.CaseId into g2
                                          select new
                                          {
                                              CaseId = g2.Key,
                                              TotalCaseTime = g2.Sum(t => t.End.Subtract(t.Start).TotalHours)
                                          }).ToList()
                             }).ToList();
            Assert.AreNotEqual(0, intervals.Count);

            // Get list of cases for above intervals
            var caseIds = (from i in intervals
                           from c in i.Cases
                           select c.CaseId).Distinct().ToList();
            
            // Get people 
            var people = apiWrapper.GetPeople();

            var cases = (from i in intervals
                         let casesWorked = apiWrapper.GetCases(caseIds)
                         let statuses = apiWrapper.GetStatuses()
                         join p in people on i.PersonId equals p.PersonId
                         select new
                         {
                             WorkdOnBy = p.FullName,
                             TotalTime = i.TotalTimes,
                             Thrash = 1 - ((double)(from intCase in i.Cases
                                                    join cWorked in casesWorked on intCase.CaseId equals cWorked.CaseId
                                                    join st in statuses on cWorked.StatusId equals st.StatusId
                                                    where st.IsResolved
                                                    select intCase.CaseId).Count() / (double)i.Cases.Count()),
                             Cases = (from c in i.Cases
                                      join c1 in casesWorked on c.CaseId equals c1.CaseId
                                      select new
                                      {
                                          CaseId = c.CaseId,
                                          CaseTitle = c1.Title,
                                          CaseStatus = c1.Status
                                      }).ToList()
                         }).ToList();

            Assert.AreNotEqual(0, cases.Count);            
        }
    }
}
