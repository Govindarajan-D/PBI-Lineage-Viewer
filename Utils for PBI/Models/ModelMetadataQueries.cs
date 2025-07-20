using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils_for_PBI.Models
{
    /// <summary>
    /// ModelMetadataQueries contains queries for retrieving metadata from Power BI models.
    /// </summary>
    public static class ModelMetadataQueries
    {
        // Query for retrieving the CalcDependency Results
        public const string DependencyQuery = @"
                                            SELECT 
                                                OBJECT_TYPE, 
                                                [TABLE] AS SOURCE_TABLE, 
                                                OBJECT, 
                                                EXPRESSION, 
                                                REFERENCED_OBJECT_TYPE, 
                                                REFERENCED_TABLE, 
                                                REFERENCED_OBJECT 
                                            FROM $SYSTEM.DISCOVER_CALC_DEPENDENCY";

        // Query for retrieving the Measure Metadata Results
        public const string MeasureMetadataQuery = @"
                                                SELECT 
                                                    [Name], 
                                                    [Expression], 
                                                    FormatString, 
                                                    IsHidden, 
                                                    IsSimpleMeasure, 
                                                    DisplayFolder, 
                                                    ModifiedTime 
                                                FROM $SYSTEM.TMSCHEMA_MEASURES";

        // Query for retrieving the Table Metadata Results
        public const string TableMetadataQuery = @"
                                                SELECT 
                                                [ID],
                                                ModelID, 
                                                [Name], 
                                                [Description],
                                                DataCategory, 
                                                IsHidden, 
                                                ModifiedTime, 
                                                StructureModifiedTime, 
                                                SystemFlags, 
                                                CalculationGroupID, 
                                                ExcludeFromModelRefresh 
                                            FROM $SYSTEM.TMSCHEMA_TABLES";

        // Query for retrieving the Column Metadata Results
        public const string ColumnMetadataQuery = @"
                                               SELECT 
                                                    [ID], 
                                                    TableID, 
                                                    ExplicitName, 
                                                    InferredName, 
                                                    ExplicitDataType, 
                                                    InferredDataType,  
                                                    [Description], 
                                                    IsHidden, 
                                                    IsUnique, 
                                                    IsKey, 
                                                    IsNullable, 
                                                    SummarizeBy, 
                                                    [Type], 
                                                    Expression, 
                                                    IsAvailableInMDX, 
                                                    SortByColumnID, 
                                                    ModifiedTime, 
                                                    StructureModifiedTime, 
                                                    RefreshedTime, 
                                                    SystemFlags
                                                FROM $SYSTEM.TMSCHEMA_COLUMNS";
    }
}
