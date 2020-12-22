CREATE PROCEDURE [dbo].[GetBankStatistics] 
	@bankId Integer,
	@userName varChar(50)
AS

SET NOCOUNT ON;
---------------------------------------------------
----------------------ITEMS------------------------
---------------------------------------------------

---(0)---Total number of items 
SELECT     COUNT(*) AS totalNumberOfItems
FROM         dbo.ItemResource INNER JOIN
                      dbo.Resource ON dbo.ItemResource.resourceId = dbo.Resource.resourceId
GROUP BY dbo.Resource.bankId
HAVING      (dbo.Resource.bankId = @bankId)

---(1)---number of items created by me
SELECT     COUNT(*) AS numberOfItemsCreatedByMe
FROM         dbo.ItemResource INNER JOIN
                      dbo.Resource ON dbo.ItemResource.resourceId = dbo.Resource.resourceId INNER JOIN
                      dbo.[User] ON dbo.Resource.createdBy = dbo.[User].id
GROUP BY dbo.Resource.bankId, dbo.[User].userName
HAVING      (dbo.Resource.bankId = @bankId) AND (dbo.[User].userName = @userName)

---(2)---unused  items
SELECT     COUNT(*) AS numberOfUnusedItems
FROM         ItemResource INNER JOIN
                      Resource ON ItemResource.resourceId = Resource.resourceId LEFT OUTER JOIN
                      DependentResource ON ItemResource.resourceId = DependentResource.dependentResourceId
GROUP BY DependentResource.dependentResourceId, Resource.bankId
HAVING      (DependentResource.dependentResourceId IS NULL) AND (Resource.bankId =  @bankId)


---(3)---top 5 new/modified items
SELECT     TOP 32 Resource.resourceId, Resource.name, Resource.title, Resource.modifiedDate, [User].fullName
FROM         ItemResource INNER JOIN
                      Resource ON ItemResource.resourceId = Resource.resourceId INNER JOIN
                      [User] ON Resource.modifiedBy = [User].id
WHERE     (Resource.bankId = @bankId)
ORDER BY Resource.modifiedDate DESC

---------------------------------------------------
----------------------TEST-------------------------
---------------------------------------------------

---(4)---Total number of tests
SELECT     COUNT(*) AS totalNumberOfTest
FROM         dbo.AssessmentTestResource INNER JOIN
                      dbo.Resource ON dbo.AssessmentTestResource.resourceId = dbo.Resource.resourceId
GROUP BY dbo.Resource.bankId, isTemplate
HAVING      (dbo.Resource.bankId = @bankId AND isTemplate=0)

---(5)---number of tests created by me
SELECT     COUNT(*) AS numberOfTestCreatedByMe
FROM         dbo.AssessmentTestResource INNER JOIN
                      dbo.Resource ON dbo.AssessmentTestResource.resourceId = dbo.Resource.resourceId INNER JOIN
                      dbo.[User] ON dbo.Resource.createdBy = dbo.[User].id
GROUP BY dbo.Resource.bankId, dbo.[User].userName, dbo.AssessmentTestResource.isTemplate
HAVING      (dbo.Resource.bankId = @bankId) AND (dbo.[User].userName = @userName) AND (dbo.AssessmentTestResource.isTemplate = 0)

---------------------------------------------------
----------------------MEDIA------------------------
---------------------------------------------------

---(6)---Total Number of media
SELECT     COUNT(*) AS totalNumberOfMedia
FROM         dbo.Resource INNER JOIN
                      dbo.GenericResource ON dbo.Resource.resourceId = dbo.GenericResource.resourceId
GROUP BY dbo.Resource.bankId
HAVING      (dbo.Resource.bankId = @bankId)

---(7)---unused  media
SELECT     COUNT(*) AS numberOfUnusedMedia
FROM         GenericResource INNER JOIN
                      Resource ON GenericResource.resourceId = Resource.resourceId LEFT OUTER JOIN
                      DependentResource ON GenericResource.resourceId = DependentResource.dependentResourceId
GROUP BY DependentResource.dependentResourceId, Resource.bankId
HAVING      (DependentResource.dependentResourceId IS NULL) AND (Resource.bankId =  @bankId)



---------------------------------------------------
-------------------ITEM_TEMPLATES------------------
---------------------------------------------------

---(8)---Total number of itemsTemplates
SELECT     COUNT(*) AS totalNumberOfItemTemplates
FROM         dbo.ItemLayoutTemplateResource INNER JOIN
                      dbo.Resource ON dbo.ItemLayoutTemplateResource.resourceId = dbo.Resource.resourceId
