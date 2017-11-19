﻿
using System;
using System.IO;
using UAOOI.Networking.SemanticData.Encoding;

namespace UAOOI.Networking.SemanticData.MessageHandling
{
  /// <summary>
  /// Class BinaryDecoder - wrapper of <see cref="BinaryReader"/> supporting OPC UA binary encoding.
  /// </summary>
  public abstract class BinaryDecoder : BinaryPacketDecoder
  {
    #region creators
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryPacketDecoder" /> class is to be used by the packet level decoding.
    /// </summary>
    /// <param name="uaDecoder">The UA decoder to be used fo decode UA Built-in data types.</param>
    public BinaryDecoder(IUADecoder uaDecoder) : base(uaDecoder) { }
    #endregion

    #region IDisposable Support
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (disposing)
      {
        BinaryReader _lc = m_UABinaryReader;
        if (_lc != null)
          _lc.Close();
        m_UABinaryReader = null;
      }
    }
    #endregion

    #region BinaryPacketDecoder
    /// <summary>
    /// Reads an 8-byte unsigned integer from the message and advances the position by eight bytes.
    /// </summary>
    /// <returns>An 8-byte unsigned integer <see cref="UInt64"/> read from this message. .</returns>
    public override UInt64 ReadUInt64()
    {
      return m_UABinaryReader.ReadUInt64();
    }
    public override UInt32 ReadUInt32()
    {
      return m_UABinaryReader.ReadUInt32();
    }
    public override UInt16 ReadUInt16()
    {
      return m_UABinaryReader.ReadUInt16();
    }
    public override String ReadString()
    {
      return m_UABinaryReader.ReadString();
    }
    public override Single ReadSingle()
    {
      return m_UABinaryReader.ReadSingle();
    }
    public override SByte ReadSByte()
    {
      return m_UABinaryReader.ReadSByte();
    }
    public override Int64 ReadInt64()
    {
      return m_UABinaryReader.ReadInt64();
    }
    public override Int32 ReadInt32()
    {
      return m_UABinaryReader.ReadInt32();
    }
    public override Int16 ReadInt16()
    {
      return m_UABinaryReader.ReadInt16();
    }
    public override Double ReadDouble()
    {
      return m_UABinaryReader.ReadDouble();
    }
    public override char ReadChar()
    {
      return m_UABinaryReader.ReadChar();
    }
    public override Byte ReadByte()
    {
      return m_UABinaryReader.ReadByte();
    }
    public override Boolean ReadBoolean()
    {
      return m_UABinaryReader.ReadBoolean();
    }
    public override byte[] ReadBytes(int count)
    {
      return m_UABinaryReader.ReadBytes(count);
    }
    protected override bool EndOfMessage()
    {
      return m_UABinaryReader.BaseStream.Position == m_UABinaryReader.BaseStream.Length;
    }
    #endregion

    #region private
    /// <summary>
    /// Called when new frame has arrived.
    /// </summary>
    /// <param name="uaBinaryReader">
    /// The UA binary reader an instance of <see cref="BinaryReader"/> created after new frame has been arrived.
    /// </param>
    /// <remarks> Just after processing the object is disposed.</remarks>
    protected void OnNewFrameArrived(BinaryReader uaBinaryReader)
    {
      m_UABinaryReader = uaBinaryReader;
      OnNewPacketArrived();
      m_UABinaryReader.Dispose();
      m_UABinaryReader = null; ;
    }
    private BinaryReader m_UABinaryReader;
    #endregion

  }

}
