
Option Strict On
Option Explicit On


Namespace My.Resources

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"), _
Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), _
Global.Microsoft.VisualBasic.HideModuleNameAttribute()> _
    Friend Module Resources

        Private resourceMan As Global.System.Resources.ResourceManager

        Private resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Questify.Builder.Plugins.DataSource.StaticGroups.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property

        Friend ReadOnly Property AddGroupCommand_Name() As String
            Get
                Return ResourceManager.GetString("AddGroupCommand_Name", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property AddItemCommand_Name() As String
            Get
                Return ResourceManager.GetString("AddItemCommand_Name", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property AddItemsFromCodesCommand_Name() As String
            Get
                Return ResourceManager.GetString("AddItemsFromCodesCommand_Name", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property AddItemsFromCodesToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("AddItemsFromCodesToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property AddItemToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("AddItemToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property AreYouSure() As String
            Get
                Return ResourceManager.GetString("AreYouSure", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property AreYouSureGoup() As String
            Get
                Return ResourceManager.GetString("AreYouSureGoup", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property AtLeastOneGroup() As String
            Get
                Return ResourceManager.GetString("AtLeastOneGroup", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CreateGroupCommand_Name() As String
            Get
                Return ResourceManager.GetString("CreateGroupCommand_Name", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DeleteItemToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("DeleteItemToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property Group() As String
            Get
                Return ResourceManager.GetString("Group", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GroupRef() As String
            Get
                Return ResourceManager.GetString("GroupRef", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemRef() As String
            Get
                Return ResourceManager.GetString("ItemRef", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property MoveDownCommand_Name() As String
            Get
                Return ResourceManager.GetString("MoveDownCommand_Name", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property MoveDownToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("MoveDownToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property MoveUpCommand_Name() As String
            Get
                Return ResourceManager.GetString("MoveUpCommand_Name", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property MoveUpToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("MoveUpToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property RemoveCommand_Name() As String
            Get
                Return ResourceManager.GetString("RemoveCommand_Name", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property StaticGroup() As String
            Get
                Return ResourceManager.GetString("StaticGroup", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SubGroup_Empty() As String
            Get
                Return ResourceManager.GetString("SubGroup_Empty", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Unknown() As String
            Get
                Return ResourceManager.GetString("Unknown", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
