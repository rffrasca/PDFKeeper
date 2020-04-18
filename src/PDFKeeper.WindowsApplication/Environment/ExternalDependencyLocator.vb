'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage and Management
'* Copyright (C) 2009-2020  Robert F. Frasca
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
Public NotInheritable Class ExternalDependencyLocator
    Private Sub New()
        ' All members are shared.
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", _
        "CA2001:AvoidCallingProblematicMethods", _
        MessageId:="System.Reflection.Assembly.LoadFile")> _
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", _
        "CA1024:UsePropertiesWhereAppropriate")> _
    Public Shared Function GetOracleDataAccessAssemblyPath() As Reflection.Assembly
        Try
            Dim odpDllPath As String = My.Computer.Registry.GetValue( _
                "HKEY_LOCAL_MACHINE\SOFTWARE\Oracle\ODP.NET\" & _
                My.Settings.ODPMVersion, _
                "DllPath", _
                "")
            Dim oraHomeKey As String = "HKEY_LOCAL_MACHINE\" & _
                My.Computer.FileSystem.ReadAllText( _
                    odpDllPath & "\oracle.key").TrimEnd
            Dim assemblyPath As String = My.Computer.Registry.GetValue( _
                oraHomeKey, _
                "ORACLE_HOME", "") & _
                "\odp.net\managed\common\Oracle.ManagedDataAccess.dll"
            Return Reflection.Assembly.LoadFile(assemblyPath)
        Catch ex As FileNotFoundException
            Dim messageService As IMessageDisplayService = New MessageDisplayService
            messageService.Show(My.Resources.ODPMissing, True)
            Return Nothing
        End Try
    End Function
End Class
