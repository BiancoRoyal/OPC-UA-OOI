﻿//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.IO;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData.Encoding;

namespace UAOOI.Networking.SemanticData.MessageHandling
{
  /// <summary>
  /// Class BinaryMessageEncoder - provides message content binary encoding functionality.
  /// </summary>
  /// <remarks>
  /// <note>
  /// Implements only simple value types. Structural types must be implemented after more details will
  /// be available in the spec.
  /// </note>
  /// </remarks>
  public abstract class BinaryMessageEncoder : MessageWriterBase, IBinaryHeaderEncoder
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryMessageEncoder" /> class.
    /// </summary>
    /// <param name="uaEncoder">The UA encoder.</param>
    /// <param name="lengthFieldType">Type of the length field.</param>
    public BinaryMessageEncoder(IUAEncoder uaEncoder, MessageLengthFieldTypeEnum lengthFieldType) : base(uaEncoder)
    {
      m_lengthFieldType = lengthFieldType;
    }

    #region IBinaryHeaderWriter

    /// <summary>
    /// If implemented by the derived class sets the position within the wrapped stream.
    /// </summary>
    /// <param name="offset">
    /// A byte offset relative to origin.
    /// </param>
    /// <param name="origin">
    /// A field of <see cref="System.IO.SeekOrigin"/> indicating the reference point from which the new position is to be obtained..
    /// </param>
    /// <returns>The position with the current stream as <see cref="System.Int64"/>.</returns>
    public abstract long Seek(int offset, SeekOrigin origin);

    #endregion IBinaryHeaderWriter

    #region Header

    /// <summary>
    /// Gets or sets the message header.
    /// </summary>
    /// <value>The message header.</value>
    internal MessageHeader MessageHeader { get; set; }

    #endregion Header

    #region MessageWriterBase

    /// <summary>
    /// Creates the message.
    /// </summary>
    /// <param name="encoding">The selected encoding for the message.</param>
    /// <param name="prodicerId">The producer identifier.</param>
    /// <param name="dataSetWriterId">The data set writer identifier.</param>
    /// <param name="fieldCount">The field count.</param>
    /// <param name="sequenceNumber">The sequence number.</param>
    /// <param name="timeStamp">The time stamp.</param>
    /// <param name="configurationVersion">The configuration version.</param>
    protected internal override void CreateMessage
      (FieldEncodingEnum encoding, Guid prodicerId, ushort dataSetWriterId, ushort fieldCount, ushort sequenceNumber, DateTime timeStamp, ConfigurationVersionDataType configurationVersion)
    {
      OnMessageAdding(prodicerId, dataSetWriterId);
      MessageHeader = MessageHeader.GetProducerMessageHeader(this, encoding, m_lengthFieldType, MessageTypeEnum.DataKeyFrame, configurationVersion);
      //Create message header and placeholder for further header content.
      MessageHeader.FieldCount = fieldCount;
      MessageHeader.MessageSequenceNumber = sequenceNumber;
      MessageHeader.TimeStamp = timeStamp;
    }

    /// <summary>
    /// Sends the message - evaluates condition if send the package.
    /// </summary>
    /// <remarks>
    /// In current implementation one message per package is sent.
    /// </remarks>
    protected override void SendMessage()
    {
      MessageHeader.Synchronize();
      OnMessageAdded();
      //TODO sign and encrypt the message.
    }

    #endregion MessageWriterBase

    #region private

    private readonly MessageLengthFieldTypeEnum m_lengthFieldType;

    /// <summary>
    /// Called when new message is adding to the package payload.
    /// </summary>
    /// <param name="producerId">The producer identifier.</param>
    /// <param name="dataSetWriterId">The data set writer identifier - must be unique in context of <paramref name="producerId"/>.</param>
    protected abstract void OnMessageAdding(Guid producerId, ushort dataSetWriterId);

    /// <summary>
    /// Called when the current message has been added and is ready to be sent out.
    /// </summary>
    protected abstract void OnMessageAdded();

    #endregion private

  }
}