﻿//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Configuration.Networking.Serializers;
using UAOOI.Configuration.Networking.Upgrade.Re_l1_00_16;
using NewConfigurationData = UAOOI.Configuration.Networking.Serialization.ConfigurationData;

namespace UAOOI.Configuration.Networking.Upgrade.UnitTest
{

  [TestClass]
  public class Re_l1_00_16UnitTest
  {
    [TestMethod]
    [DeploymentItem(@"TestingData\", @"TestingData\")]
    public void AfterCreationStateTest()
    {
      ConfigurationData _newInstance = new ConfigurationData();
      Assert.IsNull(_newInstance.DataSets);
      Assert.IsNull(_newInstance.MessageHandlers);
    }
    [TestMethod]
    public void ReadXmlTestMethod()
    {
      NewMethod(@"TestingData\ConfigurationDataConsumer.xml", @"NewConfigurationDataConsumer.xml");
      NewMethod(@"TestingData\ConfigurationDataProducer.xml", @"NewConfigurationDataProducer.xml");
    }

    private void NewMethod(string inFileName, string outFileName)
    {
      TraceSourceBase _trace = new TraceSourceBase();
      FileInfo _file2Covert = new FileInfo(inFileName);
      Assert.IsTrue(_file2Covert.Exists);
      ConfigurationData _oldConfiguration = XmlDataContractSerializers.Load<ConfigurationData>(_file2Covert, _trace.TraceData);
      Assert.IsNotNull(_oldConfiguration);
      NewConfigurationData _newConfiguration = Import(_oldConfiguration);
      Assert.IsNotNull(_newConfiguration);
      FileInfo _file2Save = new FileInfo(outFileName);
      XmlDataContractSerializers.Save<NewConfigurationData>(_file2Save, _newConfiguration, _trace.TraceData);
    }
    private NewConfigurationData Import(ConfigurationData _oldConfiguration)
    {
      NewConfigurationData _ret = new NewConfigurationData() { };
      return _ret;
    }
  }
}
