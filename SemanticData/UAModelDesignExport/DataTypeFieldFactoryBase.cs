﻿//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Diagnostics;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using TraceMessage = UAOOI.SemanticData.BuildingErrorsHandling.TraceMessage;

namespace UAOOI.SemanticData.UAModelDesignExport
{

  /// <summary>
  /// Class DataTypeFieldFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IDataTypeFieldFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IDataTypeFieldFactory" />
  internal class DataTypeFieldFactoryBase : IDataTypeFieldFactory
  {

    #region constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="DataTypeFieldFactoryBase"/> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    internal DataTypeFieldFactoryBase(Action<TraceMessage> traceEvent)
    {
      Debug.Assert(traceEvent != null);
      TraceEvent = traceEvent;
    }
    #endregion

    #region IDataTypeFieldFactory
    public XmlQualifiedName DataType
    {
      set;
      private get;
    }
    public string Name { set; private get; }
    public int? ValueRank { set; private get; }
    public int Value { set; private get; }
    public string SymbolicName { set; private get; }
    public void AddDescription(string localeField, string valueField)
    {
      Extensions.AddLocalizedText(localeField, valueField, ref m_Description, TraceEvent);
    }
    public void AddDisplayName(string localeField, string valueField)
    {
      Extensions.AddLocalizedText(localeField, valueField, ref m_Description, TraceEvent);
    }
    public string ArrayDimensions { set; private get; }
    public uint MaxStringLength { set; private get; }
    public bool IsOptional { set; private get; }
    #endregion

    #region internal API
    internal XML.Parameter Export()
    {
      bool _ValueRankSpecified;
      XML.Parameter _newParameter = new XML.Parameter()
      {
        DataType = DataType,
        Description = m_Description,
        Identifier = Value,
        IdentifierSpecified = true,
        Name = Name,
        ValueRank = ValueRank.GetValueRank(x => _ValueRankSpecified = x, TraceEvent),
        //TODO to be implemented according to the in UANodeSet.xsd - synchronize with current OPCF Release #207
        //ArrayDimensions, BitMask, m_DisplayNam 
        // m_DisplayNam and Description are arrays but here are 

        //_item.Definition 
        //The field is a structure with a layout specified by the definition.
        //This field is optional.
        //This field allows designers to create nested structures without defining a new DataType Node for each structure.
        //This field is not specified for subtypes of Enumeration.

        //_item.SymbolicName 
        //A symbolic name for the field that can be used in autogenerated code. It should only be specified if the Name cannot be used for this purpose. 
        //Only letters, digits or the underscore (‘_’) are permitted.

        //_item.Value
        //The value associated with the field.
        //This field is only specified for subtypes of Enumeration.

      };
      return _newParameter;
    }
    #endregion

    #region private
    private Action<TraceMessage> TraceEvent { get; set; }
    private XML.LocalizedText m_Description = null;
    private readonly XML.LocalizedText m_DisplayNam = null;
    #endregion

  }

}
