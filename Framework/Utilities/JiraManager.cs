using System.Linq;
using System.Xml.Linq;
using Atlassian.Jira;
using System.Threading.Tasks;

namespace Framework
{
    class JiraManager
    {
        Jira jira => Jira.CreateRestClient("https://support.vitalchek.com/", "davetestuser", "@K<uBVnv5W(64+z?");

        public async Task UpdateJira(string key, string testStatus, string testName, string screenShotName)
        {
            var issue = await jira.Issues.GetIssueAsync(key);

            switch (testStatus)
            {
                case "Passed":
                    if (issue.Status.Name == "PASS")
                        break;
                    await issue.WorkflowTransitionAsync("PASS");
                    await issue.SaveChangesAsync();
                    break;
                case "Failed":
                    if (issue.Status.Name == "FAILED")
                        break;
                    await issue.WorkflowTransitionAsync("FAIL");
                    issue.AddAttachment(screenShotName); //"../../../TestResults/" + testName + "_" + testStatus + "_" + System.DateTime.Now.ToString("dd_MMMM") + ".png"
                    issue.Assignee= "JHearn";
                    await issue.SaveChangesAsync();
                    break;
            }
        }

        public string GetKey(string fullName)
        {
            var ticketQuery =
                XDocument.Load("../../../Jiras.xml").Descendants("Issue")
                    .Where(t => (t.Element("TestName")?.Value) == fullName)
                    .Select(t => t.Element("Key")?.Value).Single();

            return ticketQuery;
        }
    }
}
