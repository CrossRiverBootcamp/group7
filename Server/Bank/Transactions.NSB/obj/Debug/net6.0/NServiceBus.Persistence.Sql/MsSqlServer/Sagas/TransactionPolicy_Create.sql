
/* TableNameVariable */

declare @tableName nvarchar(max) = '[' + @schema + '].[' + @tablePrefix + N'TransactionPolicy]';
declare @tableNameWithoutSchema nvarchar(max) = @tablePrefix + N'TransactionPolicy';


/* Initialize */

/* CreateTable */

if not exists
(
    select *
    from sys.objects
    where
        object_id = object_id(@tableName) and
        type in ('U')
)
begin
declare @createTable nvarchar(max);
set @createTable = '
    create table ' + @tableName + '(
        Id uniqueidentifier not null primary key,
        Metadata nvarchar(max) not null,
        Data nvarchar(max) not null,
        PersistenceVersion varchar(23) not null,
        SagaTypeVersion varchar(23) not null,
        Concurrency int not null
    )
';
exec(@createTable);
end

/* AddProperty TransactionID */

if not exists
(
  select * from sys.columns
  where
    name = N'Correlation_TransactionID' and
    object_id = object_id(@tableName)
)
begin
  declare @createColumn_TransactionID nvarchar(max);
  set @createColumn_TransactionID = '
  alter table ' + @tableName + N'
    add Correlation_TransactionID bigint;';
  exec(@createColumn_TransactionID);
end

/* VerifyColumnType Int */

declare @dataType_TransactionID nvarchar(max);
set @dataType_TransactionID = (
  select data_type
  from INFORMATION_SCHEMA.COLUMNS
  where
    table_name = @tableNameWithoutSchema and
    table_schema = @schema and
    column_name = 'Correlation_TransactionID'
);
if (@dataType_TransactionID <> 'bigint')
  begin
    declare @error_TransactionID nvarchar(max) = N'Incorrect data type for Correlation_TransactionID. Expected bigint got ' + @dataType_TransactionID + '.';
    throw 50000, @error_TransactionID, 0
  end

/* WriteCreateIndex TransactionID */

if not exists
(
    select *
    from sys.indexes
    where
        name = N'Index_Correlation_TransactionID' and
        object_id = object_id(@tableName)
)
begin
  declare @createIndex_TransactionID nvarchar(max);
  set @createIndex_TransactionID = N'
  create unique index Index_Correlation_TransactionID
  on ' + @tableName + N'(Correlation_TransactionID)
  where Correlation_TransactionID is not null;';
  exec(@createIndex_TransactionID);
end

/* PurgeObsoleteIndex */

declare @dropIndexQuery nvarchar(max);
select @dropIndexQuery =
(
    select 'drop index ' + name + ' on ' + @tableName + ';'
    from sysindexes
    where
        Id = object_id(@tableName) and
        Name is not null and
        Name like 'Index_Correlation_%' and
        Name <> N'Index_Correlation_TransactionID'
);
exec sp_executesql @dropIndexQuery

/* PurgeObsoleteProperties */

declare @dropPropertiesQuery nvarchar(max);
select @dropPropertiesQuery =
(
    select 'alter table ' + @tableName + ' drop column ' + column_name + ';'
    from INFORMATION_SCHEMA.COLUMNS
    where
        table_name = @tableNameWithoutSchema and
        table_schema = @schema and
        column_name like 'Correlation_%' and
        column_name <> N'Correlation_TransactionID'
);
exec sp_executesql @dropPropertiesQuery

/* CompleteSagaScript */
