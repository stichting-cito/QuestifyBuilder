Templates
=========

 

Questify Builder uses templates for item construction. *Item Layout Templates*
(ILTs) structure the general layout of an item. *Control Templates* (CTs) are
part of ILTs and contain logic for specific types of interaction, such as
*Multiple Choice* or *Text*.

Questify Builder comes with a set of sample templates with which you can
construct your own QTI-L1 level tests. This set may serve as a jumping off point
for developers to construct their own templates containing more complex
interactions. The root folder of the application contains a solution called
*QuestifyTemplates.sln*, where the sample templates can be inspected.

 

>   **Note:** Templates created by third parties could contain viruses or
>   otherwise be harmful to your computer. You should only use templates from a
>   trustworthy source!

 

Item Layout Templates
---------------------

An ILT must contain the following elements:

-   *Description*: Description/documentation of the template. This is intended
    for the developer, not the end user.

-   *Targets*

    -   *Name*: The name of the target (e.g. *GenericTest* for generic QTI test
        items, or *Word* for paper based test items to be published in a Word
        format).

    -   *Enabled*: A value indicating whether the target is available.

    -   *Description*: Description/documentation of the template. This is
        intended for the developer, not the end user.

    -   *Template*: Well formed AspV3Style Template.

 

The above hierarchy is shown in the following sample of ILT XML:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
<Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
  <Description>Sample description</Description>
  <Targets>
    <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="GenericTest">
      <Description>Sample description</Description>
      <Template>
        <![CDATA[
          <root xmlns:cito="http://www.cito.nl/citotester">
            <itemBody class="defaultBody">
              <div class="content">
                <cito:control id="Sample" type="Sample" />
              </div>
            </itemBody>
          </root>
        ]]>
      </Template>
    </Target>
  </Targets>
</Template>
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

 

The template in the targets can contain references to CTs. This reference
contains the following information:

-   *Id*: The unique identifier for the CT.

-   *Type*: The resource name of the CT referenced.

-   *Parameter overrides*: zero or more parameter-value-overrides.

 

### Inline multimedia

It is possible to add inline multimedia to an item. For this to work, however, a
third element must be added to an ILT: *Settings*.

 

The set of sample templates contains additional ILTs for adding inline audio,
images or video to an item. These can be added in the *Settings* element of an
item:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
<Settings>
  <DesignerSetting key="inlineAudioTemplate">ILT.Inline.Audio</DesignerSetting>
  <DesignerSetting key="inlineImageTemplate">ILT.Inline.Image</DesignerSetting>
  <DesignerSetting key="inlineVideoTemplate">ILT.Inline.Video</DesignerSetting>
</Settings>
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

 

Control Templates
-----------------

CTs are the smart building blocks used in ILTs. CTs have the following
components:

-   *Description*: Description/documentation of the template. This is intended
    for the developer, not the end user.

-   *Targets*

    -   *Name*: The name of the target (e.g. *GenericTest* for generic QTI test
        items, or *Word* for paper based test items to be published in a Word
        format).

    -   *Enabled*: A value indicating whether the target is available.

    -   *Description*: Description/documentation of the template. This is
        intended for the developer, not the end user.

    -   *Template*: Well formed AspV3Style Template.

    -   *ParameterSet* (optional): A set of parameters specific for this target.
        The parameters are not accessible from scripts of other targets.

-   *SharedFunctions*: Functions shared between the different targets.

-   *SharedParameterSet*: Parameters shared between the different targets.

 

The above hierarchy is shown in the following sample of CT XML:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
<Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
  <Description>Choice template</Description>
  <Targets>
    <Target xsi:type="ControlTemplateTarget" enabled="true" name="GenericTest">
      <Template>
        <![CDATA[
          <div>
            <% #SampleParameter %>
          </div>
        ]]>
      </Template>
    </Target>
  </Targets>
  <SharedFunctions>
    Private Shared Function SampleFunction(ByVal elements() as System.Xml.XmlNode) As Boolean
    End Function
  </SharedFuctions>
  <SharedParameterSet id="">
    <xhtmlparameter name="SampleParameter">
      <designersetting key="label">Sample label</designersetting>
      <designersetting key="description">Sample description</designersetting>
      <designersetting key="required">False</designersetting>
    </xhtmlparameter>
  </SharedParameterSet>
</Template>
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
