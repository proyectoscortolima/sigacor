﻿Public Class NuevoRegistro
    Inherits System.Web.UI.Page

    Dim parametrizacion As New clParametrizacion
    Dim report As New clReportPac
    Dim users As New clLogin
    Dim fun As New Funciones

#Region "Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlPac.Visible = True
            pestaña(1)
            pnlSubNivel.Visible = False
            pnlNiveles.Visible = False
            pnlPlanAccion.Visible = False
            pnlMetas.Visible = False
            lblError.Visible = False
            btnActPac.Visible = False
            pnlNiv2.Visible = False
            pnlNiv3.Visible = False
            pnlNiv4.Visible = False
            pnlNiv5.Visible = False
            btnFiltro.Visible = False
            pnlNuevoJerarquia.Visible = False
            pnlNiv2Meta.Visible = False
            pnlNiv3Meta.Visible = False
            pnlNiv4Meta.Visible = False
            pnlNiv5Meta.Visible = False
            pnlMetaNuevo.Visible = False
            btnFiltroMeta.Visible = False

            pnlNvl1Reg.Visible = False
            pnlNvl2Reg.Visible = False
            pnlNvl3Reg.Visible = False
            pnlNvl4Reg.Visible = False
            pnlNvl5Reg.Visible = False

            lblLineas.Text = "No hay niveles"
            lblLineasMeta.Text = "No hay lineas"
            Session("Actualizar") = "N"

            DataT = Nothing
            DataT = users.selectUsuario
            If DataT.Rows.Count > 0 Then
                cmbAlimentador.Items.Clear()
                cmbAlimentador.DataTextField = "nombreEmp"
                cmbAlimentador.DataValueField = "user_id"
                cmbAlimentador.DataSource = DataT
                cmbAlimentador.DataBind()
                cmbAlimentador.Items.Insert(0, New ListItem("---Seleccione---", ""))

                cmbAlimentadorMdl.Items.Clear()
                cmbAlimentadorMdl.DataTextField = "nombreEmp"
                cmbAlimentadorMdl.DataValueField = "user_id"
                cmbAlimentadorMdl.DataSource = DataT
                cmbAlimentadorMdl.DataBind()
                cmbAlimentadorMdl.Items.Insert(0, New ListItem("---Seleccione---", ""))
            End If

            DataT = Nothing
            DataT = parametrizacion.selectComdepndnc
            If DataT.Rows.Count > 0 Then
                cmbResponsable.Items.Clear()
                cmbResponsable.DataTextField = "nombr_depndnc"
                cmbResponsable.DataValueField = "codg_depndnc"
                cmbResponsable.DataSource = DataT
                cmbResponsable.DataBind()
                cmbResponsable.Items.Insert(0, New ListItem("---Seleccione---", ""))

                cmbResponsableMdl.Items.Clear()
                cmbResponsableMdl.DataTextField = "nombr_depndnc"
                cmbResponsableMdl.DataValueField = "codg_depndnc"
                cmbResponsableMdl.DataSource = DataT
                cmbResponsableMdl.DataBind()
                cmbResponsableMdl.Items.Insert(0, New ListItem("---Seleccione---", ""))
            End If

            DataT = Nothing
            DataT = fun.goal_type
            If DataT.Rows.Count > 0 Then
                cmbTipoMeta.Items.Clear()
                cmbTipoMeta.DataTextField = "description"
                cmbTipoMeta.DataValueField = "name"
                cmbTipoMeta.DataSource = DataT
                cmbTipoMeta.DataBind()
                cmbTipoMeta.Items.Insert(0, New ListItem("---Seleccione---", ""))

                cmbTipoMetaMdl.Items.Clear()
                cmbTipoMetaMdl.DataTextField = "description"
                cmbTipoMetaMdl.DataValueField = "name"
                cmbTipoMetaMdl.DataSource = DataT
                cmbTipoMetaMdl.DataBind()
                cmbTipoMetaMdl.Items.Insert(0, New ListItem("---Seleccione---", ""))
            End If

            visualizarPac(Session("id_pac"))
            cargarLineas()
            Session("id_pac") = Nothing
            Session("pac") = Nothing

            End If

            If tblNiveles.Rows.Count > 0 Then
            tblNiveles.UseAccessibleHeader = True
            tblNiveles.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
        If tblPlanAccion.Rows.Count > 0 Then
            tblPlanAccion.UseAccessibleHeader = True
            tblPlanAccion.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
        If tblMetas.Rows.Count > 0 Then
            tblMetas.UseAccessibleHeader = True
            tblMetas.HeaderRow.TableSection = TableRowSection.TableHeader
        End If

    End Sub

#End Region

#Region "TextChanged"
    Private Sub txtYearInicial_TextChanged(sender As Object, e As EventArgs) Handles txtYearInicial.TextChanged
        calcularYearFinal()
        txtCantYears.Focus()
    End Sub

    Private Sub txtCantYears_TextChanged(sender As Object, e As EventArgs) Handles txtCantYears.TextChanged
        calcularYearFinal()
        txtYearFinal.Focus()
    End Sub

#End Region

