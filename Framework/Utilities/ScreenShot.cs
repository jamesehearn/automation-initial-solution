using OpenQA.Selenium;

namespace Framework
{
    public class ScreenShot
    {
        public void TakeScreenshot(IWebDriver driver, string saveLocation)
        {
            var screenshotDriver = driver as ITakesScreenshot;
            var screenshot = screenshotDriver?.GetScreenshot();
            screenshot?.SaveAsFile(saveLocation, ScreenshotImageFormat.Png);
        }
    }
}
