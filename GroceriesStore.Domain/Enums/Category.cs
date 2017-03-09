using System.ComponentModel;

namespace GroceriesStore.Domain.Enums
{
    public enum Category
    {
        [Description("Canned")]
        Canned = 1,
        [Description("Bakery")]
        Bakery = 2,
        [Description("Beverages")]
        Beverages = 3,
        [Description("Dairy")]
        Dairy = 4,
        [Description("Baking Goods")]
        BakingGoods = 5,
        [Description("Frozen Foods")]
        FrozenFoods = 6,
        [Description("Meat")]
        Meat = 7,
        [Description("Produce")]
        Produce = 8,
        [Description("Cleaners")]
        Cleaners = 9,
        [Description("Paper Goods")]
        PaperGoods = 10,
        [Description("Personal Care")]
        PersonalCare = 11,
        [Description("Other")]
        Other = 12
    }
}
