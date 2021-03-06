﻿

REM ***********************************************************************************************************************************************
REM *********** ON TRACK DATABASE TOOLING 4 VSTO
REM *********** 
REM *********** Commons Static and other Information
REM ***********
REM *********** Version: X.YY
REM *********** Created: 2013-08-08
REM *********** Last Change:
REM ***********
REM *********** Change Log:
REM ***********
REM *********** (C) by Boris Schneider 2013
REM ***********************************************************************************************************************************************

Option Explicit On

Imports OnTrack.Database

Public Module otAddinCommon
    <ormChangeLogEntry(Application:=ConstApplicationAddinCommon, module:="", version:=1, release:=1, patch:=2, changeimplno:=4, _
       description:="Rework adding data to the DBExplorer Grids")> _
    <ormChangeLogEntry(Application:=ConstApplicationAddinCommon, module:="", version:=1, release:=1, patch:=2, changeimplno:=3, _
       description:="Password Editor for encrypted entry properties")> _
   <ormChangeLogEntry(Application:=ConstApplicationAddinCommon, module:="", version:=1, release:=1, patch:=2, changeimplno:=2, _
       description:="Reworked the validation on the UIControlDataGridView. Reworked the Column Size Setting")> _
   <ormChangeLogEntry(Application:=ConstApplicationAddinCommon, module:="", version:=1, release:=1, patch:=2, changeimplno:=1, _
       description:="Added the View on Changes in About Box")> _
    Public Const OTAddinCommonsVersion = "V1.R1.P1"
    Public Const ConstApplicationAddinCommon = "AddinCommon"
End Module
