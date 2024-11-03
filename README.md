# AdvancedDataTable

The `AdvancedDataTable` library provides custom classes for advanced manipulation of `DataTable`, `DataRow`, and `DataColumn` in .NET. These classes allow for the addition of extended properties and custom methods to facilitate complex operations with tabular data.

## Classes

### AdvancedDataTable

The `AdvancedDataTable` class inherits from `DataTable` and adds methods for cloning and copying tables, as well as methods for handling advanced columns and rows.

#### Methods

- **CloneTo(DataTable dataTable)**: Clones the current table structure and properties to the specified `DataTable`.
- **CopyTo(DataTable dataTable)**: Copies the current table structure, properties, and data to the specified `DataTable`.
- **CloneFrom(DataTable dataTable)**: Clones the structure and properties from the specified `DataTable` to the current table.
- **CopyFrom(DataTable dataTable)**: Copies the structure, properties, and data from the specified `DataTable` to the current table.
- **NewAdvancedRow()**: Creates a new instance of `AdvancedDataRow`.
- **AddAdvancedColumn(string columnName, Type dataType)**: Adds a new column of type `AdvancedDataColumn` to the table.
- **ImportAdvancedRow(AdvancedDataRow row)**: Imports a row of type `AdvancedDataRow` into the table.
- **ImportAdvancedRow(AdvancedDataRow row, bool ignoreDifColumns)**: Imports a row of type `AdvancedDataRow` into the table, with an option to ignore different columns.

### AdvancedDataRow

The `AdvancedDataRow` class inherits from `DataRow` and adds a dictionary of extended properties.

#### Properties

- **ExtendedProperties**: Gets or sets the extended properties for the row.

#### Methods

- **SetAttribute(string name, object value)**: Sets an attribute in the extended properties.

### AdvancedDataColumn

The `AdvancedDataColumn` class inherits from `DataColumn` and adds a dictionary of extended properties.

#### Properties

- **ExtendedProperties**: Gets or sets the extended properties for the column.

#### Methods

- **SetAttribute(string name, object value)**: Sets an attribute in the extended properties.

## Usage Example

```csharp
using AdvancedDataTable;
using System;
using System.Data;

class Program
{
    static void Main()
    {
        // Create an advanced table
        var advancedTable = new AdvancedDataTable();
        advancedTable.TableName = "ExampleTable";

        // Add an advanced column
        var column = advancedTable.AddAdvancedColumn("ExampleColumn", typeof(string));

        // Create an advanced row
        var row = advancedTable.NewAdvancedRow();
        row["ExampleColumn"] = "ExampleData";
        advancedTable.Rows.Add(row);

        // Clone the table
        var clonedTable = new DataTable();
        advancedTable.CloneTo(clonedTable);

        Console.WriteLine("Table cloned successfully!");
    }
}
```

## License

This project is licensed under the terms of the MIT license. See the LICENSE file for more details.