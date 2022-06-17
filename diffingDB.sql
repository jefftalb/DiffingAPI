/********************************************************
* This script creates the database named diffingDB 
*********************************************************/
USE [master]
GO
DROP DATABASE IF EXISTS diffingDB;
CREATE DATABASE diffingDB;
GO
USE diffingDB;

CREATE TABLE diff (
  id        INT            PRIMARY KEY,
  left_data      VARCHAR(MAX)   NOT NULL,
  right_data      VARCHAR(MAX)   NOT NULL
);
