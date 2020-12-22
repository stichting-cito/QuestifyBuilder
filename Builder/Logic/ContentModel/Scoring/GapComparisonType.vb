Namespace ContentModel.Scoring
    <Flags>
    Public Enum GapComparisonType
        <CasScoringOperator(False)>
        Equals = 2
        <CasScoringOperator(False)>
        Range = 4
        <CasScoringOperator(False)>
        GreaterThan = 8
        <CasScoringOperator(False)>
        SmallerThan = 16
        <CasScoringOperator(False)>
        GreaterThanEquals = 32
        <CasScoringOperator(False)>
        SmallerThanEquals = 64
        <CasScoringOperator(True)>
        Equivalent = 128
        <CasScoringOperator(False)>
        NotEquals = 256
        <CasScoringOperator(True)>
        Evaluate = 512
        <CasScoringOperator(True)>
        Dependency = 1024
        <CasScoringOperator(False)>
        NoValue = 2048
        <CasScoringOperator(False)>
        EqualsSoft = 4096
        <CasScoringOperator(False)>
        EqualEquation = 8192
        <CasScoringOperator(False)>
        EqualsStrict = 16384
    End Enum

End Namespace