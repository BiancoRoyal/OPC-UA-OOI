﻿
namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Enum Focus
  /// </summary>
  public enum Focus
  {

    /// <summary>
    /// The reference
    /// </summary>
    Reference,
    /// <summary>
    /// The diagnostic
    /// </summary>
    Diagnostic,
    /// <summary>
    /// The NodeClass
    /// </summary>
    NodeClass,
    /// <summary>
    /// The XML error 
    /// </summary>
    XML,
    /// <summary>
    /// The non categorized error, e.g. exception during execution.
    /// </summary>
    NonCategorized,
    /// <summary>
    /// The data encoding errors - the syntax is validated against OPC UA XML encoding.
    /// </summary>
    DataEncoding,
    /// <summary>
    /// The data type definition error - eny error that relates to custom DataType definion.
    /// </summary>
    DataType

  }
  /// <summary>
  /// Class BuildError - provides building descriptions of building errors. 
  /// </summary>
  public partial class BuildError
  {

    /// <summary>
    /// Gets the focus.
    /// </summary>
    /// <value>The focus.</value>
    public Focus Focus { get; set; }
    /// <summary>
    /// Gets or sets the unique identifier of the error.
    /// </summary>
    /// <value>The identifier.</value>
    public string Identifier { get; set; }
    /// <summary>
    /// Gets or sets the descriptor of the error.
    /// </summary>
    /// <value>The descriptor.</value>
    public string Descriptor { get; set; }
    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
      return string.Format("Focus:{0}, ErrorID: {1} Info: {2}", Focus, Identifier, Descriptor);
    }

  }

}