#Region "SelectedIndexChanged"

    Private Sub cmbNiveles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiveles.SelectedIndexChanged
        Try

            If cmbNiveles.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el nivel que desea ingresar", "info", "contenedor2_cmbNiveles")
                Exit Sub
            End If

            cmbNvl1Reg.Items.Clear()
            pnlNvl1Reg.Visible = False
            cmbNvl2Reg.Items.Clear()
            pnlNvl2Reg.Visible = False
            cmbNvl3Reg.Items.Clear()
            pnlNvl3Reg.Visible = False
            cmbNvl4Reg.Items.Clear()
            pnlNvl4Reg.Visible = False
            cmbNvl5Reg.Items.Clear()
            pnlNvl5Reg.Visible = False

            If CInt(1 < cmbNiveles.SelectedValue) Then
                DataT = Nothing
                DataT = report.selectContentsFiltro(lblPac.Text.Trim, "", "1")
                If DataT.Rows.Count > 0 Then
                    lblNvl1Reg.Text = "¿A que " & DataT(0)(3) & " pertenece?"
                    pnlNvl1Reg.Visible = True

                    cmbNvl1Reg.Items.Clear()
                    cmbNvl1Reg.DataTextField = "name"
                    cmbNvl1Reg.DataValueField = "code"
                    cmbNvl1Reg.DataSource = DataT
                    cmbNvl1Reg.DataBind()
                    cmbNvl1Reg.Items.Insert(0, New ListItem("---Seleccione--", ""))
                    cmbNvl1Reg.Focus()
                Else
                    txtNombrePlanAcc.Focus()
                End If
            End If

            lblCodigo.Text = "Código de " & cmbNiveles.SelectedItem.ToString
            lblNombre.Text = "Nombre de " & cmbNiveles.SelectedItem.ToString
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub cmbNvl1Reg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNvl1Reg.SelectedIndexChanged
        Try

            DataT = Nothing
            If CInt(2 < cmbNiveles.SelectedValue) Then
                If cmbNvl1Reg.SelectedIndex = 0 Then
                    cmbNvl2Reg.Items.Clear()
                    pnlNvl2Reg.Visible = False
                Else
                    DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNvl1Reg.SelectedValue)
                    If DataT.Rows.Count > 0 Then
                        cmbNvl2Reg.Items.Clear()
                        cmbNvl2Reg.DataTextField = "name"
                        cmbNvl2Reg.DataValueField = "code"
                        cmbNvl2Reg.DataSource = DataT
                        cmbNvl2Reg.DataBind()
                        cmbNvl2Reg.Items.Insert(0, New ListItem("---Seleccione---", ""))
                        lblNvl2Reg.Text = DataT(0)(2)
                        pnlNvl2Reg.Visible = True
                        cmbNvl2Reg.Focus()
                    Else
                        cmbNvl2Reg.Items.Clear()
                        pnlNvl2Reg.Visible = False
                        alerta("Advertencia", "No se han encontrado programas", "info")
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub cmbNvl2Reg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNvl2Reg.SelectedIndexChanged
        Try

            DataT = Nothing
            If CInt(3 < cmbNiveles.SelectedValue) Then
                If cmbNvl2Reg.SelectedIndex = 0 Then
                    cmbNvl3Reg.Items.Clear()
                    pnlNvl3Reg.Visible = False
                Else
                    DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNvl2Reg.SelectedValue)
                    If DataT.Rows.Count > 0 Then
                        cmbNvl3Reg.Items.Clear()
                        cmbNvl3Reg.DataTextField = "name"
                        cmbNvl3Reg.DataValueField = "code"
                        cmbNvl3Reg.DataSource = DataT
                        cmbNvl3Reg.DataBind()
                        cmbNvl3Reg.Items.Insert(0, New ListItem("---Seleccione---", ""))
                        lblNvl3Reg.Text = DataT(0)(2)
                        pnlNvl3Reg.Visible = True
                        cmbNvl3Reg.Focus()
                    Else
                        cmbNvl3Reg.Items.Clear()
                        pnlNvl3Reg.Visible = False
                        alerta("Advertencia", "No se han encontrado Proyectos", "info")
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub cmbNvl3Reg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNvl3Reg.SelectedIndexChanged
        Try

            DataT = Nothing
            If CInt(4 < cmbNiveles.SelectedValue) Then
                If cmbNvl3Reg.SelectedIndex = 0 Then
                    cmbNvl4Reg.Items.Clear()
                    pnlNvl4Reg.Visible = False
                Else
                    DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNvl3Reg.SelectedValue)
                    If DataT.Rows.Count > 0 Then
                        cmbNvl4Reg.Items.Clear()
                        cmbNvl4Reg.DataTextField = "name"
                        cmbNvl4Reg.DataValueField = "code"
                        cmbNvl4Reg.DataSource = DataT
                        cmbNvl4Reg.DataBind()
                        cmbNvl4Reg.Items.Insert(0, New ListItem("---Seleccione---", ""))
                        lblNvl4Reg.Text = DataT(0)(2)
                        pnlNvl4Reg.Visible = True
                        cmbNvl4Reg.Focus()
                    Else
                        cmbNvl4Reg.Items.Clear()
                        pnlNvl4Reg.Visible = False
                        alerta("Advertencia", "No se han encontrado Actividades", "info")
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub cmbNvl4Reg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNvl4Reg.SelectedIndexChanged
        Try

            DataT = Nothing
            If CInt(5 < cmbNiveles.SelectedValue) Then
                If cmbNvl4Reg.SelectedIndex = 0 Then
                    cmbNvl5Reg.Items.Clear()
                    pnlNvl5Reg.Visible = False
                Else
                    DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNvl4Reg.SelectedValue)
                    If DataT.Rows.Count > 0 Then
                        cmbNvl5Reg.Items.Clear()
                        cmbNvl5Reg.DataTextField = "name"
                        cmbNvl5Reg.DataValueField = "code"
                        cmbNvl5Reg.DataSource = DataT
                        cmbNvl5Reg.DataBind()
                        cmbNvl5Reg.Items.Insert(0, New ListItem("---Seleccione---", ""))
                        lblNvl5Reg.Text = DataT(0)(2)
                        pnlNvl5Reg.Visible = True
                        cmbNvl5Reg.Focus()
                    Else
                        cmbNvl5Reg.Items.Clear()
                        pnlNvl5Reg.Visible = False
                        alerta("Advertencia", "No se han encontrado Actividades", "info")
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub



    Private Sub cmbLineas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLineas.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbLineas.SelectedIndex = 0 Then
                cmbNiv2.Items.Clear()
                pnlNiv2.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbLineas.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv2.Items.Clear()
                    cmbNiv2.DataTextField = "name"
                    cmbNiv2.DataValueField = "code"
                    cmbNiv2.DataSource = DataT
                    cmbNiv2.DataBind()
                    cmbNiv2.Items.Insert(0, New ListItem("Todos", ""))
                    lblNiv2.Text = DataT(0)(2)
                    pnlNiv2.Visible = True
                Else
                    cmbNiv2.Items.Clear()
                    pnlNiv2.Visible = False
                    alerta("Advertencia", "No se han encontrado registros", "info")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub cmbLineasMeta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLineasMeta.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbLineasMeta.SelectedIndex = 0 Then
                cmbNiv2Meta.Items.Clear()
                pnlNiv2Meta.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbLineasMeta.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv2Meta.Items.Clear()
                    cmbNiv2Meta.DataTextField = "name"
                    cmbNiv2Meta.DataValueField = "code"
                    cmbNiv2Meta.DataSource = DataT
                    cmbNiv2Meta.DataBind()
                    cmbNiv2Meta.Items.Insert(0, New ListItem("Todos", ""))
                    lblNiv2Meta.Text = DataT(0)(2)
                    pnlNiv2Meta.Visible = True
                Else
                    cmbNiv2Meta.Items.Clear()
                    pnlNiv2Meta.Visible = False
                    alerta("Advertencia", "No se han encontrado registros", "info")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub cmbNiv2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiv2.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbNiv2.SelectedIndex = 0 Then
                cmbNiv3.Items.Clear()
                pnlNiv3.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNiv2.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv3.Items.Clear()
                    cmbNiv3.DataTextField = "name"
                    cmbNiv3.DataValueField = "code"
                    cmbNiv3.DataSource = DataT
                    cmbNiv3.DataBind()
                    cmbNiv3.Items.Insert(0, New ListItem("Todos", ""))
                    lblNiv3.Text = DataT(0)(2)
                    pnlNiv3.Visible = True
                Else
                    cmbNiv3.Items.Clear()
                    pnlNiv3.Visible = False
                    alerta("Advertencia", "No se han encontrado registros", "info")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub cmbNiv2Meta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiv2Meta.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbNiv2Meta.SelectedIndex = 0 Then
                cmbNiv3Meta.Items.Clear()
                pnlNiv3Meta.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNiv2Meta.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv3Meta.Items.Clear()
                    cmbNiv3Meta.DataTextField = "name"
                    cmbNiv3Meta.DataValueField = "code"
                    cmbNiv3Meta.DataSource = DataT
                    cmbNiv3Meta.DataBind()
                    cmbNiv3Meta.Items.Insert(0, New ListItem("Todos", ""))
                    lblNiv3Meta.Text = DataT(0)(2)
                    pnlNiv3Meta.Visible = True
                Else
                    cmbNiv3Meta.Items.Clear()
                    pnlNiv3Meta.Visible = False
                    alerta("Advertencia", "No se han encontrado registros", "info")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub cmbNiv3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiv3.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbNiv3.SelectedIndex = 0 Then
                cmbNiv4.Items.Clear()
                pnlNiv4.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNiv3.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv4.Items.Clear()
                    cmbNiv4.DataTextField = "name"
                    cmbNiv4.DataValueField = "code"
                    cmbNiv4.DataSource = DataT
                    cmbNiv4.DataBind()
                    cmbNiv4.Items.Insert(0, New ListItem("Todos", ""))
                    lblNiv4.Text = DataT(0)(2)
                    pnlNiv4.Visible = True
                Else
                    cmbNiv4.Items.Clear()
                    pnlNiv4.Visible = False
                    alerta("Advertencia", "No se han encontrado registros", "info")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub cmbNiv4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiv4.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbNiv4.SelectedIndex = 0 Then
                cmbNiv5.Items.Clear()
                pnlNiv5.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNiv4.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv5.Items.Clear()
                    cmbNiv5.DataTextField = "name"
                    cmbNiv5.DataValueField = "code"
                    cmbNiv5.DataSource = DataT
                    cmbNiv5.DataBind()
                    cmbNiv5.Items.Insert(0, New ListItem("Todos", ""))
                    lblNiv5.Text = DataT(0)(2)
                    pnlNiv5.Visible = True
                Else
                    cmbNiv5.Items.Clear()
                    pnlNiv5.Visible = False
                    alerta("Advertencia", "No se han encontrado registros", "info")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub cmbNiv3Meta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiv3Meta.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbNiv3Meta.SelectedIndex = 0 Then
                cmbNiv4Meta.Items.Clear()
                pnlNiv4Meta.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNiv3Meta.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv4Meta.Items.Clear()
                    cmbNiv4Meta.DataTextField = "name"
                    cmbNiv4Meta.DataValueField = "code"
                    cmbNiv4Meta.DataSource = DataT
                    cmbNiv4Meta.DataBind()
                    cmbNiv4Meta.Items.Insert(0, New ListItem("Todos", ""))
                    lblNiv4Meta.Text = DataT(0)(2)
                    pnlNiv4Meta.Visible = True
                Else
                    cmbNiv4Meta.Items.Clear()
                    pnlNiv4Meta.Visible = False
                    alerta("Advertencia", "No se han encontrado registros", "info")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub cmbNiv4Meta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiv4Meta.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbNiv4Meta.SelectedIndex = 0 Then
                cmbNiv5Meta.Items.Clear()
                pnlNiv5Meta.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNiv4Meta.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv5Meta.Items.Clear()
                    cmbNiv5Meta.DataTextField = "name"
                    cmbNiv5Meta.DataValueField = "code"
                    cmbNiv5Meta.DataSource = DataT
                    cmbNiv5Meta.DataBind()
                    cmbNiv5Meta.Items.Insert(0, New ListItem("Todos", ""))
                    lblNiv5Meta.Text = DataT(0)(2)
                    pnlNiv5Meta.Visible = True
                Else
                    cmbNiv5Meta.Items.Clear()
                    pnlNiv5Meta.Visible = False
                    alerta("Advertencia", "No se han encontrado registros", "info")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region

