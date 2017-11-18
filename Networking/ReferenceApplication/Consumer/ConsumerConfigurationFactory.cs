﻿
using System;
using System.IO;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Configuration.Networking.Serializers;

namespace UAOOI.Networking.ReferenceApplication.Consumer
{
  /// <summary>
  /// Class ConsumerConfigurationFactory - provides implementation of the <see cref="ConfigurationFactoryBase"/> for the UA Data consumer.
  /// </summary>
  /// <remarks>In production environment it shall be replaced by reading a configuration file.</remarks>
  internal class ConsumerConfigurationFactory : ConfigurationFactoryBase
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsumerConfigurationFactory"/> class.
    /// </summary>
    public ConsumerConfigurationFactory()
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

    #region private
    private ConfigurationData LoadConfig()
    {
      FileInfo _configurationFile = new FileInfo(Properties.Settings.Default.ConsumerConfigurationFileName);
      return ConfigurationDataFactoryIO.Load<ConfigurationData>(() => XmlDataContractSerializers.Load<ConfigurationData>(_configurationFile, (x, y, z) => { }), () => RaiseEvents());
    }
    protected override void RaiseEvents()
    {
      OnAssociationConfigurationChange?.Invoke(this, EventArgs.Empty);
      OnMessageHandlerConfigurationChange?.Invoke(this, EventArgs.Empty);
    }

    #endregion

  }
}
