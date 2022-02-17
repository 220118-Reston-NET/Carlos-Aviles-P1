namespace ShopUI
{

    /*
     * An enumeration that will host all different type of menu options.
     */
    public enum MenuType
    {
        MainMenu,

        /*
         * The customer's menus.
         */
        CustomerPortal,
        AddCustomer,
        SearchCustomer,

        /*
         * Everything to do with the stores part of the menu.
         */
        Stores,
        ViewStore,
        ViewStoreOptions,
        SelectCustomer,
        BuyProducts,
        SearchStore,
        ReplenishInventory,
        ViewOrderHistory,
        SelectEmployee,
        
        Exit
    }
}