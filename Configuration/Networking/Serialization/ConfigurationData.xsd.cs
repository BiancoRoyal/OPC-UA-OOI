﻿
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: ContractNamespaceAttribute("http://commsvr.com/UAOOI/SemanticData/UANetworking/Configuration/Serialization.xsd", ClrNamespace = "UAOOI.Configuration.Networking.Serialization")]

namespace UAOOI.Configuration.Networking.Serialization
{

  [DataContractAttribute(Name = "ConfigurationData", Namespace = CommonDefinitions.Namespace)]
  [SerializableAttribute()]
  [XmlRoot(Namespace = CommonDefinitions.Namespace)]
  //[XmlType(Namespace = CommonDefinitions.Namespace)]
  public partial class ConfigurationData : object, IExtensibleDataObject
  {

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2235:MarkAllNonSerializableFields")]
    private ExtensionDataObject extensionDataField;
    private DataSetConfiguration[] DataSetsField;
    private MessageHandlerConfiguration[] MessageHandlersField;
    private TypeDictionary[] TypeDictionariesField;

    public ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }

    [DataMemberAttribute(EmitDefaultValue = true, IsRequired = true)]
    [XmlElementAttribute(IsNullable = false)]
    public DataSetConfiguration[] DataSets
    {
      get
      {
        return this.DataSetsField;
      }
      set
      {
        this.DataSetsField = value;
      }
    }

    [DataMemberAttribute(EmitDefaultValue = true, IsRequired = true)]
    [XmlArray(IsNullable = false)]
    [XmlArrayItem(Type = typeof(MessageWriterConfiguration), ElementName = "MessageWriterConfiguration")]
    [XmlArrayItem(Type = typeof(MessageReaderConfiguration), ElementName = "MessageReaderConfiguration")]
    public MessageHandlerConfiguration[] MessageHandlers
    {
      get
      {
        return this.MessageHandlersField;
      }
      set
      {
        this.MessageHandlersField = value;
      }
    }

