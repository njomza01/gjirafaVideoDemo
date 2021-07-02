using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using FluentAssertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Interactions;

namespace VideoTest1
{
    public class Program
    {

        IWebDriver driver = new ChromeDriver();


        static void Main(string[] args)
        {


        }


        [OneTimeSetUp]
        public void Initialize()
        {
            driver.Navigate().GoToUrl("https://video.gjirafa.com/explicit-sample");
            driver.Manage().Window.Maximize();
            Console.WriteLine("Opened the Url");
        }


        [Test, Order(0)]
        public void LoginTest()
        {

            IWebElement LoginButton = driver.FindElement(By.CssSelector("#app-menu > div.container.mall__menu-container.d-flex.align-items-center > nav.mall__menu-user.d-none.d-lg-block > button"));
            LoginButton.Click();


            IWebElement IFrame = driver.FindElement(By.Id("ssoPopup_iframe"));
            driver.SwitchTo(IFrame);


            IWebElement Email = driver.FindElement(By.Id("Username"));
            IWebElement Password = driver.FindElement(By.Id("Password"));



            Email.SendKeys("njomeza@gjirafa.com");
            Password.SendKeys("123456");

            //Kuçu buttoni
            driver.FindElement(By.CssSelector("#event-btn")).Click();
            Thread.Sleep(5000);

            Console.WriteLine("Successfully Loged In");

        }

      
        //[Test, Order(1)]
        //public void SkipAdTest()
        //{

        //   // IWebElement bannerVideo = driver.FindElement(By.CssSelector("#homepage__sections > div:nth-child(3) > section > div > div.video-grid__body > div > div:nth-child(6) > a"));
        //   //bannerVideo.Click();
        //   // var isAt = driver.WaitUntilUrlContains("/rrushe");
        //    //isAt.Should().BeTrue(because: "It didnt find the specific url");

        //    Thread.Sleep(16000); //wait until skip ad button is visible

        //    IWebElement PlayPauseButton = driver.FindElement(By.CssSelector("#vp-player > div.vp-controls > div.vp-controls-holder > div > div.vp-buttons > button.vp-play-pause.vp-button.vp-tooltip"));
        //    PlayPauseButton.Click();

        //    //frame ne te cilin ndodhet butoni "SkipAd"
        //    IWebElement IFrame = driver.FindElement(By.CssSelector("#vp-player > div.gj--ads > div > iframe:nth-child(3)"));
        //    driver.SwitchTo(IFrame);


        //    //Skip AD butoni
        //    IWebElement SkipAdButton = driver.FindElement(By.CssSelector("body > div.videoAdUi > div.videoAdUiBottomBar > div > div.videoAdUiSkipContainer.html5-stop-propagation > button"));
        //    SkipAdButton.Click();
        //}
        
        [Test, Order(2)]
        public void AutoplayBtn()
        {
            IWebElement AutoplayBtn = driver.FindElement(By.Id("autoplay-switch"));
            AutoplayBtn.Click();
            Thread.Sleep(2000);
            AutoplayBtn.Click();
        }

        [Test, Order(3)]
        public void LikeVideoTest()
        {
            driver.Navigate().GoToUrl("https://video.gjirafa.com/explicit-sample");
            IWebElement PlayPauseButton = driver.FindElement(By.CssSelector("#vp-player > div.vp-controls > div.vp-controls-holder > div > div.vp-buttons > button.vp-play-pause.vp-button.vp-tooltip"));
            PlayPauseButton.Click();
            IWebElement LikeButton = driver.FindElement(By.ClassName("icon-like"));
            IWebElement UnlikeButton = driver.FindElement(By.ClassName("icon-dislike"));
            LikeButton.Click();
            Thread.Sleep(2000);
            UnlikeButton.Click();

        } 

        [Test, Order(4)]
        public void SaveVideo()
        {
            IWebElement SaveBtn = driver.FindElement(By.CssSelector(".icon-bookmark"));
            SaveBtn.Click();
            Thread.Sleep(2000);
            SaveBtn.Click();

        }


