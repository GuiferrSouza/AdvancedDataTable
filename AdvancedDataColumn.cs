using System;
using System.Collections.Generic;
using System.Data;

using Newtonsoft.Json;

[Serializable]
public class AdvancedDataColumn : DataColumn
{
    private Dictionary<string, object> _extendedProperties;

    /// <summary>
    /// Gets or sets the extended properties for the DataColumn.
    /// </summary>
    [JsonProperty]
    public new Dictionary<string, object> ExtendedProperties
    {
        get { return _extendedProperties; }
        set
        {
            _extendedProperties = value;
        }
    }

    /// <summary>
    /// Initializes a new instance of the AdvancedDataColumn class.
    /// </summary>
    /// <param name="columnName">The name of the column.</param>
    /// <param name="dataType">The data type of the column.</param>
    public AdvancedDataColumn(string columnName, Type dataType) : base(columnName, dataType)
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