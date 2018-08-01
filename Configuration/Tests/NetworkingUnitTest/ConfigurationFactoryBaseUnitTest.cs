﻿//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Configuration.Networking.Serialization;
using System.IO;
using UAOOI.Configuration.Networking.Serializers;

namespace UAOOI.Configuration.Networking.UnitTest
{
  [TestClass]
  [DeploymentItem(@"TestData\", @"TestData\")]
  public class ConfigurationFactoryBaseUnitTest
  {

    [TestMethod]
    [TestCategory("DataBindings_XmlSerializerTestMethod")]
    public void CreationStateTest()
    {
      TestConfigurationDataFactory _newOne = new TestConfigurationDataFactory();
      Assert.IsNotNull(_newOne.Loader);
      ConfigurationData _config = _newOne.GetConfiguration();
      Assert.IsNotNull(_config);
    }
    private class TestConfigurationDataFactory : ConfigurationFactoryBase<ConfigurationData>
    {

      /// <summary>
      /// Initializes a new instance of the <see cref="TestConfigurationDataFactory"/> class.
      /// </summary>
      public TestConfigurationDataFactory()
      {
        Loader = LoadConfig;
      }

      #region ConfigurationFactoryBase
      /// <summary>
      /// Occurs after the association configuration has been changed.
      /// </summary>
      public override event EventHandler<EventArgs> OnAssociationConfigurationChange;
      /// <summary>
      /// Occurs after the communication configuration has been changed.
      /// </summary>
      public override event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;
      #endregion

      internal new Func<ConfigurationData> Loader { get { return base.Loader; } set { base.Loader = value; } }
      #region private
      private ConfigurationData LoadConfig()
      {
        FileInfo _configurationFile = new FileInfo(_ConsumerConfigurationFileName);
        return ConfigurationDataFactoryIO.Load<ConfigurationData>(() => XmlDataContractSerializers.Load<ConfigurationData>(_configurationFile, (x, y, z) => { }), () => RaiseEvents());
      }
      protected override void RaiseEvents()
      {
        OnAssociationConfigurationChange?.Invoke(this, EventArgs.Empty);
        OnMessageHandlerConfigurationChange?.Invoke(this, EventArgs.Empty);
      }
      private String _ConsumerConfigurationFileName = @"TestData\ConfigurationDataConsumer.xml";
      #endregion

    }
  }
}