GROUP BY dbo.Resource.bankId
HAVING      (dbo.Resource.bankId = @bankId)



---(9)---unused  items Templates
SELECT     COUNT(*) AS numberOfUnusedItemsTemplates
FROM         ItemLayoutTemplateResource INNER JOIN
                      Resource ON ItemLayoutTemplateResource.resourceId = Resource.resourceId LEFT OUTER JOIN
                      DependentResource ON ItemLayoutTemplateResource.resourceId = DependentResource.dependentResourceId
GROUP BY DependentResource.dependentResourceId, Resource.bankId
HAVING      (DependentResource.dependentResourceId IS NULL) AND (Resource.bankId =  @bankId)

---(10)---items Templates created by me
SELECT     COUNT(*) AS numberOfItemTemplatesCreatedByMe
FROM         dbo.ItemLayoutTemplateResource INNER JOIN
                      dbo.Resource ON dbo.ItemLayoutTemplateResource.resourceId = dbo.Resource.resourceId INNER JOIN
                      dbo.[User] ON dbo.Resource.createdBy = dbo.[User].id
GROUP BY dbo.Resource.bankId, dbo.[User].userName
HAVING      (dbo.Resource.bankId = @bankId) AND (dbo.[User].userName = @userName)

---------------------------------------------------
-------------------TEST_TEMPLATES------------------
---------------------------------------------------

---(11)---Total number of testTemplates
SELECT     COUNT(*) AS totalNumberOfTestTemplates
FROM         dbo.AssessmentTestResource INNER JOIN
                      dbo.Resource ON dbo.AssessmentTestResource.resourceId = dbo.Resource.resourceId
GROUP BY dbo.Resource.bankId, isTemplate
HAVING      (dbo.Resource.bankId = @bankId AND isTemplate=1)

---(12)---number of testTemplates created by me
SELECT     COUNT(*) AS numberOfTestTemplatesCreatedByMe
FROM         dbo.AssessmentTestResource INNER JOIN
                      dbo.Resource ON dbo.AssessmentTestResource.resourceId = dbo.Resource.resourceId INNER JOIN
                      dbo.[User] ON dbo.Resource.createdBy = dbo.[User].id
GROUP BY dbo.Resource.bankId, dbo.[User].userName, dbo.AssessmentTestResource.isTemplate
HAVING      (dbo.Resource.bankId = @bankId) AND (dbo.[User].userName = @userName) AND (dbo.AssessmentTestResource.isTemplate = 1)

---------------------------------------------------
----------------CONTROL_TEMPLATES------------------
---------------------------------------------------

---(13)---Total number of controlTemplates
SELECT     COUNT(*) AS totalNumberOfControlTemplates
FROM         dbo.ControlTemplateResource INNER JOIN
                      dbo.Resource ON dbo.ControlTemplateResource.resourceId = dbo.Resource.resourceId
GROUP BY dbo.Resource.bankId
HAVING      (dbo.Resource.bankId = @bankId)


---(14)---orphaned  control Templates
SELECT     COUNT(*) AS numberOfUnusedControlTemplates
FROM         ControlTemplateResource INNER JOIN
                      Resource ON ControlTemplateResource.resourceId = Resource.resourceId LEFT OUTER JOIN
                      DependentResource ON ControlTemplateResource.resourceId = DependentResource.dependentResourceId
GROUP BY DependentResource.dependentResourceId, Resource.bankId
HAVING      (DependentResource.dependentResourceId IS NULL) AND (Resource.bankId =  @bankId)


---(15)---number of control templates created by me
SELECT     COUNT(*) AS numberOfControlTemplates
FROM         dbo.ControlTemplateResource INNER JOIN
                      dbo.Resource ON dbo.ControlTemplateResource.resourceId = dbo.Resource.resourceId INNER JOIN
                      dbo.[User] ON dbo.Resource.createdBy = dbo.[User].id
GROUP BY dbo.Resource.bankId, dbo.[User].userName
HAVING      (dbo.Resource.bankId = @bankId) AND (dbo.[User].userName = @userName)

---------------------------------------------------
--------------------OTHER_INFORMATION--------------
---------------------------------------------------

---(20)---state information.
SELECT     COUNT(*) AS numberOfItem, dbo.State.name
FROM         dbo.ItemResource INNER JOIN
                      dbo.Resource ON dbo.ItemResource.resourceId = dbo.Resource.resourceId INNER JOIN
                      dbo.State ON dbo.Resource.stateId = dbo.State.stateId
GROUP BY dbo.Resource.bankId, dbo.State.name, dbo.State.stateId
HAVING      (dbo.Resource.bankId = @bankId)