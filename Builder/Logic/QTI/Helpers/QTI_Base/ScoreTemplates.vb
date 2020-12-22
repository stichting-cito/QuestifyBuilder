Imports System.Xml.Linq
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base

Namespace QTI.Helpers.QTI_Base

    Public Class ScoreTemplates
        Public Enum TemplateType
            None = 0
            MatchCorrect = 1
            MatchResponse = 2
            MapResponsePoint = 3
            NoScore = 4
        End Enum

        Private Shared _responseIdentifer As String = PackageCreatorConstants.RESPONSE
        Private Shared _scoringIdentifier As String = PackageCreatorConstants.SCORE

        Friend Shared Function GetResponseTemplate(templateType As TemplateType) As String
            Dim template As String = String.Empty
            Select Case templateType
                Case ScoreTemplates.TemplateType.MatchCorrect
                    template = GetMatchCorrect()
                Case ScoreTemplates.TemplateType.MatchResponse
                    template = GetMatchResponse()
                Case ScoreTemplates.TemplateType.MapResponsePoint
                    template = GetMapResponsePoint()
                Case ScoreTemplates.TemplateType.NoScore
                    template = GetNoScore()
            End Select
            Return template
        End Function

        Friend Shared Function GetResponseTemplate(templateType As TemplateType, responseIdentifier As String, scoringIdentifier As String) As String
            _responseIdentifer = responseIdentifier
            _scoringIdentifier = scoringIdentifier
            Return GetResponseTemplate(templateType)
        End Function

        Private Shared Function GetMatchCorrect() As String
            Dim matchCorrectTemplate As XElement = <responseCondition>
                                                       <responseIf>
                                                           <match>
                                                               <variable identifier="{0}"/>
                                                               <correct identifier="{0}"/>
                                                           </match>
                                                           <setOutcomeValue identifier="{1}">
                                                               <baseValue baseType="integer">1</baseValue>
                                                           </setOutcomeValue>
                                                       </responseIf>
                                                       <responseElse>
                                                           <setOutcomeValue identifier="{1}">
                                                               <baseValue baseType="integer">0</baseValue>
                                                           </setOutcomeValue>
                                                       </responseElse>
                                                   </responseCondition>

            Return String.Format(matchCorrectTemplate.ToString, _responseIdentifer, _scoringIdentifier)
        End Function

        Private Shared Function GetMatchResponse() As String
            Dim matchResponseTemplate As XElement = <responseCondition>
                                                        <responseIf>
                                                            <isNull>
                                                                <variable identifier="{0}"/>
                                                            </isNull>
                                                            <setOutcomeValue identifier="{1}">
                                                                <baseValue baseType="integer">0</baseValue>
                                                            </setOutcomeValue>
                                                        </responseIf>
                                                        <responseElse>
                                                            <setOutcomeValue identifier="{1}">
                                                                <mapResponse identifier="{0}"/>
                                                            </setOutcomeValue>
                                                        </responseElse>
                                                    </responseCondition>

            Return String.Format(matchResponseTemplate.ToString, _responseIdentifer, _scoringIdentifier)
        End Function


        Private Shared Function GetMapResponsePoint() As String
            Dim mapResponsePointTemplate As XElement = <responseCondition>
                                                           <responseIf>
                                                               <isNull>
                                                                   <variable identifier="{0}"/>
                                                               </isNull>
                                                               <setOutcomeValue identifier="{1}">
                                                                   <baseValue baseType="integer">0</baseValue>
                                                               </setOutcomeValue>
                                                           </responseIf>
                                                           <responseElse>
                                                               <setOutcomeValue identifier="{1}">
                                                                   <mapResponsePoint identifier="{0}"/>
                                                               </setOutcomeValue>
                                                           </responseElse>
                                                       </responseCondition>

            Return String.Format(mapResponsePointTemplate.ToString, _responseIdentifer, _scoringIdentifier)
        End Function

        Private Shared Function GetNoScore() As String
            Dim noScoreTemplate As XElement = <setOutcomeValue identifier="{0}">
                                                  <baseValue baseType="integer">0</baseValue>
                                              </setOutcomeValue>

            Return String.Format(noScoreTemplate.ToString, _scoringIdentifier)
        End Function

    End Class
End Namespace