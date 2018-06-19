﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.Configuration.DataBindings.Common;

namespace UAOOI.Configuration.DataBindings.UnitTest
{
  [TestClass]
  public class InstanceConfigurationBaseUnitTest
  {

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void ClearConfigurationTestMethod()
    {
      IInstanceConfiguration _ic = new InstanceConfigurationBase();
      Assert.IsNotNull(_ic);
      _ic.ClearConfiguration();
    }

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void EditTestMethod()
    {
      IInstanceConfiguration _ic = new InstanceConfigurationBase();
      Assert.IsNotNull(_ic);
      _ic.Edit();
    }

  }
}