    [DataMemberAttribute(EmitDefaultValue = true, IsRequired = false)]
    [XmlArray(IsNullable = true)]
    public TypeDictionary[] TypeDictionaries
    {
      get { return TypeDictionariesField; }
      set { TypeDictionariesField = value; }
    }

  }

  [DataContractAttribute(Name = "DataSetConfiguration", Namespace = CommonDefinitions.Namespace)]
  [SerializableAttribute()]
  public partial class DataSetConfiguration : object, IExtensibleDataObject
  {

    #region private
    [NonSerializedAttribute()]
    private ExtensionDataObject extensionDataField;
    private AssociationRole AssociationRoleField;
    [OptionalFieldAttribute()]
    private string AssociationNameField;
    [OptionalFieldAttribute()]
    private string RepositoryGroupField;
    [OptionalFieldAttribute()]
    private string InformationModelURIField;
    [OptionalFieldAttribute()]
    private string DataSymbolicNameField;
    [OptionalFieldAttribute()]
    private FieldMetaData[] DataSetField;
    [OptionalFieldAttribute()]
    private string GuidField;
    [OptionalFieldAttribute()]
    private NodeDescriptor RootField;
    [OptionalFieldAttribute()]
    private double PublishingIntervalField;
    [OptionalFieldAttribute()]
    private double MaxBufferTimeField;
    [OptionalFieldAttribute()]
    private Guid ConfigurationGuidField;
    [OptionalFieldAttribute()]
    private ConfigurationVersionDataType ConfigurationVersionField;
    #endregion

    #region public
    public ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }
    [DataMemberAttribute(IsRequired = true)]
    public AssociationRole AssociationRole
    {
      get
      {
        return this.AssociationRoleField;
      }
      set
      {
        this.AssociationRoleField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 1, IsRequired = true)]
    public string AssociationName
    {
      get
      {
        return this.AssociationNameField;
      }
      set
      {
        this.AssociationNameField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 2)]
    public string RepositoryGroup
    {
      get
      {
        return this.RepositoryGroupField;
      }
      set
      {
        this.RepositoryGroupField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 3)]
    public string InformationModelURI
    {
      get
      {
        return this.InformationModelURIField;
      }
      set
      {
        this.InformationModelURIField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 4)]
    public string DataSymbolicName
    {
      get
      {
        return this.DataSymbolicNameField;
      }
      set
      {
        this.DataSymbolicNameField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 5, IsRequired = true)]
    public FieldMetaData[] DataSet
    {
      get
      {
        return this.DataSetField;
      }
      set
      {
        this.DataSetField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 6)]
    public string Guid
    {
      get
      {
        return this.GuidField;
      }
      set
      {
        this.GuidField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 7, IsRequired = true)]
    [XmlElementAttribute(IsNullable = false)]
    public NodeDescriptor Root
    {
      get { return RootField; }
      set { RootField = value; }
    }
    /// <summary>
    /// Gets or sets the publishing interval - The interval in milliseconds for sampling the Variables and publishing the Values in a DataSet by the related MessageWriter. 
    /// The Duration DataType is a subtype of Double and allows configuration of intervals smaller than a millisecond.
    /// </summary>
    /// <value>The publishing interval.</value>
    [DataMemberAttribute(EmitDefaultValue = true, Order = 8, IsRequired = true)]
    [XmlElementAttribute(IsNullable = false)]
    public double PublishingInterval
    {
      get { return PublishingIntervalField; }
      set { PublishingIntervalField = value; }
    }
    /// <summary>
    /// Gets or sets the maximum buffer time. The MaxBufferTime defines the maximum time the delivery of the DataSet may be delayed by the 
    /// MessageWriter, to allow for the collection of additional Messages. This parameter allows the Producer to reduce the number of network packets necessary to send the Messages.
    /// </summary>
    /// <value>The maximum buffer time.</value>
    [DataMemberAttribute(EmitDefaultValue = true, Order = 9, IsRequired = true)]
    [XmlElementAttribute(IsNullable = false)]
    public double MaxBufferTime
    {
      get { return MaxBufferTimeField; }
      set { MaxBufferTimeField = value; }
    }
    /// <summary>
    /// Gets or sets the configuration unique identifier. It provides a unique identifier for the current configuration of this object. 
    /// Any change of the ConfigurationVersion Property triggers a creation of a new value.
    /// </summary>
    /// <value>The configuration unique identifier.</value>
    [DataMemberAttribute(EmitDefaultValue = true, Order = 10, IsRequired = true)]
    [XmlElementAttribute(IsNullable = false)]
    public Guid ConfigurationGuid
    {
      get { return ConfigurationGuidField; }
      set { ConfigurationGuidField = value; }
    }
    [DataMemberAttribute(EmitDefaultValue = true, Order = 11, IsRequired = true)]
    [XmlElementAttribute(IsNullable = false)]
    public ConfigurationVersionDataType ConfigurationVersion
    {
      get { return ConfigurationVersionField; }
      set { ConfigurationVersionField = value; }
    }
    #endregion

  }
  [Serializable]
  [DataContractAttribute(Name = "ConfigurationVersionDataType", Namespace = CommonDefinitions.Namespace)]
  public partial class ConfigurationVersionDataType
  {
    private byte MajorVersionField;
    private byte MinorVersionField;

    /// <summary>
    /// Gets or sets the major version. The major number reflects the primary format of the DataSet and must be equal in both Producer and Consumer.
    /// Removing fields from the DataSet, reordering fields, adding fields in between other fields or a DataType change in fields shall result in an update of the MajorVersion. 
    /// The initial value for the MajorVersion is 0. If the MajorVersion is incremented, the MinorVersion shall be set to 0.
    /// An overflow of the MajorVersion is treated like any other major version change and requires a meta data exchange.
    /// </summary>
    /// <value>The major version.</value>
    [DataMemberAttribute(EmitDefaultValue = true, IsRequired = true)]
    public byte MajorVersion
    {
      get { return MajorVersionField; }
      set { MajorVersionField = value; }
    }
    /// <summary>
    /// Gets or sets the minor version. The minor number reflects backward compatible changes of the DataSet like adding a field at the end of the DataSet.
    /// The initial value for the MinorVersion is 0. The MajorVersion shall be incremented after an overflow of the MinorVersion.
    /// </summary>
    /// <value>The minor version.</value>
    [DataMemberAttribute(EmitDefaultValue = true, IsRequired = true)]
    public byte MinorVersion
    {
      get { return MinorVersionField; }
      set { MinorVersionField = value; }
    }
  }
  [DataContractAttribute(Name = "AssociationConfiguration", Namespace = CommonDefinitions.Namespace)]
  [KnownType(typeof(ProducerAssociationConfiguration))]
  [KnownType(typeof(ConsumerAssociationConfiguration))]
  [SerializableAttribute()]
  public partial class AssociationConfiguration
  {

    private string AssociationNameField;
    private UInt16 DataSetWriterIdField;
    private Guid PublisherIdField;

    [DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
    public string AssociationName
    {
      get { return AssociationNameField; }
      set { AssociationNameField = value; }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 1)]
    public UInt16 DataSetWriterId
    {
      get { return DataSetWriterIdField; }
      set { DataSetWriterIdField = value; }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 2)]
    public Guid PublisherId
    {
      get { return PublisherIdField; }
      set { PublisherIdField = value; }
    }

  }
  [DataContractAttribute(Name = "ProducerAssociationConfiguration", Namespace = CommonDefinitions.Namespace)]
  [SerializableAttribute()]
  public partial class ProducerAssociationConfiguration : AssociationConfiguration
  {
    FieldEncodingEnum FieldEncodingField;
    [DataMemberAttribute(EmitDefaultValue = true, IsRequired = true, Order = 1)]
    public FieldEncodingEnum FieldEncoding
    {
      get { return FieldEncodingField; }
      set { FieldEncodingField = value; }
    }
  }
  [DataContractAttribute(Name = "ConsumerAssociationConfiguration", Namespace = CommonDefinitions.Namespace)]
  [SerializableAttribute()]
  public partial class ConsumerAssociationConfiguration : AssociationConfiguration
  { }
  [DataContractAttribute(Name = "MessageWriterConfiguration", Namespace = CommonDefinitions.Namespace)]
  [SerializableAttribute()]
  public partial class MessageWriterConfiguration : MessageHandlerConfiguration
  {
    private ProducerAssociationConfiguration[] ProducerAssociationConfigurationField;

    [DataMemberAttribute(EmitDefaultValue = false)]
    [XmlArray(ElementName = "ProducerAssociationConfigurations")]
    public ProducerAssociationConfiguration[] ProducerAssociationConfigurations
    {
      get { return ProducerAssociationConfigurationField; }
      set { ProducerAssociationConfigurationField = value; }
    }

  }
  [DataContractAttribute(Name = "MessageReaderConfiguration")]
  [SerializableAttribute()]
  public partial class MessageReaderConfiguration : MessageHandlerConfiguration
  {

    private ConsumerAssociationConfiguration[] ConsumerAssociationConfigurationsFields;

    [DataMemberAttribute(EmitDefaultValue = false)]
    [XmlArray(ElementName = "ConsumerAssociationConfigurations")]
    public ConsumerAssociationConfiguration[] ConsumerAssociationConfigurations
    {
      get { return ConsumerAssociationConfigurationsFields; }
      set { ConsumerAssociationConfigurationsFields = value; }
    }

  }
  //[DataContractAttribute(Name = "MessageHandlerConfiguration", Namespace = CommonDefinitions.Namespace)]
  [DataContractAttribute()]
  [KnownType(typeof(MessageReaderConfiguration))]
  [KnownType(typeof(MessageWriterConfiguration))]
  [SerializableAttribute()]
  public partial class MessageHandlerConfiguration : object, IExtensibleDataObject
  {

    [NonSerializedAttribute()]
    private ExtensionDataObject extensionDataField;
    private string NameField;
    private MessageChannelConfiguration ConfigurationField;
    private AssociationRole TransportRoleField;

    public ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false)]
    public string Name
    {
      get
      {
        return this.NameField;
      }
      set
      {
        this.NameField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = true, IsRequired = true, Order = 2)]
    public MessageChannelConfiguration Configuration
    {
      get
      {
        return this.ConfigurationField;
      }
      set
      {
        this.ConfigurationField = value;
      }
    }
    [DataMemberAttribute(IsRequired = true, Order = 3)]
    public AssociationRole TransportRole
    {
      get
      {
        return this.TransportRoleField;
      }
      set
      {
        this.TransportRoleField = value;
      }
    }

  }
  [DataContractAttribute(Name = "MessageChannelConfiguration", Namespace = CommonDefinitions.Namespace)]
  [Serializable]
  public class MessageChannelConfiguration
  {
    [DataMemberAttribute(IsRequired = false, Order = 0)]
    public string ChannelConfiguration { get; set; }
  }
  [DataContractAttribute(Name = "AssociationRole", Namespace = CommonDefinitions.Namespace)]
  public enum AssociationRole : int
  {

    [EnumMemberAttribute()]
    Consumer = 0,

    [EnumMemberAttribute()]
    Producer = 1,
  }

  [DataContractAttribute(Name = "DataMemberConfiguration", Namespace = CommonDefinitions.Namespace)]
  [SerializableAttribute()]
  public partial class FieldMetaData : object, IExtensibleDataObject
  {

    #region private
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2235:MarkAllNonSerializableFields")]
    private ExtensionDataObject extensionDataField;
    private string SymbolicNameField;
    private string ProcessValueNameField;
    private UATypeInfo m_TypeInfo;
    #endregion

    #region public
    public ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }
    /// <summary>
    /// Gets or sets the name of the field.
    /// </summary>
    /// <value>The name of the field.</value>
    [DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
    public string SymbolicName
    {
      get
      {
        return this.SymbolicNameField;
      }
      set
      {
        this.SymbolicNameField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, Order = 1)]
    public string ProcessValueName
    {
      get
      {
        return this.ProcessValueNameField;
      }
      set
      {
        this.ProcessValueNameField = value;
      }
    }
    [DataMemberAttribute(EmitDefaultValue = false, IsRequired = true, Order = 2)]
    public UATypeInfo TypeInformation
    {
      get { return m_TypeInfo; }
      set { m_TypeInfo = value; }
    }
    #endregion

  }
}
