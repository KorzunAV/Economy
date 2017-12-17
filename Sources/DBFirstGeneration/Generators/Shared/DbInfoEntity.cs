using BLToolkit.Mapping;

namespace DBFirstGeneration.Generators.Shared
{
    public class DbInfoEntity
    {
        [MapField("table_catalog")]
        public string TableCatalog { get; set; }
        [MapField("table_schema")]
        public string TableSchema { get; set; }
        [MapField("table_name")]
        public string TableName { get; set; }
        [MapField("column_name")]
        public string ColumnName { get; set; }
        [MapField("ordinal_position")]
        public int? OrdinalPosition { get; set; }
        [MapField("column_default")]
        public string ColumnDefault { get; set; }
        [MapField("is_nullable")]
        public string IsNullable { get; set; }
        [MapField("data_type")]
        public string DataType { get; set; }
        [MapField("character_maximum_length")]
        public int? CharacterMaximumLength { get; set; }
        [MapField("character_octet_length")]
        public int? CharacterOctetLength { get; set; }
        [MapField("numeric_precision")]
        public int? NumericPrecision { get; set; }
        [MapField("numeric_precision_radix")]
        public int? NumericPrecisionRadix { get; set; }
        [MapField("numeric_scale")]
        public int? NumericScale { get; set; }
        [MapField("datetime_precision")]
        public int? DatetimePrecision { get; set; }
        [MapField("interval_type")]
        public string IntervalType { get; set; }
        [MapField("interval_precision")]
        public int? IntervalPrecision { get; set; }
        [MapField("character_set_catalog")]
        public string CharacterSetCatalog { get; set; }
        [MapField("character_set_schema")]
        public string CharacterSetSchema { get; set; }
        [MapField("character_set_name")]
        public string CharacterSetName { get; set; }
        [MapField("collation_catalog")]
        public string CollationCatalog { get; set; }
        [MapField("collation_schema")]
        public string CollationSchema { get; set; }
        [MapField("collation_name")]
        public string CollationName { get; set; }
        [MapField("domain_catalog")]
        public string DomainCatalog { get; set; }
        [MapField("domain_schema")]
        public string DomainSchema { get; set; }
        [MapField("domain_name")]
        public string DomainName { get; set; }
        [MapField("udt_catalog")]
        public string UdtCatalog { get; set; }
        [MapField("udt_schema")]
        public string UdtSchema { get; set; }
        [MapField("udt_name")]
        public string UdtName { get; set; }
        [MapField("scope_catalog")]
        public string ScopeCatalog { get; set; }
        [MapField("scope_schema")]
        public string ScopeSchema { get; set; }
        [MapField("scope_name")]
        public string ScopeName { get; set; }
        [MapField("maximum_cardinality")]
        public int? MaximumCardinality { get; set; }
        [MapField("dtd_identifier")]
        public string DtdIdentifier { get; set; }
        [MapField("is_self_referencing")]
        public string IsSelfReferencing { get; set; }
        [MapField("is_identity")]
        public string IsIdentity { get; set; }
        [MapField("identity_generation")]
        public string IdentityGeneration { get; set; }
        [MapField("identity_start")]
        public string IdentityStart { get; set; }
        [MapField("identity_increment")]
        public string IdentityIncrement { get; set; }
        [MapField("identity_maximum")]
        public string IdentityMaximum { get; set; }
        [MapField("identity_minimum")]
        public string IdentityMinimum { get; set; }
        [MapField("identity_cycle")]
        public string IdentityCycle { get; set; }
        [MapField("is_generated")]
        public string IsGenerated { get; set; }
        [MapField("generation_expression")]
        public string GenerationExpression { get; set; }
        [MapField("is_updatable")]
        public string IsUpdatable { get; set; }
        [MapField("description")]
        public string Description { get; set; }
        [MapField("IsPrimaryKey")]
        public bool? IsPrimaryKey { get; set; }
        [MapField("IsForeignKey")]
        public bool? IsForeignKey { get; set; }
        [MapField("IsUniqueKey")]
        public bool? IsUniqueKey { get; set; }

        public string RefTableName { get; set; }
        public string ColumnObjName { get; set; }
    }
}