#Region "RowDataBound"
    Private Sub tblPlanAccion_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles tblPlanAccion.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(0).Visible = False
                e.Row.Cells(4).Visible = False
                e.Row.Cells(6).Visible = False

                Dim linkBtnEditar, linkBtnEliminar, linkBtnConfirmar As New LinkButton
                linkBtnEditar = e.Row.FindControl("lnkEditPlanAcc")
                linkBtnEliminar = e.Row.FindControl("lnkEliPlanAcc")
                linkBtnConfirmar = e.Row.FindControl("lnkConEditPlanAcc")

                linkBtnEditar.CommandArgument = e.Row.Cells(0).Text.Trim
                'linkBtnEliminar.CommandArgument = e.Row.Cells(0).Text.Trim

                linkBtnConfirmar.Visible = False

            End If
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Visible = False
                e.Row.Cells(4).Visible = False
                e.Row.Cells(6).Visible = False                
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub tblNiveles_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles tblNiveles.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(0).Visible = False
                e.Row.Cells(3).Visible = False
                'e.Row.Cells(4).Visible = False

                Dim linkBtnEditar, linkBtnEliminar, linkBtnConfirmar As New LinkButton
                linkBtnEditar = e.Row.FindControl("lnkEditNiv")
                'linkBtnEliminar = e.Row.FindControl("lnkEliNiv")
                linkBtnConfirmar = e.Row.FindControl("lnkConEdit")

                linkBtnEditar.CommandArgument = e.Row.Cells(0).Text.Trim
                'linkBtnEliminar.CommandArgument = e.Row.Cells(0).Text.Trim

                linkBtnConfirmar.Visible = False

            End If
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Visible = False
                e.Row.Cells(3).Visible = False
                'e.Row.Cells(4).Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub tblMetas_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles tblMetas.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim linkBtn, linkBtn2 As New LinkButton
                linkBtn = e.Row.FindControl("lnkEditarMeta")
                'linkBtn2 = e.Row.FindControl("lnkEliminarMeta")

                linkBtn.CommandArgument = e.Row.Cells(0).Text.Trim
                'linkBtn2.CommandArgument = e.Row.Cells(0).Text.Trim

                e.Row.Cells(0).Visible = False
            End If
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region

