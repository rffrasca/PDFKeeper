'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2023 Robert F. Frasca
'*
'* This file is part of PDFKeeper.
'*
'* PDFKeeper is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* PDFKeeper is distributed in the hope that it will be useful,
'* but WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
'******************************************************************************
Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Security
Imports PDFKeeper.Common

Public NotInheritable Class DbSession
    Implements INotifyPropertyChanged
    Private Shared _Platform As String
    Private Shared _UserName As String
    Private Shared _Password As SecureString
    Private Shared _DataSource As String
    Private Shared odpResolverEnabled As Boolean

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    ''' <summary>
    ''' Database platform.
    ''' </summary>
    Public Enum DbPlatform
        Sqlite  ' = 0
        Oracle  ' = 1
    End Enum

    ''' <summary>
    ''' Gets or Sets the database platform.
    ''' </summary>
    ''' <returns>DbPlatform type</returns>
    Public Shared Property Platform As DbPlatform
        Get
            Return _Platform
        End Get
        Set(value As DbPlatform)
            _Platform = value
            NotifyPropertyChanged()
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the database user name.
    ''' </summary>
    ''' <returns>Database user name or Windows user name when the database platform is SQLite.</returns>
    Public Shared Property UserName As String
        Get
            If Platform = DbPlatform.Sqlite Then
                Return Environment.UserName
            Else
                Return _UserName
            End If
        End Get
        Set(value As String)
            _UserName = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the database password.
    ''' </summary>
    ''' <returns>Password secure string or nothing when the database platform is SQLite.</returns>
    Public Shared Property Password As SecureString
        Get
            If Platform = DbPlatform.Sqlite Then
                Return Nothing
            Else
                Return _Password
            End If
        End Get
        Set(value As SecureString)
            If value Is Nothing Then
                Throw New ArgumentNullException(NameOf(value))
            End If
            _Password = New SecureString
            _Password = value
            _Password.MakeReadOnly()
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the database data source.
    ''' </summary>
    ''' <returns>Database data source or nothing when the database Platform is SQLite.</returns>
    Public Shared Property DataSource As String
        Get
            If Platform = DbPlatform.Sqlite Then
                Return Nothing
            Else
                Return _DataSource
            End If
        End Get
        Set(value As String)
            _DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the Oracle Wallet path name used for Mutual TLS (mTLS) authentication.
    ''' </summary>
    ''' <returns>Path name</returns>
    Public Shared ReadOnly Property OracleWalletPath As String
        Get
            Dim wallet As String = My.Computer.Registry.GetValue(AppProperties.RegistryTopLevelKeyFullName,
                                                                 "OracleWalletPath", Nothing)
            Return wallet
        End Get
    End Property

    ''' <summary>
    ''' Gets or Sets the local SQLite database path name.
    ''' </summary>
    ''' <returns>Path name or nothing when the file does not exist.</returns>
    Public Shared Property LocalDatabasePath As String
        Get
            Dim folderPath = My.Computer.Registry.GetValue(AppProperties.RegistryTopLevelKeyFullName, "LocalDatabasePath",
                                                           AppFolders.GetPath(
                                                           AppFolders.AppFolder.DataTopLevel))
            Dim filePath = IO.Path.Combine(folderPath, String.Concat(My.Application.Info.ProductName, ".sqlite"))
            If IO.File.Exists(filePath) Then
                Return filePath
            Else
                Return Nothing
            End If
        End Get
        Set(value As String)
            My.Computer.Registry.SetValue(AppProperties.RegistryTopLevelKeyFullName, "LocalDatabasePath",
                                          Path.GetDirectoryName(value))
        End Set
    End Property

    Private Shared Sub NotifyPropertyChanged(<CallerMemberName> ByVal Optional propertyName As String = "")
        If propertyName = "Platform" Then
            If Platform = DbPlatform.Oracle And odpResolverEnabled = False Then
                AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf LoadOracleDataProvider
                odpResolverEnabled = True
            End If
        End If
    End Sub

    Private Shared Function LoadOracleDataProvider() As Reflection.Assembly
        Try
            Dim dllPath = My.Computer.Registry.GetValue(String.Concat("HKEY_LOCAL_MACHINE\SOFTWARE\Oracle\ODP.NET\",
                                                                      My.Resources.OracleDataProviderVersion),
                                                        "DllPath", "")
            Dim oraKeyPath = String.Concat("HKEY_LOCAL_MACHINE\",
                                           File.ReadAllText(String.Concat(dllPath, "\oracle.key"))).TrimEnd
            Dim assemblyPath = String.Concat(My.Computer.Registry.GetValue(oraKeyPath, "ORACLE_HOME", ""),
                                             "\odp.net\managed\common\Oracle.ManagedDataAccess.dll")
            Return Reflection.Assembly.LoadFile(assemblyPath)
        Catch ex As FileNotFoundException
            Dim commonDialogs = New CommonDialogs
            commonDialogs.ShowMessageBox(My.Resources.OracleDataProviderMissing, True)
            Return Nothing
        End Try
    End Function
End Class
