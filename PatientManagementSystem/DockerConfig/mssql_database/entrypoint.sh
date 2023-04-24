#!/bin/sh
set -m
/opt/mssql/bin/sqlservr & ./runInitialization.sh
fg