#Region "RowCommand"
    Private Sub tblNiveles_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles tblNiveles.RowCommand
        Try

            Dim linkBtnConfirmar, linkBtnEditar, linkBtnEliminar As New LinkButton
            Dim nombre As TextBox

            If e.CommandName = "Editar" Then
                For Each row As GridViewRow In tblNiveles.Rows

                    linkBtnConfirmar = tblNiveles.Rows(row.RowIndex).FindControl("lnkConEdit")
                    linkBtnEditar = tblNiveles.Rows(row.RowIndex).FindControl("lnkEditNiv")
                    'linkBtnEliminar = tblNiveles.Rows(row.RowIndex).FindControl("lnkEliNiv")

                    'codigo = tblNiveles.Rows(row.RowIndex).FindControl("txtCodigo")
                    nombre = tblNiveles.Rows(row.RowIndex).FindControl("txtNombre")

                    If e.CommandArgument = row.Cells(0).Text Then

                        linkBtnEditar.Visible = False
                        linkBtnConfirmar.Visible = True
                        'linkBtnEliminar.Visible = False

                        'codigo.Text = row.Cells(1).Text.Trim
                        nombre.Text = HttpUtility.HtmlDecode(row.Cells(2).Text.Trim)

                        row.Cells(2).Visible = False
                        row.Cells(3).Visible = True

                        'row.Cells(3).Visible = False
                        'row.Cells(4).Visible = True


                    Else
                        linkBtnEditar.Visible = False
                        linkBtnEliminar.Visible = False
                    End If
                Next

            ElseIf e.CommandName = "Confirmar" Then

                For Each row As GridViewRow In tblNiveles.Rows
                    'codigo = tblNiveles.Rows(row.RowIndex).FindControl("txtCodigo")
                    nombre = tblNiveles.Rows(row.RowIndex).FindControl("txtNombre")

                    If row.Cells(3).Visible = True Then
                        'If codigo.Text = String.Empty Then
                        '    alerta("Advertencia", "Ingrese el código del nivel", "info")
                        '    Exit Sub
                        'End If
                        If nombre.Text = String.Empty Then
                            alerta("Advertencia", "Ingrese el nombre del nivel", "info")
                            Exit Sub
                        End If
                        parametrizacion.updateLevels(row.Cells(0).Text.Trim, nombre.Text.Trim, row.Cells(1).Text.Trim, "A")
                        'Fila = Nothing
                        'Fila = parametrizacion.selectLevelsFila(lblPac.Text.Trim, row.Cells(1).Text.Trim, nombre.Text.Trim)
                        'If Fila Is Nothing Then

                        '    If row.Cells(1).Text.Trim <> codigo.Text.Trim Then
                        '        Fila = Nothing
                        '        Fila = parametrizacion.selectLevelsFila(lblPac.Text.Trim, codigo.Text.Trim)
                        '        If Fila IsNot Nothing Then
                        '            alerta("Advertencia", "El código del nivel, ya existe", "info")
                        '            Exit Sub
                        '        End If
                        '    Else

                        '    End If

                        'End If
                    End If
                Next

                cargarNiveles(lblPac.Text.Trim)
                alerta("Se ha actualizado el nivel correctamente", "", "success")

            ElseIf e.CommandName = "Eliminar" Then
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alertaNivel", "AlertaEliminacionNivel();", True)
                Session("idNivel") = e.CommandArgument
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub tblPlanAccion_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles tblPlanAccion.RowCommand
        Try

            Dim linkBtnConfirmar, linkBtnEditar, linkBtnEliminar As New LinkButton
            'Dim jerarquia, nombre, peso As TextBox
            Dim nombre, peso As TextBox

            If e.CommandName = "Editar" Then
                For Each row As GridViewRow In tblPlanAccion.Rows

                    linkBtnConfirmar = tblPlanAccion.Rows(row.RowIndex).FindControl("lnkConEditPlanAcc")
                    linkBtnEditar = tblPlanAccion.Rows(row.RowIndex).FindControl("lnkEditPlanAcc")
                    'linkBtnEliminar = tblPlanAccion.Rows(row.RowIndex).FindControl("lnkEliPlanAcc")

                    'jerarquia = tblPlanAccion.Rows(row.RowIndex).FindControl("txtJerarquia")
                    nombre = tblPlanAccion.Rows(row.RowIndex).FindControl("txtNombrePlanAcc")
                    peso = tblPlanAccion.Rows(row.RowIndex).FindControl("txtPeso")

                    If e.CommandArgument = row.Cells(0).Text Then

                        linkBtnEditar.Visible = False
                        linkBtnConfirmar.Visible = True
                        'linkBtnEliminar.Visible = False

                        'jerarquia.Text = row.Cells(2).Text.Trim
                        nombre.Text = HttpUtility.HtmlDecode(row.Cells(3).Text.Trim)
                        peso.Text = row.Cells(5).Text.Trim

                        row.Cells(3).Visible = False
                        row.Cells(4).Visible = True

                        row.Cells(5).Visible = False
                        row.Cells(6).Visible = True
                    Else
                        linkBtnEditar.Visible = False
                        'linkBtnEliminar.Visible = False
                    End If
                Next

            ElseIf e.CommandName = "Confirmar" Then

                For Each row As GridViewRow In tblPlanAccion.Rows
                    'jerarquia = tblPlanAccion.Rows(row.RowIndex).FindControl("txtJerarquia")
                    nombre = tblPlanAccion.Rows(row.RowIndex).FindControl("txtNombrePlanAcc")
                    peso = tblPlanAccion.Rows(row.RowIndex).FindControl("txtPeso")

                    If row.Cells(4).Visible = True Then
                        'If jerarquia.Text = String.Empty Then
                        '    alerta("Advertencia", "Ingrese el código de la jerarquia", "info")
                        '    Exit Sub
                        'End If
                        If nombre.Text = String.Empty Then
                            alerta("Advertencia", "Ingrese el nombre del plan de acción cuatrienal", "info")
                            Exit Sub
                        End If
                        If peso.Text = String.Empty Then
                            alerta("Advertencia", "Ingrese el peso del plan de acción cuatrienal", "info")
                            Exit Sub
                        End If
                        parametrizacion.updateContents(row.Cells(0).Text.Trim, row.Cells(2).Text.Trim, nombre.Text.Trim, peso.Text.Trim)
                        Exit For
                    End If
                Next

                'cargarPlanAccion(lblPac.Text.Trim)
                btnConsultar_Click(Nothing, Nothing)
                alerta("Se ha actualizado el plan de acción cuatrienal correctamente", "", "success")

            ElseIf e.CommandName = "Eliminar" Then
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alertaPlanAcc", "AlertaEliminacionPlanAcc();", True)
                Session("idPlanAcc") = e.CommandArgument
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub tblMetas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles tblMetas.RowCommand
        Try
            lblIdMeta.Text = e.CommandArgument

            If e.CommandName = "Editar" Then
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "abrirModal();", True)
                Fila = parametrizacion.selectGoals(lblPac.Text.Trim, lblIdMeta.Text)
                If Fila IsNot Nothing Then
                    txtNombreMetaMdl.Text = Fila("name")
                    cmbTipoMetaMdl.SelectedValue = Fila("type_goal")
                    cmbNivelMetaMdl.SelectedValue = Fila("subactivity")
                    txtLineaBaseMetaMdl.Text = Fila("line_base")
                    txtPriYearMetaMdl.Text = Fila("value_one_year")
                    txtSegYearMetaMdl.Text = Fila("value_two_year")
                    txtTercYearMetaMdl.Text = Fila("value_three_year")
                    txtCuartYearMetaMdl.Text = Fila("value_four_year")
                    cmbResponsableMdl.SelectedValue = Fila("responsable_id")
                    cmbAlimentadorMdl.SelectedValue = Fila("feeder_id")
                End If

            ElseIf e.CommandName = "Eliminar" Then
                parametrizacion.updateStateGoals(lblIdMeta.Text.Trim)
                btnConsultarMeta_Click(Nothing, Nothing)
                'cargarMetas(lblPac.Text.Trim, 0)
                alerta("Se ha eliminado la meta correctamente", "", "success", "")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region

#Region "Click"