        [Test, Order(6)]
        public void ShareVideo()
        {
            
            IWebElement ShareBtn = driver.FindElement(By.Id("share-video"));
            driver.ScrollToElement(ShareBtn);
            ShareBtn.Click();

            IWebElement WebEmbed = driver.FindElement(By.Id("show-embed"));
            WebEmbed.Click();

            IWebElement formEmbed = driver.FindElement(By.CssSelector(".embed-video"));

            IWebElement IFrameButton = formEmbed.FindElement(By.CssSelector(".icon-code"));
            IFrameButton.Click();



            String CodeCopied = formEmbed.FindElement(By.Id("embed-video-link")).GetAttribute("value");
            String CodeToCompare;
            Boolean isSame;
            IWebElement CheckBoxForEmbed;

            for (int i = 1; i<= 4;  i++)
            {
                CheckBoxForEmbed = formEmbed.FindElement(By.XPath("//div[3]/label[" + i + " ]"));
                CheckBoxForEmbed.Click();
                CodeToCompare = formEmbed.FindElement(By.Id("embed-video-link")).GetAttribute("value");
                isSame = CodeCopied.Equals(CodeToCompare);
                isSame.Should().BeFalse(because: "The embed code was not changed");



               //Console.WriteLine(CodeCopied);
               //Console.WriteLine(CodeToCompare);
            }
            driver.FindElement(By.CssSelector(".popup__share")).FindElement(By.CssSelector(".popup-close")).Click();

        }

        [Test, Order(5)]
        public void CommentVideoTest()
        {
            //driver.ScrollToTheBottom();
            IWebElement commentsTab = driver.FindElement(By.Id("video-comments-tab"));
            driver.ScrollToElement(commentsTab);
           
            //Actions actions = new Actions(driver);
            //actions.MoveToElement(commentsTab);
            //actions.Perform();

            //ADD COMMENT
            IWebElement CommentMessage = driver.FindElement(By.Id("comment-message"));
            CommentMessage.SendKeys("dite me shi");

            IWebElement SubmitComment = driver.FindElement(By.Id("submit-comment"));
            SubmitComment.Click();
            Thread.Sleep(5000);


            //move to see added comment
            IWebElement VideoLastComment = driver.FindElements(By.CssSelector(".video-comments__item")).ToList().FirstOrDefault();
            driver.ScrollToElement(VideoLastComment);
           

            ////  LIKE/UNLIKE COMMENT
            // Thread.Sleep(5000);
            //perdoret n rast se nuk e gjen VideoLastComment IWebElement comment = driver.FindElements(By.ClassName("video-comments__item")).ToList().Last().FindElement(By.CssSelector(".comment-content"));
            IWebElement LikeComment = VideoLastComment.FindElement(By.XPath("//*[@id='react-holder']/li[1]/i"));
            IWebElement UnlikeCommnet = VideoLastComment.FindElement(By.XPath("//*[@id='react-holder']/li[2]/i"));
            LikeComment.Click();
            Thread.Sleep(3000);
            UnlikeCommnet.Click();

            ////REPLY COMMENT
            IWebElement ReplyComment = VideoLastComment.FindElement(By.XPath("//*[@id='react-holder']/li[3]"));
            ReplyComment.Click();


            IWebElement ReplyMessage = VideoLastComment.FindElement(By.CssSelector(".form-control"));
            //ReplyMessage.Click();
            ReplyMessage.SendKeys("TESTTT replyyyy");
            IWebElement SubmitReplyBtn = VideoLastComment.FindElement(By.CssSelector(".submit-reply"));
            SubmitReplyBtn.Click();
            Thread.Sleep(5000);

            
            //DELETE REPLY
            try
            {
                IWebElement ShowReplies = VideoLastComment.FindElement(By.CssSelector(".replis-show-more"));
                ShowReplies.Click();
                IWebElement LastReply = driver.FindElements(By.CssSelector(".reply-item")).ToList().FirstOrDefault();
                Thread.Sleep(5000);
                IWebElement ThreeDotsInReply = LastReply.FindElement(By.CssSelector(".manage-comment-toggle"));
                ThreeDotsInReply.Click();

                Thread.Sleep(5000);
                IWebElement DeleteReply = LastReply.FindElement(By.CssSelector(".delete"));

                Thread.Sleep(2000);
                DeleteReply.Click();


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }



            // DELETE COMMENT
            IWebElement ThreeDots = VideoLastComment.FindElement(By.CssSelector(".manage-comment-toggle"));
            ThreeDots.Click();
            IWebElement DeleteComment = VideoLastComment.FindElement(By.CssSelector(".delete"));

            Thread.Sleep(2000);
            DeleteComment.Click();



        }



        [TearDown]
        public void CleanUp()
        {
            //driver.Close();
            Console.WriteLine("Closedddddd!!");
        }
    }
}
