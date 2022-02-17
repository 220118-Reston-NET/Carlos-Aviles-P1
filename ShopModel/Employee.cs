﻿namespace ShopModel
{

    /*
     * Represents a employee to the store.
     */
    public class Employee
    {

        /* The unique identification of this employee. */
        private int _id;
        public int Id {
            get { return _id; }
            set { _id = value; }
        }

        /* The full name of this employee. */
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /* The username of this employee. */
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        /* The password of this employee. */
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// Instantiates a new employee instance.
        /// </summary>
        public Employee()
        {
            Name = "John Doe";
            Username = "";
            Password = "";
        }
    }
}