#Region "Pestañas"
    Private Sub btnPac_Click(sender As Object, e As EventArgs) Handles btnPac.Click
        Try
            pnlPac.Visible = True
            pestaña(1)
            pnlNiveles.Visible = False
            pnlPlanAccion.Visible = False
            pnlMetas.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub btnNiveles_Click(sender As Object, e As EventArgs) Handles btnNiveles.Click
        Try
            If lblPac.Text = String.Empty Then
                alerta("Advertencia", "El pac no esta registrado", "info", "")
                btnPac_Click(Nothing, Nothing)
                Exit Sub
            End If

            pestaña(2)
            pnlPac.Visible = False
            pnlNiveles.Visible = True
            pnlPlanAccion.Visible = False
            pnlMetas.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnPlanAccion_Click(sender As Object, e As EventArgs) Handles btnPlanAccion.Click
        Try
            If lblPac.Text = String.Empty Then
                alerta("Advertencia", "El pac no esta registrado", "info", "")
                btnPac_Click(Nothing, Nothing)
                Exit Sub
            End If

            pestaña(3)
            pnlPac.Visible = False
            pnlNiveles.Visible = False
            pnlPlanAccion.Visible = True
            pnlMetas.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnMetas_Click(sender As Object, e As EventArgs) Handles btnMetas.Click
        Try
            If lblPac.Text = String.Empty Then
                alerta("Advertencia", "El pac no esta registrado", "info", "")
                btnPac_Click(Nothing, Nothing)
                Exit Sub
            End If

            pestaña(4)
            pnlPac.Visible = False
            pnlNiveles.Visible = False
            pnlPlanAccion.Visible = False
            pnlMetas.Visible = True
            cargarMetas(lblPac.Text.Trim, 1)
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region
    Private Sub btnSigPac_Click(sender As Object, e As EventArgs) Handles btnSigPac.Click
        Try
            If Session("Actualizar") = "N" Then
                DataT = Nothing
                DataT = parametrizacion.selectPac
                If DataT.Rows.Count > 0 Then
                    alerta("Advertencia", "No se puede grabar el pac, se encuentra un pac activo", "info", "")
                    Exit Sub
                End If
                DataT = Nothing
                DataT = parametrizacion.selectPacPeriodo(txtYearInicial.Text.Trim, txtYearFinal.Text.Trim)
                If DataT.Rows.Count > 0 Then
                    alerta("Advertencia", "No se puede grabar el pac, el periodo ya fue cerrado", "info", "")
                    Exit Sub
                End If
            End If

            Session("Actualizar") = "N"

            If txtNomPac.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el nombre el PAC", "info", "contenedor2_txtNomPac")
                Exit Sub
            End If
            If txtYearInicial.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el año inicial", "info", "contenedor2_txtYearInicial")
                Exit Sub
            End If
            If txtCantYears.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la cantidad de años", "info", "contenedor2_txtCantYears")
                Exit Sub
            End If

            If parametrizacion.updatePac(txtNomPac.Text.Trim, txtYearInicial.Text.Trim,
                                         txtYearFinal.Text.Trim, txtCantYears.Text.Trim,
                                         "A", lblPac.Text.Trim) > 0 Then

                alerta("Se ha actualizado el pac correctamente", "PAC:  " & lblPac.Text.Trim, "success", "")
            Else
                parametrizacion.insertPac(txtNomPac.Text.Trim, txtYearInicial.Text.Trim,
                                          txtYearFinal.Text.Trim, txtCantYears.Text.Trim, "A")
                lblPac.Text = parametrizacion.consecutivoPac
                alerta("Se ha creado el PAC correctamente", "Pac:  " & lblPac.Text.Trim, "success", "")
            End If

            pestaña(2)
            pnlPac.Visible = False
            pnlNiveles.Visible = True
            pnlPlanAccion.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnActPac_Click(sender As Object, e As EventArgs) Handles btnActPac.Click
        Session("Actualizar") = "S"
        btnSigPac_Click(Nothing, Nothing)
    End Sub

    Private Sub btnSigNiveles_Click(sender As Object, e As EventArgs) Handles btnSigNiveles.Click
        Try
            cargarNiveles(lblPac.Text.Trim)

            pestaña(3)
            pnlPac.Visible = False
            pnlNiveles.Visible = False
            pnlPlanAccion.Visible = True

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnAtrasNiveles_Click(sender As Object, e As EventArgs) Handles btnAtrasNiveles.Click
        Try
            btnPac_Click(Nothing, Nothing)
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnAtrasPlanAcc_Click(sender As Object, e As EventArgs) Handles btnAtrasPlanAcc.Click
        Try
            pestaña(2)
            pnlPac.Visible = False
            pnlNiveles.Visible = True
            pnlPlanAccion.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnAtrasMetas_Click(sender As Object, e As EventArgs) Handles btnAtrasMetas.Click
        Try
            pestaña(3)
            pnlPac.Visible = False
            pnlNiveles.Visible = False
            pnlPlanAccion.Visible = True
            pnlMetas.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnSigPlanAcc_Click(sender As Object, e As EventArgs) Handles btnSigPlanAcc.Click
        Try
            If lblPac.Text = String.Empty Then
                alerta("Advertencia", "El PAC no esta registrado", "info", "")
                btnPac_Click(Nothing, Nothing)
                Exit Sub
            End If
            If tblPlanAccion.Rows.Count = 0 Then
                alerta("Advertencia", "Ingrese la parametrización de plan de acción cuatrienal", "info", "")
                Exit Sub
            End If

            'cargarMetas(lblPac.Text.Trim, 1)
            btnMetas_Click(Nothing, Nothing)
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try

    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            Dim hierarchy As Integer = 1
            If txtNombreNiv.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el nombre del nivel", "info", "contenedor2_txtNombreNiv")
                Exit Sub
            End If

            DataT = Nothing
            DataT = parametrizacion.selectLevels(lblPac.Text.Trim, "hierarchy desc")
            If DataT.Rows.Count > 0 Then
                hierarchy = CInt(DataT(0)(3)) + 1
            End If

            If parametrizacion.insertLevels(txtNombreNiv.Text.Trim, lblPac.Text.Trim, hierarchy, "A") > 0 Then
                cargarNiveles(lblPac.Text.Trim)
                txtNombreNiv.Text = String.Empty
            Else
                alerta("Advertencia", "Se genero un error al grabar", "error", "")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnAgregarPlanAcc_Click(sender As Object, e As EventArgs) Handles btnAgregarPlanAcc.Click
        Try
            Dim code, array As String
            Dim subNivel, name As String

            If cmbNiveles.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione un nivel", "info", "contenedor2_cmbNiveles")
                Exit Sub
            End If
            If pnlNvl1Reg.Visible = True And cmbNvl1Reg.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione la " & lblNvl1Reg.Text, "info", "contenedor2_cmbNvl1Reg")
                Exit Sub
            Else
                If cmbNvl1Reg.SelectedIndex > 0 Then
                    subNivel = cmbNvl1Reg.SelectedValue
                End If
            End If
            If pnlNvl2Reg.Visible = True And cmbNvl2Reg.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el " & lblNvl2Reg.Text, "info", "contenedor2_cmbNvl2Reg")
                Exit Sub
            Else
                If cmbNvl2Reg.SelectedIndex > 0 Then
                    subNivel = cmbNvl2Reg.SelectedValue
                End If
            End If
            If pnlNvl3Reg.Visible = True And cmbNvl3Reg.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el " & lblNvl3Reg.Text, "info", "contenedor2_cmbNvl3Reg")
                Exit Sub
            Else
                If cmbNvl3Reg.SelectedIndex > 0 Then
                    subNivel = cmbNvl3Reg.SelectedValue
                End If
            End If
            If pnlNvl4Reg.Visible = True And cmbNvl4Reg.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione la " & lblNvl4Reg.Text, "info", "contenedor2_cmbNvl4Reg")
                Exit Sub
            Else
                If cmbNvl4Reg.SelectedIndex > 0 Then
                    subNivel = cmbNvl4Reg.SelectedValue
                End If
            End If
            If pnlNvl5Reg.Visible = True And cmbNvl5Reg.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione la " & lblNvl5Reg.Text, "info", "contenedor2_cmbNvl5Reg")
                Exit Sub
            Else
                If cmbNvl5Reg.SelectedIndex > 0 Then
                    subNivel = cmbNvl5Reg.SelectedValue
                End If
            End If

            If txtCodigo.Text = String.Empty Then
                alerta("Advertencia", "Ingrese un codigo código", "info", "contenedor2_cmbNiveles")
                Exit Sub
            End If
            If txtNombrePlanAcc.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el nombre", "info", "contenedor2_txtNombrePlanAcc")
                Exit Sub
            End If
            If txtPesoPlanAcc.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el peso", "info", "contenedor2_txtPesoPlanAcc")
                Exit Sub
            End If

            If subNivel <> String.Empty Then
                code = subNivel & "." & txtCodigo.Text.Trim
            Else
                code = txtCodigo.Text.Trim
            End If

            DataT = parametrizacion.selectContents(lblPac.Text.Trim, code)
            If DataT.Rows.Count > 0 Then
                alerta("Advertencia", "Jerarquia " & code & " ya existe", "info")
                Exit Sub
            End If


            If parametrizacion.insertContents(lblPac.Text.Trim, cmbNiveles.SelectedValue, code, cmbNiveles.SelectedItem.ToString.Trim, subNivel,
                                           txtNombrePlanAcc.Text.Trim, txtPesoPlanAcc.Text.Trim, "A", array) > 0 Then

                txtCodigo.Text = String.Empty
                txtNombrePlanAcc.Text = String.Empty
                txtPesoPlanAcc.Text = String.Empty

                limiarFiltroRegistro()
                cmbNiveles_SelectedIndexChanged(Nothing, Nothing)
                'cargarLineas()
                btnConsultar_Click(Nothing, Nothing)
                alerta("Se ha creado el item correctamente", "", "success")
            Else
                alerta("Advertencia", "Se genero un error al grabar", "error", "")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnGrabarMetas_Click(sender As Object, e As EventArgs) Handles btnGrabarMetas.Click
        Try
            If txtNombreMeta.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el nombre de la meta", "info", "contenedor2_txtNombreMeta")
                Exit Sub
            End If
            If cmbTipoMeta.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el tipo de meta", "info", "contenedor2_cmbTipoMeta")
                Exit Sub
            End If
            If cmbNivelMeta.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el nivel de la meta", "info", "contenedor2_cmbNivelMeta")
                Exit Sub
            End If
            If txtLineaBaseMeta.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la linea base", "info", "contenedor2_txtLineaBaseMeta")
                Exit Sub
            End If
            If txtPriYearMeta.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la cantidad para el primer año", "info", "contenedor2_txtPriYearMeta")
                Exit Sub
            End If
            If txtSegYearMeta.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la cantidad para el segundo año", "info", "contenedor2_txtSegYearMeta")
                Exit Sub
            End If
            If txtTerYearMeta.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la cantidad para el tercer año", "info", "contenedor2_txtTerYearMeta")
                Exit Sub
            End If
            If txtCuaYearMeta.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la cantidad para el cuarto año", "info", "contenedor2_txtCuaYearMeta")
                Exit Sub
            End If
            If cmbResponsable.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el responsable", "info", "contenedor2_cmbResponsable")
                Exit Sub
            End If
            If cmbAlimentador.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el alimentador", "info", "contenedor2_cmbAlimentador")
                Exit Sub
            End If

            If parametrizacion.insertGoals(lblPac.Text.Trim, txtNombreMeta.Text.Trim, cmbTipoMeta.SelectedValue,
                                           cmbNivelMeta.SelectedValue, txtLineaBaseMeta.Text.Trim, txtPriYearMeta.Text.Trim,
                                           txtSegYearMeta.Text.Trim, txtTerYearMeta.Text.Trim, txtCuaYearMeta.Text.Trim,
                                           cmbResponsable.SelectedValue, cmbAlimentador.SelectedValue, "A") > 0 Then

                alerta("Se ha creado correctamente la meta", "", "success", "")
                limpiarMetas()
            Else
                alerta("Advertencia", "Se genero un error al grabar", "error", "")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnActualizarMeta_Click(sender As Object, e As EventArgs) Handles btnActualizarMeta.Click
        Try
            If txtNombreMetaMdl.Text = String.Empty Then
                alertaMdl("Advertencia", "Ingrese el nombre de la meta", "info", "mdlEditarMeta", "contenedor2_txtNombreMetaMdl")
                Exit Sub
            End If
            If cmbTipoMetaMdl.SelectedIndex = 0 Then
                alertaMdl("Advertencia", "Seleccione el tipo de meta", "info", "mdlEditarMeta", "contenedor2_cmbTipoMetaMdl")
                Exit Sub
            End If
            If cmbNivelMetaMdl.SelectedIndex = 0 Then
                alertaMdl("Advertencia", "Seleccione la el nivel del la meta", "info", "mdlEditarMeta", "contenedor2_cmbNivelMetaMdl")
                Exit Sub
            End If
            If txtLineaBaseMetaMdl.Text = String.Empty Then
                alertaMdl("Advertencia", "Ingrese la linea base", "info", "mdlEditarMeta", "contenedor2_txtLineaBaseMetaMdl")
                Exit Sub
            End If
            If txtPriYearMetaMdl.Text = String.Empty Then
                alertaMdl("Advertencia", "Ingrese la cantidad para el primer año", "info", "mdlEditarMeta", "contenedor2_txtPriYearMetaMdl")
                Exit Sub
            End If
            If txtSegYearMetaMdl.Text = String.Empty Then
                alertaMdl("Advertencia", "Ingrese la cantidad para el segundo año", "info", "mdlEditarMeta", "contenedor2_txtSegYearMetaMdl")
                Exit Sub
            End If
            If txtTercYearMetaMdl.Text = String.Empty Then
                alertaMdl("Advertencia", "Ingrese la cantidad para el tercer año", "info", "mdlEditarMeta", "contenedor2_txtTercYearMetaMdl")
                Exit Sub
            End If
            If txtCuartYearMetaMdl.Text = String.Empty Then
                alertaMdl("Advertencia", "Ingrese la cantidad para el cuarto año", "info", "mdlEditarMeta", "contenedor2_txtPCuartYearMetaMdl")
                Exit Sub
            End If
            If cmbResponsableMdl.SelectedIndex = 0 Then
                alertaMdl("Advertencia", "Seleccione el responsable", "info", "contenedor2_cmbResponsableMdl")
                Exit Sub
            End If
            If cmbAlimentadorMdl.SelectedIndex = 0 Then
                alertaMdl("Advertencia", "Seleccione el alimentador", "info", "contenedor2_cmbAlimentadorMdl")
                Exit Sub
            End If

            If parametrizacion.updateGoals(lblIdMeta.Text.Trim, txtNombreMetaMdl.Text.Trim, cmbTipoMetaMdl.SelectedValue,
                                           cmbNivelMetaMdl.SelectedValue, txtLineaBaseMetaMdl.Text.Trim, txtPriYearMetaMdl.Text.Trim,
                                           txtSegYearMetaMdl.Text.Trim, txtTercYearMetaMdl.Text.Trim, txtCuartYearMetaMdl.Text.Trim,
                                           cmbResponsableMdl.SelectedValue, cmbAlimentadorMdl.SelectedValue) > 0 Then

                alerta("Se ha actualizado correctamente la meta", "", "success", "")
                limpiarMetas()
            Else
                alerta("Advertencia", "Se genero un error al actualizar", "error", "")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub


    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Try
            DataT = Nothing
            Dim code, level_id As String
            If cmbLineas.SelectedIndex > 0 Then
                level_id = "1"
                If cmbNiv2.SelectedIndex > 0 Then
                    level_id = "2"
                    If cmbNiv3.SelectedIndex > 0 Then
                        level_id = "3"
                        If cmbNiv4.SelectedIndex > 0 Then
                            level_id = "4"
                            If cmbNiv5.SelectedIndex > 0 Then
                                level_id = "5"
                                code = cmbNiv5.SelectedValue
                            Else
                                level_id = "5"
                                code = cmbNiv4.SelectedValue
                            End If
                        Else
                            level_id = "4"
                            code = cmbNiv3.SelectedValue
                        End If
                    Else
                        level_id = "3"
                        code = cmbNiv2.SelectedValue
                    End If
                Else
                    level_id = "2"
                    code = cmbLineas.SelectedValue
                End If
            Else
                level_id = "1"
                code = String.Empty
            End If

            DataT = parametrizacion.selectContentsFiltro(lblPac.Text.Trim, code, level_id)
            If DataT.Rows.Count > 0 Then
                tblPlanAccion.DataSource = DataT
                tblPlanAccion.DataBind()
                tblPlanAccion.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                alerta("No se han encontraron registros", "", "info")
                tblPlanAccion.DataSource = Nothing
                tblPlanAccion.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub btnConsultarMeta_Click(sender As Object, e As EventArgs) Handles btnConsultarMeta.Click
        Try
            DataT = Nothing
            Dim code As String

            If cmbLineasMeta.SelectedIndex > 0 Then
                If cmbNiv2Meta.SelectedIndex > 0 Then
                    If cmbNiv3Meta.SelectedIndex > 0 Then
                        If cmbNiv4Meta.SelectedIndex > 0 Then
                            code = cmbNiv4Meta.SelectedValue
                        Else
                            code = cmbNiv3Meta.SelectedValue
                        End If
                    Else
                        code = cmbNiv2Meta.SelectedValue
                    End If
                Else
                    code = cmbLineasMeta.SelectedValue
                End If
            Else
                code = String.Empty
            End If

            DataT = parametrizacion.selectGoalsFiltro(lblPac.Text.Trim, code)
            If DataT.Rows.Count > 0 Then
                tblMetas.DataSource = DataT
                tblMetas.DataBind()
                tblMetas.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                alerta("No se han encontraron registros", "", "info")
                tblMetas.DataSource = Nothing
                tblMetas.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Try
            pnlNuevoJerarquia.Visible = True
            btnNuevo.Visible = False
            pnlFiltro.Visible = False
            btnFiltro.Visible = True
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub btnNuevoMeta_Click(sender As Object, e As EventArgs) Handles btnNuevoMeta.Click
        Try
            pnlMetaNuevo.Visible = True
            btnNuevoMeta.Visible = False
            pnlFiltroMeta.Visible = False
            btnFiltroMeta.Visible = True
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            pnlNuevoJerarquia.Visible = False
            btnNuevo.Visible = True
            pnlFiltro.Visible = True
            btnFiltro.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnCancelarMeta_Click(sender As Object, e As EventArgs) Handles btnCancelarMeta.Click
        Try
            pnlMetaNuevo.Visible = False
            btnNuevoMeta.Visible = True
            pnlFiltroMeta.Visible = True
            btnFiltroMeta.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnFiltro_Click(sender As Object, e As EventArgs) Handles btnFiltro.Click
        Try
            pnlNuevoJerarquia.Visible = False
            btnNuevo.Visible = True
            pnlFiltro.Visible = True
            btnFiltro.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnFiltroMeta_Click(sender As Object, e As EventArgs) Handles btnFiltroMeta.Click
        Try
            pnlMetaNuevo.Visible = False
            btnNuevoMeta.Visible = True
            pnlFiltroMeta.Visible = True
            btnFiltroMeta.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub


