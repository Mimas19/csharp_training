using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using static NUnit.Framework.Assert;


namespace addressbook_tests_autoit
{
    public class TestBase
    {
      ApplicationManager app;  
        
      [OneTimeSetUp]
      public void initApplication()
      {
          app = new ApplicationManager();
      }

      [OneTimeTearDown]
      public void stopApplication()
      {
          app.Stop();
      }
    }
}

