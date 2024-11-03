using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[Serializable]
public class AdvancedDataTable : DataTable
{
    public AdvancedDataTable() : base() { }
    public AdvancedDataTable(string tableName) : base(tableName) { }
    public AdvancedDataTable(string tableName, string tableNamespace) : base(tableName, tableNamespace) { }

    protected override Type GetRowType()
    {
        return typeof(AdvancedDataRow);
    }

    protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
    {
        return new AdvancedDataRow(builder);
    }

    public IEnumerable<AdvancedDataRow> AdvancedRows
    {
        get
        {
            return Rows.OfType<AdvancedDataRow>();
        }
    }

    /// <summary>
    /// Clones the current table structure and properties to the specified DataTable.
    /// </summary>
    /// <param name="dataTable">The DataTable to clone to.</param>
    /// <returns>The current instance of AdvancedDataTable.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided DataTable is null.</exception>
    public AdvancedDataTable CloneTo(DataTable dataTable)
    {
        if (dataTable == null) throw new ArgumentNullException(nameof(dataTable));

        dataTable.Clear();

        dataTable.TableName = TableName;
        dataTable.Namespace = Namespace;
        dataTable.Prefix = Prefix;
        dataTable.MinimumCapacity = MinimumCapacity;

        foreach (DataColumn column in dataTable.Columns)
        {
            DataColumn newColumn = Columns.Add(column.ColumnName, column.DataType);

            foreach (DictionaryEntry entry in column.ExtendedProperties)
            {
                newColumn.ExtendedProperties.Add(entry.Key, entry.Value);
            }
        }

        foreach (DictionaryEntry entry in ExtendedProperties)
        {
            dataTable.ExtendedProperties.Add(entry.Key, entry.Value);
        }

        return this;
    }

    /// <summary>
    /// Copies the current table structure, properties, and data to the specified DataTable.
    /// </summary>
    /// <param name="dataTable">The DataTable to copy to.</param>
    /// <returns>The current instance of AdvancedDataTable.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided DataTable is null.</exception>
    public AdvancedDataTable CopyTo(DataTable dataTable)
    {
        if (dataTable == null) throw new ArgumentNullException(nameof(dataTable));

        CloneTo(dataTable);

        foreach (DataRow row in Rows)
        {
            dataTable.ImportRow(row);
        }

        return this;
    }

    /// <summary>
    /// Clones the structure and properties from the specified DataTable to the current table.
    /// </summary>
    /// <param name="dataTable">The DataTable to clone from.</param>
    /// <returns>The current instance of AdvancedDataTable.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided DataTable is null.</exception>
    public AdvancedDataTable CloneFrom(DataTable dataTable)
    {
        if (dataTable == null) throw new ArgumentNullException(nameof(dataTable));

        Clear();

        TableName = dataTable.TableName;
        Namespace = dataTable.Namespace;
        Prefix = dataTable.Prefix;
        MinimumCapacity = dataTable.MinimumCapacity;

        foreach (DataColumn column in dataTable.Columns)
        {
            DataColumn newColumn = Columns.Add(column.ColumnName, column.DataType);

            foreach (DictionaryEntry entry in column.ExtendedProperties)
            {
                newColumn.ExtendedProperties.Add(entry.Key, entry.Value);
            }
        }

        foreach (DictionaryEntry entry in dataTable.ExtendedProperties)
        {
            ExtendedProperties.Add(entry.Key, entry.Value);
        }

        return this;
    }

    /// <summary>
    /// Copies the structure, properties, and data from the specified DataTable to the current table.
    /// </summary>
    /// <param name="dataTable">The DataTable to copy from.</param>
    /// <returns>The current instance of AdvancedDataTable.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided DataTable is null.</exception>
    public AdvancedDataTable CopyFrom(DataTable dataTable)
    {
        if (dataTable == null) throw new ArgumentNullException(nameof(dataTable));

        CloneFrom(dataTable);

        foreach (DataRow row in dataTable.Rows)
        {
            ImportRow(row);
        }

        return this;
    }

    /// <summary>
    /// Creates a new instance of AdvancedDataRow.
    /// </summary>
    /// <returns>A new instance of AdvancedDataRow.</returns>
    public AdvancedDataRow NewAdvancedRow()
    {
        return (AdvancedDataRow)NewRow();
    }