#End Region

#Region "Metodos - Funciones"
    Public Sub visualizarPac(ByVal pac As String)
        Try
            If pac <> String.Empty Then
                Fila = Nothing
                Fila = parametrizacion.selectPac(pac)
                If Fila IsNot Nothing Then
                    lblTituloForm.Text = "EDITAR PAC"
                    lblPac.Text = Fila("id")
                    txtNomPac.Text = Fila("name")
                    txtYearInicial.Text = Fila("initial_year")
                    txtCantYears.Text = Fila("number_years")
                    txtYearFinal.Text = Fila("final_year")

                    btnActPac.Visible = True
                    btnSigPac.Visible = False

                    cargarNiveles(lblPac.Text.Trim)
                    'cargarPlanAccion(lblPac.Text.Trim)
                    cargarMetas(lblPac.Text.Trim, 1)
                End If
            Else
                lblTituloForm.Text = "CREACIÓN DEL PAC"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Public Sub cargarNiveles(ByVal pac As String)
        Try
            DataT = parametrizacion.selectLevels(pac, "hierarchy")
            If DataT.Rows.Count > 0 Then
                tblNiveles.DataSource = DataT
                tblNiveles.DataBind()
                tblNiveles.UseAccessibleHeader = True
                tblNiveles.HeaderRow.TableSection = TableRowSection.TableHeader

                Session("dtNiveles") = DataT
                cmbNiveles.Items.Clear()
                cmbNiveles.DataTextField = "name"
                cmbNiveles.DataValueField = "hierarchy"
                cmbNiveles.DataSource = DataT
                cmbNiveles.DataBind()
                cmbNiveles.Items.Insert(0, New ListItem("---Seleccione---", ""))
                If DataT(0)(3) = "1" Then
                    lblLineas.Text = DataT(0)(1)
                    lblLineasMeta.Text = DataT(0)(1)
                Else
                    lblLineas.Text = "No hay lineas"
                    lblLineasMeta.Text = "No hay lineas"
                End If


            Else
                    tblNiveles.DataSource = Nothing
                tblNiveles.DataBind()
                lblLineas.Text = "No hay lineas"
                lblLineasMeta.Text = "No hay lineas"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Public Sub cargarPlanAccion(ByVal pac As String)
        Try

            DataT = Nothing
            DataT = parametrizacion.selectContents(pac)
            If DataT.Rows.Count > 0 Then
                tblPlanAccion.DataSource = DataT
                tblPlanAccion.DataBind()
                tblPlanAccion.UseAccessibleHeader = True
                tblPlanAccion.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                tblPlanAccion.DataSource = Nothing
                tblPlanAccion.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Public Sub cargarMetas(ByVal pac As String, ByVal proceso As Integer)
        Try
            If pac <> String.Empty Then
                If proceso = 1 Then
                    Dim codLevel As String
                    DataT = Nothing
                    DataT = parametrizacion.selectLevels(pac, "hierarchy desc")
                    If DataT.Rows.Count > 0 Then
                        codLevel = DataT(0)(3)
                        lblNivelMeta.Text = DataT(0)(1)
                        lblNivelMetaMdl.Text = DataT(0)(1)
                    Else
                        codLevel = "0"
                        lblNivelMeta.Text = "No hay niveles"
                        lblNivelMetaMdl.Text = "No hay niveles"
                    End If

                    DataT = Nothing
                    DataT = parametrizacion.selectContents(pac, codLevel, , )
                    If DataT.Rows.Count > 0 Then
                        cmbNivelMeta.Items.Clear()
                        cmbNivelMeta.DataTextField = "name"
                        cmbNivelMeta.DataValueField = "code"
                        cmbNivelMeta.DataSource = DataT
                        cmbNivelMeta.DataBind()
                        cmbNivelMeta.Items.Insert(0, New ListItem("---Seleccione---", ""))

                        cmbNivelMetaMdl.Items.Clear()
                        cmbNivelMetaMdl.DataTextField = "name"
                        cmbNivelMetaMdl.DataValueField = "code"
                        cmbNivelMetaMdl.DataSource = DataT
                        cmbNivelMetaMdl.DataBind()
                        cmbNivelMetaMdl.Items.Insert(0, New ListItem("---Seleccione---", ""))
                    End If
                End If
            Else
                tblMetas.DataSource = Nothing
                tblMetas.DataBind()
            End If

            lblIdMeta.Text = String.Empty
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub eliminarNivel_Click(sender As Object, e As EventArgs) Handles eliminarNivel.Click
        Try
            parametrizacion.deleteLevels(Session("idNivel"), "I")
            Session("idNivel") = Nothing
            cargarNiveles(lblPac.Text.Trim)

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub eliminarPlanAcc_Click(sender As Object, e As EventArgs) Handles eliminarPlanAcc.Click
        Try
            parametrizacion.deleteContents(Session("idPlanAcc"), "I")
            Session("idPlanAcc") = Nothing
            cargarPlanAccion(lblPac.Text.Trim)
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Public Sub alerta(ByVal mensaje As String, ByVal subMensaje As String, ByVal tipo As String, Optional foco As String = "")
        Dim Script As String = "<script type='text/javascript'> swal({title:'" + mensaje.Replace("'", " | ") + "', text:'" + subMensaje.Replace("'", " | ") + "' , type:'" + tipo + "', confirmButtonText:'OK'})"
        If foco.Trim <> "" Then
            Script &= ".then((result) => {if (result.value) {document.getElementById('" + foco + "').focus();}});"
        End If
        Script &= " </script>"
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "swal", Script, False)
    End Sub

    Public Sub alertaMdl(ByVal mensaje As String, ByVal subMensaje As String, ByVal tipo As String, ByVal modal As String, Optional foco As String = "")
        Dim Script As String = "<script type='text/javascript'> swal({title:'" + mensaje.Replace("'", " | ") + "', text:'" + subMensaje.Replace("'", " | ") + "' , type:'" + tipo + "', confirmButtonText:'OK'})"
        If foco.Trim <> "" Then
            Script &= ".then((result) => {if (result.value) {document.getElementById('" + foco + "').focus();}});"
        End If
        Script &= " $(window).on('load', function () {
                        $('#" & modal & "').modal('show');
                    });"
        Script &= " </script>"
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "swal", Script, False)
    End Sub

    Sub pestaña(index As Integer)
        btnPac.Attributes.Add("class", "")
        btnNiveles.Attributes.Add("class", "")
        btnPlanAccion.Attributes.Add("class", "")
        btnMetas.Attributes.Add("class", "")

        Select Case index
            Case 1
                btnPac.Attributes.Add("class", "nav-link active")
                btnNiveles.Attributes.Add("class", "nav-link")
                btnPlanAccion.Attributes.Add("class", "nav-link")
                btnMetas.Attributes.Add("class", "nav-link")
                lblSubTitulo.Text = "Editar información general"
            Case 2
                btnNiveles.Attributes.Add("class", "nav-link active")
                btnPac.Attributes.Add("class", "nav-link")
                btnPlanAccion.Attributes.Add("class", "nav-link")
                btnMetas.Attributes.Add("class", "nav-link")
                lblSubTitulo.Text = "Editar niveles"
            Case 3
                btnPlanAccion.Attributes.Add("class", "nav-link active")
                btnPac.Attributes.Add("class", "nav-link")
                btnNiveles.Attributes.Add("class", "nav-link")
                btnMetas.Attributes.Add("class", "nav-link")
                lblSubTitulo.Text = "Editar contenido"
            Case 4
                btnMetas.Attributes.Add("class", "nav-link active")
                btnPac.Attributes.Add("class", "nav-link")
                btnNiveles.Attributes.Add("class", "nav-link")
                btnPlanAccion.Attributes.Add("class", "nav-link")
                lblSubTitulo.Text = "Editar metas"
        End Select
    End Sub
    Public Sub cargarLineas()
        Try
            Fila = Nothing
            Fila = parametrizacion.selectPacActivo
            If Fila IsNot Nothing Then
                'pac.Text = Fila("id")
                DataT = Nothing
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim)
                If DataT.Rows.Count > 0 Then
                    cmbLineas.Items.Clear()
                    cmbLineas.DataTextField = "name"
                    cmbLineas.DataValueField = "code"
                    cmbLineas.DataSource = DataT
                    cmbLineas.DataBind()
                    cmbLineas.Items.Insert(0, New ListItem("---Seleccione---", ""))
                    cmbLineasMeta.Items.Clear()
                    cmbLineasMeta.DataTextField = "name"
                    cmbLineasMeta.DataValueField = "code"
                    cmbLineasMeta.DataSource = DataT
                    cmbLineasMeta.DataBind()
                    cmbLineasMeta.Items.Insert(0, New ListItem("---Seleccione---", ""))
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Public Sub limpiarForm()
        Try
            lblPac.Text = String.Empty
            txtNomPac.Text = String.Empty
            txtYearInicial.Text = String.Empty
            txtCantYears.Text = String.Empty
            txtYearFinal.Text = String.Empty

            txtNombreNiv.Text = String.Empty
            tblNiveles.DataSource = Nothing
            tblNiveles.DataBind()

            cmbNiveles.Items.Clear()
            cmbSubNivel.Items.Clear()
            txtNombrePlanAcc.Text = String.Empty
            txtPesoPlanAcc.Text = String.Empty
            tblPlanAccion.DataSource = Nothing
            tblPlanAccion.DataBind()

            pnlPac.Visible = True
            pestaña(1)
            pnlNiveles.Visible = False
            pnlPlanAccion.Visible = False
            lblError.Text = String.Empty
            lblError.Visible = False

            pnlSubNivel.Visible = False

            limpiarMetas()

            Session("niveles") = Nothing
            Session("pac") = Nothing
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Public Sub limpiarMetas()
        lblIdMeta.Text = String.Empty
        txtNombreMeta.Text = String.Empty
        cmbTipoMeta.SelectedIndex = 0
        cmbNivelMeta.SelectedIndex = 0
        txtLineaBaseMeta.Text = String.Empty
        txtPriYearMeta.Text = String.Empty
        txtSegYearMeta.Text = String.Empty
        txtTerYearMeta.Text = String.Empty
        txtCuaYearMeta.Text = String.Empty
        cmbResponsable.SelectedIndex = 0
        cmbAlimentador.SelectedIndex = 0
        'cargarMetas(lblPac.Text.Trim, 0)
    End Sub

    Public Sub calcularYearFinal()
        Try
            Dim yearInicial As Integer = 0
            Dim cantidadYears As Integer = 0

            If txtYearInicial.Text <> String.Empty Then yearInicial = CInt(txtYearInicial.Text.Trim)
            If txtCantYears.Text <> String.Empty Then cantidadYears = CInt(txtCantYears.Text.Trim)

            txtYearFinal.Text = yearInicial + (cantidadYears - 1)

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub


    Public Sub limiarFiltroRegistro()
        pnlNvl1Reg.Visible = False
        pnlNvl2Reg.Visible = False
        pnlNvl3Reg.Visible = False
        pnlNvl4Reg.Visible = False
        pnlNvl5Reg.Visible = False
        cmbNvl1Reg.Items.Clear()
        cmbNvl2Reg.Items.Clear()
        cmbNvl3Reg.Items.Clear()
        cmbNvl4Reg.Items.Clear()
        cmbNvl5Reg.Items.Clear()
    End Sub

#End Region

End Class