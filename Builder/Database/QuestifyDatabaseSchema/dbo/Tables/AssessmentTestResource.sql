CREATE TABLE [dbo].[AssessmentTestResource] (
    [resourceId] UNIQUEIDENTIFIER NOT NULL,
    [isTemplate] BIT              CONSTRAINT [DF_AssessmentTestResource_isTemplate] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AssessmentTestResource] PRIMARY KEY CLUSTERED ([resourceId] ASC) WITH (FILLFACTOR = 85),
    CONSTRAINT [FK_AssessmentTestResource_Resource] FOREIGN KEY ([resourceId]) REFERENCES [dbo].[Resource] ([resourceId]) ON DELETE CASCADE
);

