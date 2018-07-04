﻿
using System;
using System.Linq;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.MessageHandling;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData.Common;

namespace UAOOI.Networking.SemanticData
{

  /// <summary>
  /// Class ConsumerAssociation - implements the association for the consumer side.
  /// </summary>
  internal class ConsumerAssociation : Association
  {

    #region constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsumerAssociation" /> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="dataSet">The members.</param>
    /// <param name="bindingFactory">The binding factory.</param>
    /// <param name="encodingFactory">The encoding factory.</param>
    internal ConsumerAssociation(ISemanticData data, DataSetConfiguration dataSet, IBindingFactory bindingFactory, IEncodingFactory encodingFactory) :
      base(data, dataSet.AssociationName)
    {
      m_DataSetBindings = dataSet.DataSet.Select<FieldMetaData, IConsumerBinding>(x => x.GetConsumerBinding4DataMember(dataSet.RepositoryGroup, bindingFactory, encodingFactory)).ToArray<IConsumerBinding>();
    }
    #endregion

    #region API
    internal void AddMessageReader(IMessageReader messageReader)
    {
      if (messageReader == null)
        throw new ArgumentNullException("messageReader");
      messageReader.ReadMessageCompleted += MessageHandler;
    }
    internal void RemoveMessageReader(IMessageReader messageReader)
    {
      if (messageReader == null)
        throw new ArgumentNullException("messageReader");
      messageReader.ReadMessageCompleted -= MessageHandler;
    }
    #endregion

    #region Association
    protected override void InitializeCommunication()
    {
      //Do nothing;
    }
    protected internal override void AddMessageHandler(IMessageHandler messageHandler, AssociationConfiguration configuration)
    {
      base.AddMessageHandler(messageHandler, configuration);
      AddMessageReader(messageHandler as IMessageReader);
    }
    protected override void OnEnabling()
    {
      foreach (IBinding _va in m_DataSetBindings)
        _va.OnEnabling();
    }
    protected override void OnDisabling()
    {
      foreach (IBinding _va in m_DataSetBindings)
        _va.OnDisabling();
    }
    #endregion

    #region private
    private IConsumerBinding[] m_DataSetBindings = null;
    private void MessageHandler(object sender, MessageEventArg messageArg)
    {
      if (this.State.State != HandlerState.Operational)
        return;
      if ((messageArg.DataSetId) != DataSetId.DataSetWriterId || (messageArg.ProducerId != DataSetId.PublisherId))
        return;
      messageArg.MessageContent.UpdateMyValues(x => m_DataSetBindings[x], m_DataSetBindings.Length);
    }
    #endregion

  }

}
