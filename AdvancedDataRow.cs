using System.Collections.Generic;
using System.Data;

using Newtonsoft.Json;

public class AdvancedDataRow : DataRow
{
    private Dictionary<string, object> _extendedProperties;

    /// <summary>
    /// Gets or sets the extended properties for the DataRow.
    /// </summary>
    [JsonProperty]
    public Dictionary<string, object> ExtendedProperties
    {
        get { return _extendedProperties; }
        set
        {
            _extendedProperties = value;
        }
    }

    /// <summary>
    /// Initializes a new instance of the AdvancedDataRow class.
    /// </summary>
    /// <param name="builder">The DataRowBuilder used to create the DataRow.</param>
    public AdvancedDataRow(DataRowBuilder builder) : base(builder)
    {
        _extendedProperties = new Dictionary<string, object>();
    }

    /// <summary>
    /// Sets an attribute in the extended properties.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="value">The value of the attribute.</param>
    public void SetAttribute(string name, object value)
    {
        _extendedProperties[name] = value;
    }
}