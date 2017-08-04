using NUnit.Framework;
using UI_Tests.Data;
using Framework.Pages;

namespace UI_Tests.Smoke
{
    [Parallelizable]
    [TestFixture]
    class Smoke : BaseTest
    {
        private DataObject Data;
        private HomePage Home;
        private MyAccountPage MyAccount;
        //private LoginWorkflow LoginWorkflow;
        //private BirthSearchPage BirthSearch;
        //private DeathSearchPage DeathSearch;
        //private BirthSearchResultsPage BirthSearchResults;
        //private DeathSearchResultsPage DeathSearchResults;
        //private ChildPage Child;
        //private DecedentPage Decedent;

        [SetUp]
        public new void SetUp()
        {
            Data = new DataObject();
            Home = new HomePage(Driver);
            MyAccount = new MyAccountPage(Driver);
            //LoginWorkflow = new LoginWorkflow(Driver);
            //BirthSearch = new BirthSearchPage(Driver);
            //DeathSearch = new DeathSearchPage(Driver);
            //BirthSearchResults = new BirthSearchResultsPage(Driver);
            //DeathSearchResults = new DeathSearchResultsPage(Driver);
            //Child = new ChildPage(Driver);
            //Decedent = new DecedentPage(Driver);
        }

        [Test]
        [Category("Smoke")]
        public void LoginTest()
        {
            //SetData(1);
            //LoginWorkflow.InitialLogin(Data);
            Home.GoTo(BaseUrl);
            Assert.That(Home.PageDisplays);
            Assert.That(Driver.Title, Is.EqualTo("Automation – Site for Automation and Testing Demonstrations"));

            Home.MyAccount();

            Assert.That(MyAccount.PageDisplays);

            MyAccount.Login("autocustomer", "65O@ZTr8sVFW5L^4#pw&ayag");

            Assert.That(MyAccount.UserContentDisplays);
        }
        private void SetData(int row)
        {
            //Data.Target = Lib.Data(row, "target");
        }
    }
}
