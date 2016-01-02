SELECT	Name AS ObjectName, 
		'ASS' AS ObjectType, 
		NULL AS SchemaName, 
		Name AS FullyQualifiedName 
FROM sys.assemblies A
UNION ALL 
SELECT	O.Name AS ObjectName, 
		O.[type] AS ObjectType, 
		S.name AS SchemaName, 
		S.name + '.' + O.name AS FullyQualifiedName
FROM sys.objects O
	WITH (NOLOCK)
INNER JOIN sys.schemas S
	WITH (NOLOCK)
ON S.schema_id = O.schema_id
WHERE type <> 'S' and type <> 'IT' 