﻿-- <auto-generated>
--   This code was generated by a tool.
--   Changes to this file may cause incorrect behaviour and will be lost if the code is regenerated.
-- </auto-generated>


/*
 * TABLE: dbo.NUMERATOR
 *
 * */

-- USE <database-name>
-- GO

-- DROP TABLE [dbo].[NUMERATOR]
-- GO

CREATE TABLE [dbo].[NUMERATOR] (
      [ID] VARCHAR(50) NOT NULL
    , [LAST_VALUE] BIGINT NOT NULL
    , CONSTRAINT [PK_NUMERATOR] PRIMARY KEY NONCLUSTERED ( [ID] )
)
GO

-- Tpl: DbSql-CreateTable-1.0.tt
-- Src: TableNumerator.tbl
