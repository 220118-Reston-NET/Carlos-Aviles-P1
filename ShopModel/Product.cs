namespace ShopModel
{

    /*
     * Represents an item a store is selling.
     */
    public class Product
    {

        /* The unique identification of this product. */
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /* The name of the product. */
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        /* The price of the product being sold. */
        private double _price;
        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        
        /* The description of the product. */
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /* The type of product. */
        private string _category;
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        /* The age limit to purchase this item, for legal reasons. */
        private int _minimumAge;
        public int MinimumAge
        {
            get { return _minimumAge; }
            set { _minimumAge = value; }
        }
    }
}