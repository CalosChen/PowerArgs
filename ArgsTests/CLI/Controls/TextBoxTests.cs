﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerArgs.Cli;
using PowerArgs;
using System.Threading;

namespace ArgsTests.CLI.Controls
{
    [TestClass]
    public class TextBoxTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Basic()
        {
            var testCli = new CliUnitTestConsole(80, 1);
            ConsoleProvider.Current = testCli;
            var app = new ConsoleApp(0, 0, 80, 1);
            app.LayoutRoot.Add(new TextBox()).Fill();
            var task = app.Start();

            testCli.Input.Enqueue(new ConsoleKeyInfo('a', ConsoleKey.A, false, false, false));


            string result = null;

            app.Stopping.SubscribeForLifetime(() =>
            {
                result = testCli.Buffer.ToString();
            }, app);

            testCli.Input.Enqueue(new ConsoleKeyInfo('*', ConsoleKey.Escape, false, false, false));
            task.Wait();


            Assert.AreEqual(80, result.Length);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void TestRenderTextBox()
        {
            var app = new CliTestHarness(this.TestContext, 8, 1);

            app.QueueAction(() =>
            {
                app.LayoutRoot.Add(new TextBox() { Value = "SomeText".ToWhite() }).Fill();
                app.Stop();
            });

            app.Start().Wait();
            Assert.IsTrue(app.Find("SomeText".ToWhite()).HasValue);

            app.AssertThisTestMatchesLKG();
        }

        [TestMethod]
        public void TestTextBoxBlinkState()
        {
            var app = new CliTestHarness(this.TestContext, 9, 1);
            app.LayoutRoot.Add(new TextBox() { Value = "SomeText".ToWhite() }).Fill();
            app.SetTimeout(app.Stop, TimeSpan.FromSeconds(1.2));
            app.Start().Wait();
            app.AssertThisTestMatchesLKG();
        }
    }
}
