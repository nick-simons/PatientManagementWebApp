#!/usr/bin/env bash
sleep 20s
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P L0c@lP@ssw0rd -i setup.sql