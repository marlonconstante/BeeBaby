using System.Linq;
using NUnit.Framework;
using Skahal.Infrastructure.Framework.Repositories;
using Domain.Moment;
using Infrastructure.Repositories.Dropbox;

namespace Infrastructure.Repositories.FunctionalTest.Repositories
{
    [TestFixture]
    public class DropboxRepositoryBaseTest
    {
        [Test]
        public void PersistNewItem_Item_PersistedOnDropbox()
        {
            var unitOfWork = new MemoryUnitOfWork();
			var target = new DropboxRepositoryBase<Moment>("TEST");
            target.ClearAll();
            target.SetUnitOfWork(unitOfWork);

			var moment = new Moment();
			moment.Description = "TEST";
			target.Add(moment);

            var originalCount = target.CountAll();
            unitOfWork.Commit();

            Assert.AreEqual(originalCount + 1, target.CountAll());
        }

//        [Test]
//        public void CountAll_Filter_FiltedResults()
//        {
//            var unitOfWork = new MemoryUnitOfWork();
//            var target = new DropboxRepositoryBase<ContainsTextRule>("TEST");
//            target.ClearAll();
//            target.SetUnitOfWork(unitOfWork);
//
//            var rule = new ContainsTextRule("TEST_1");
//            target.Add(rule);
//
//            rule = new ContainsTextRule("TEST_2");
//            target.Add(rule);
//
//            unitOfWork.Commit();
//
//            Assert.AreEqual(2, target.CountAll());
//            Assert.AreEqual(2, target.CountAll(c => c.Text.StartsWith("TEST")));
//            Assert.AreEqual(2, target.CountAll(c => c.Text.Contains("_")));
//            Assert.AreEqual(1, target.CountAll(c => c.Text.EndsWith("_1")));
//            Assert.AreEqual(1, target.CountAll(c => c.Text.EndsWith("_2")));
//        }
//
//        [Test]
//        public void FindAll_Filter_FiltedResults()
//        {
//            var unitOfWork = new MemoryUnitOfWork();
//            var target = new DropboxRepositoryBase<ContainsTextRule>("TEST");
//            target.ClearAll();
//            target.SetUnitOfWork(unitOfWork);
//
//            var rule = new ContainsTextRule("TEST_1");
//            target.Add(rule);
//
//            rule = new ContainsTextRule("TEST_2");
//            target.Add(rule);
//
//            unitOfWork.Commit();
//
//            Assert.AreEqual(2, target.FindAll().Count());
//            Assert.AreEqual(2, target.FindAll(0, 2).Count());
//            Assert.AreEqual(1, target.FindAll(0, 1).Count());
//            Assert.AreEqual(1, target.FindAll(1, 2).Count());
//            Assert.AreEqual(2, target.FindAll(c => c.Text.StartsWith("TEST")).Count());
//            Assert.AreEqual(2, target.FindAll(c => c.Text.Contains("_")).Count());
//            Assert.AreEqual(1, target.FindAll(c => c.Text.EndsWith("_1")).Count());
//            Assert.AreEqual(1, target.FindAll(c => c.Text.EndsWith("_2")).Count());
//        }
//
//        [Test]
//        public void Pesists_Item_Persisted()
//        {
//            var unitOfWork = new MemoryUnitOfWork();
//            var target = new DropboxRepositoryBase<ContainsTextRule>("TEST");
//            target.ClearAll();
//            target.SetUnitOfWork(unitOfWork);
//
//            var rule = new ContainsTextRule("TEST_1");
//            target.Add(rule);
//
//            rule = new ContainsTextRule("TEST_2");
//            target.Add(rule);
//
//            unitOfWork.Commit();
//
//            var actual = target.FindAll().ToList();
//            Assert.AreEqual(2, actual.Count);
//            Assert.AreEqual("TEST_1", actual[0].Text);
//            Assert.AreEqual("TEST_2", actual[1].Text);
//
//            var actualRecord = actual[1];
//            actualRecord.Text += " (UPDATED)";
//            target[actualRecord.Id] = actualRecord;
//            target.Remove(actual[0]);
//            unitOfWork.Commit();
//
//            actual = target.FindAll().ToList();
//            Assert.AreEqual(1, actual.Count);
//            Assert.AreEqual("TEST_2 (UPDATED)", actual[0].Text);
//        }
    }
}
