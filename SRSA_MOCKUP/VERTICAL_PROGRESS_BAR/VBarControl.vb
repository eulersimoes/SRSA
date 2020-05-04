Public Class VBarControl
    Public Function setarValor(valor As Integer)

        Try
            Vbar.Value = valor
        Catch ex As Exception

        End Try

    End Function

End Class