    /// <summary>
    /// Adds a new AdvancedDataColumn to the table.
    /// </summary>
    /// <param name="columnName">The name of the column.</param>
    /// <param name="dataType">The data type of the column.</param>
    /// <returns>The newly added AdvancedDataColumn.</returns>
    public AdvancedDataColumn AddAdvancedColumn(string columnName, Type dataType)
    {
        var column = new AdvancedDataColumn(columnName, dataType);
        Columns.Add(column);
        return column;
    }

    /// <summary>
    /// Imports an AdvancedDataRow into the table.
    /// </summary>
    /// <param name="row">The AdvancedDataRow to import.</param>
    /// <returns>The imported AdvancedDataRow.</returns>
    public AdvancedDataRow ImportAdvancedRow(AdvancedDataRow row) => ImportAdvancedRow(row, true);

    /// <summary>
    /// Imports an AdvancedDataRow into the table with an option to ignore different columns.
    /// </summary>
    /// <param name="row">The AdvancedDataRow to import.</param>
    /// <param name="ignoreDifColumns">If true, ignores columns that do not exist in the target table.</param>
    /// <returns>The imported AdvancedDataRow.</returns>
    public AdvancedDataRow ImportAdvancedRow(AdvancedDataRow row, bool ignoreDifColumns)
    {
        var newRow = (AdvancedDataRow)NewRow();
        foreach (DataColumn column in Columns)
        {
            if (!ignoreDifColumns || row.Table.Columns.Contains(column.ColumnName))
            {
                newRow[column.ColumnName] = row[column.ColumnName];
            }
        }

        foreach (var property in row.ExtendedProperties)
        {
            newRow.SetAttribute(property.Key, property.Value);
        }

        Rows.Add(newRow);
        return newRow;
    }

    public class JsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(AdvancedDataTable).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is AdvancedDataTable table))
            {
                writer.WriteNull();
                return;
            }

            writer.WriteStartObject();
            writer.WritePropertyName("TableName");
            writer.WriteValue(table.TableName);
            writer.WritePropertyName("Namespace");
            writer.WriteValue(table.Namespace);
            writer.WritePropertyName("Columns");
            writer.WriteStartArray();

            foreach (DataColumn column in table.Columns)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("ColumnName");
                writer.WriteValue(column.ColumnName);
                writer.WritePropertyName("DataType");
                writer.WriteValue(column.DataType.FullName);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
            writer.WritePropertyName("PrimaryKey");
            writer.WriteStartArray();

            foreach (DataColumn column in table.PrimaryKey)
            {
                writer.WriteValue(column.ColumnName);
            }

            writer.WriteEndArray();
            writer.WritePropertyName("Rows");
            writer.WriteStartArray();

            foreach (AdvancedDataRow row in table.Rows)
            {
                writer.WriteStartObject();
                foreach (DataColumn column in table.Columns)
                {
                    writer.WritePropertyName(column.ColumnName);
                    writer.WriteValue(row[column]);
                }

                writer.WritePropertyName("ExtendedProperties");
                serializer.Serialize(writer, row.ExtendedProperties);

                writer.WriteEndObject();
            }

            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            var table = new AdvancedDataTable((string)obj["TableName"]);

            foreach (var column in obj["Columns"])
            {
                table.Columns.Add((string)column["ColumnName"],
                Type.GetType((string)column["DataType"]));
            }

            var primaryKeyColumnNames = obj["PrimaryKey"].ToObject<string[]>();
            var primaryKeyColumns = new DataColumn[primaryKeyColumnNames.Length];

            for (int i = 0; i < primaryKeyColumnNames.Length; i++)
            {
                primaryKeyColumns[i] = table.Columns[primaryKeyColumnNames[i]];
            }

            table.PrimaryKey = primaryKeyColumns;

            foreach (var row in obj["Rows"])
            {
                var dataRow = table.NewRow() as AdvancedDataRow;
                foreach (DataColumn column in table.Columns)
                {
                    dataRow[column.ColumnName] = row[column.ColumnName].ToObject(column.DataType);
                }

                var extendedProperties = row["ExtendedProperties"].ToObject<Dictionary<string, object>>();
                foreach (var kvp in extendedProperties)
                {
                    dataRow.SetAttribute(kvp.Key, kvp.Value);
                }

                table.Rows.Add(dataRow);
            }
            return table;
        }
    }
}