Imports System
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.HelperClasses
    <Serializable> _
    Public Class TypeDefaultValue
        Implements ITypeDefaultValue

        Public Sub New()
        End Sub


        Public Function DefaultValue(defaultValueType As System.Type) As Object Implements ITypeDefaultValue.DefaultValue
            Return TypeDefaultValue.GetDefaultValue(defaultValueType)
        End Function


        Public Shared Function GetDefaultValue(defaultValueType As System.Type) As Object
            Dim valueToReturn As Object = Nothing

            Select Case Type.GetTypeCode(defaultValueType)
                Case TypeCode.Boolean
                    valueToReturn = False
                Case TypeCode.Byte
                    valueToReturn = CByte(0)
                Case TypeCode.DateTime
                    valueToReturn = DateTime.MinValue
                Case TypeCode.Decimal
                    valueToReturn = 0.0D
                Case TypeCode.Double
                    valueToReturn = 0.0R
                Case TypeCode.Int16
                    valueToReturn = CShort(0)
                Case TypeCode.Int32
                    valueToReturn = CInt(0)
                Case TypeCode.Int64
                    valueToReturn = CLng(0)
                Case TypeCode.Object
                    Select Case defaultValueType.UnderlyingSystemType.FullName
                        Case "System.Byte[]"
                            valueToReturn = New Byte(-1) {}
                        Case "System.Guid"
                            valueToReturn = Guid.Empty
                        case "System.DateTimeOffset"
                            valueToReturn = DateTimeOffset.MinValue
                        case "System.TimeSpan"
                            valueToReturn = TimeSpan.MinValue
                    End Select
                Case TypeCode.String
                    valueToReturn = String.Empty
                Case TypeCode.Single
                    valueToReturn = 0.0F
                case TypeCode.UInt16
                    valueToReturn = 0US
                case TypeCode.UInt32
                    valueToReturn = 0UI
                case TypeCode.UInt64
                    valueToReturn = 0US
                Case Else
            End Select
            Return valueToReturn

        End Function


    End Class
End Namespace

