namespace Questify.Builder.Logic.Service.Model
{
    public enum ResourceEntityType
    {
        Item,
        Test,
        Selection,
        TestPackage,
        Aspect,
        Media,
        CustomProperty,
        ItemLayoutTemplate,
        SelectionTemplate,
        TestTemplate,
        ControlTemplate,
        Delivery,
        Bank
    };

    public enum CustomPropertyType
    {
        Free,
        ListSingle,
        ListMultiple,
        Concept,
        Tree,
        FreeRichText
    };
}
