namespace ShopUI
{
    /*
     * A basic abstract interface that the UI will use to show menus.
     */
    public interface MenuInterface
    {
        /// <summary>
        /// Print basic menu options.
        /// </summary>
        void Print();

        /// <summary>
        /// Grab the user's keyboard input.
        /// </summary>
        /// <returns>The user's input event.</returns>
        MenuType UserInput();
    }
}