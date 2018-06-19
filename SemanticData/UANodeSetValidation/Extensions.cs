﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Serialization;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Delegate LocalizedTextFactory - encapsulates a method that must be used to create localized text using <paramref name="localeField"/> and <paramref name="valueField"/>
  /// </summary>
  /// <param name="localeField">The locale field. This argument is specified as a <see cref="string"/>  that is composed of a language component and a country/region 
  /// component as specified by RFC 3066. The country/region component is always preceded by a hyphen. The format of the LocaleId string is shown below: 
  /// <c>
  /// &lt;language&gt;[-&lt;country/region&gt;], 
  /// where:
  ///   &lt;language&gt; is the two letter ISO 639 code for a language,
  ///   &lt;country/region&gt; is the two letter ISO 3166 code for the country/region.
  /// </c>
  /// </param>
  /// <param name="valueField">The value field.</param>
  internal delegate void LocalizedTextFactory(string localeField, string valueField);

  /// <summary>
  /// Class Extensions - provides helper functions for this namespace
  /// </summary>
  internal static class Extensions
  {
    //string
    internal static string SymbolicName(this List<string> path)
    {
      return String.Join("_", path.ToArray());
    }
    /// <summary>
    /// Exports the string and filter out the default value. 
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>Returns <paramref name="value"/> if not equal to <paramref name="defaultValue"/>, otherwise it returns <see cref="String.Empty"/>.</returns>
    internal static string ExportString(this string value, string defaultValue)
    {
      if (String.IsNullOrEmpty(value))
        return null;
      return String.Compare(value, defaultValue) == 0 ? null : value;
    }
    internal static bool? Export(this bool value, bool defaultValue)
    {
      return !value.Equals(defaultValue) ? value : new Nullable<bool>();
    }
    internal static int? Export(this double value, double defaultValue)
    {
      return !value.Equals(defaultValue) ? Convert.ToInt32(value) : new Nullable<int>();
    }
    internal static UInt32 Validate(this UInt32 value, UInt32 maxValue, Action<UInt32> reportError)
    {
      if (value.CompareTo(maxValue) >= 0)
        reportError(value);
      return value & maxValue - 1;
    }
    [SecurityPermissionAttribute(SecurityAction.Demand)]
    internal static string ValidateIdentifier(this string name, Action<TraceMessage> reportError)
    {
      if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(name))
        reportError(TraceMessage.BuildErrorTraceMessage(BuildError.WrongSymbolicName, String.Format("SymbolicName: '{0}'.", name)));
      return name;
    }
    internal static string NodeIdentifier(this UANode node)
    {
      if (String.IsNullOrEmpty(node.BrowseName))
        return node.SymbolicName;
      return node.BrowseName;
    }
    internal static string ConvertToString(this XML.LocalizedText[] localizedText)
    {
      if (localizedText == null || localizedText.Length == 0)
        return "Empty LocalizedText";
      return String.Format("{0}:{1}", localizedText[0].Locale, localizedText[0].Value);
    }
    internal static void GetParameters(this DataTypeDefinition dataTypeDefinition, IDataTypeDefinitionFactory definition, UAModelContext modelContext, Action<TraceMessage> traceEvent)
    {
      if (dataTypeDefinition == null || dataTypeDefinition.Field == null || dataTypeDefinition.Field.Length == 0)
        return;
      foreach (DataTypeField _item in dataTypeDefinition.Field)
      {
        IDataTypeFieldFactory _nP = definition.NewField();
        _nP.DataType = modelContext.ExportBrowseName(_item.DataType, DataTypes.BaseDataType, traceEvent);
        _item.Description.ExportLocalizedTextArray(_nP.AddDescription);
        _nP.Identifier = _item.Value;
        _nP.Name = _item.Name;
        _nP.ValueRank = _item.ValueRank.GetValueRank(traceEvent);
        _nP.SymbolicName = _item.SymbolicName;
        _nP.Value = _item.Value;
        if (_item.Definition == null)
          continue;
        IDataTypeDefinitionFactory _new = _nP.NewDefinition();
        _item.Definition.GetParameters(_new, modelContext, traceEvent);
      }
    }
    internal static void ExportLocalizedTextArray(this XML.LocalizedText[] text, LocalizedTextFactory createLocalizedText)
    {

      if (text == null || text.Length == 0)
        return;
      foreach (XML.LocalizedText item in text)
        createLocalizedText(item.Locale, item.Value);
    }
    internal static XML.LocalizedText[] Truncate(this XML.LocalizedText[] localizedText, int maxLength, Action<TraceMessage> reportError)
    {
      if (localizedText == null || localizedText.Length == 0)
        return null;
      List<XML.LocalizedText> _ret = new List<XML.LocalizedText>();
      foreach (XML.LocalizedText _item in localizedText)
      {
        if (_item.Value.Length > maxLength)
        {
          reportError(TraceMessage.BuildErrorTraceMessage(BuildError.WrongDisplayNameLength, String.Format
            ("The localized text starting with '{0}:{1}' of length {2} is too long.", _item.Locale, _item.Value.Substring(0, 20), _item.Value.Length)));
          XML.LocalizedText _localizedText = new XML.LocalizedText()
          {
            Locale = _item.Locale,
            Value = _item.Value.Substring(0, maxLength)
          };
        }
      }
      return localizedText;
    }
    internal static List<Argument> GetParameters(this XmlElement xmlElement)
    {
      ListOfExtensionObject _wrapper = xmlElement.GetObject<ListOfExtensionObject>();
      Debug.Assert(_wrapper != null);
      if (_wrapper.ExtensionObject.AsEnumerable<ExtensionObject>().Where<ExtensionObject>(x => !((string)x.TypeId.Identifier).Equals("i=297")).Any())
        throw new ArgumentOutOfRangeException("ExtensionObject.TypeId.Identifier");
      List<Argument> _ret = new List<Argument>();
      foreach (var item in _wrapper.ExtensionObject)
        _ret.Add(item.Body.GetObject<Argument>());
      return _ret;
    }
    /// <summary>
    /// Deserialize <see cref="XmlElement" /> object using <see cref="XmlSerializer" />
    /// </summary>
    /// <typeparam name="type">The type of the type.</typeparam>
    /// <param name="xmlElement">The object to be deserialized.</param>
    /// <returns>Deserialized object</returns>
    internal static type GetObject<type>(this XmlElement xmlElement)
    {
      using (MemoryStream _memoryBuffer = new MemoryStream(1000))
      {
        XmlWriterSettings _settings = new XmlWriterSettings() { ConformanceLevel = ConformanceLevel.Fragment };
        using (XmlWriter wrt = XmlWriter.Create(_memoryBuffer, _settings))
          xmlElement.WriteTo(wrt);
        _memoryBuffer.Flush();
        _memoryBuffer.Position = 0;
        type _Value;
        XmlSerializer _serializer = new XmlSerializer(typeof(type));
        using (XmlReader _reader = XmlReader.Create(_memoryBuffer))
          _Value = (type)_serializer.Deserialize(_reader);
        return _Value;
      }
    }

  }

}
