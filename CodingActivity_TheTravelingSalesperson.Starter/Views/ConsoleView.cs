using System;
using System.Text;

namespace CodingActivity_TheTravelingSalesperson
{
    /// <summary>
    /// Console class for the MVC pattern
    /// </summary>
    public class ConsoleView
    {
        #region FIELDS

        //
        // declare a Salesperson object for the Controller to use
        // Note: There is no need for a Salesperson property given the Controller already 
        //       has access to the same Salesperson object.
        //
        private Salesperson _salesperson;

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// default constructor to create the console view objects
        /// </summary>
        public ConsoleView(Salesperson salesperson)
        {
            _salesperson = salesperson;

            InitializeConsole();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initialize all console settings
        /// </summary>
        private void InitializeConsole()
        {
            ConsoleUtil.WindowTitle = "The Traveling Salesperson";
            ConsoleUtil.HeaderText = "The Traveling Salesperson";
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the Exit prompt on a clean screen
        /// </summary>
        public void DisplayExitPrompt()
        {
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            ConsoleUtil.DisplayMessage("Thank you for using the application. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }


        /// <summary>
        /// display the welcome screen
        /// </summary>
        public void DisplayWelcomeScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("- add welcome message -");
            Console.WriteLine();

            sb.Clear();
            sb.AppendFormat("Your first task will be to set up your account details.");
            ConsoleUtil.DisplayMessage(sb.ToString());

            DisplayContinuePrompt();
        }

        /// <summary>
        /// setup the new salesperson object with the initial data
        /// Note: To maintain the pattern of only the Controller changing the data this method should
        ///       return a Salesperson object with the initial data to the controller. For simplicity in 
        ///       this demo, the ConsoleView object is allowed to access the Salesperson object's properties.
        /// </summary>
        public void DisplaySetupAccount()
        {
            ConsoleUtil.HeaderText = "Account Setup";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Setup your account now.");
            Console.WriteLine();

            ConsoleUtil.DisplayPromptMessage("Enter your first name: ");
            _salesperson.FirstName = Console.ReadLine();
            Console.WriteLine();

            ConsoleUtil.DisplayPromptMessage("Enter your last name: ");
            _salesperson.LastName = Console.ReadLine();
            Console.WriteLine();

            ConsoleUtil.DisplayPromptMessage("Enter the type of the item: ");
            Console.WriteLine();
            foreach (WidgetItemStock.WidgetType type in Enum.GetValues(typeof(WidgetItemStock.WidgetType)))
            {
                ConsoleUtil.DisplayMessage(type.ToString());
            }
            // complicated enum validation
            WidgetItemStock.WidgetType itemType;
            Enum.TryParse<WidgetItemStock.WidgetType>(Console.ReadLine(), out itemType);
            _salesperson.Item.Type = itemType;

			int numberOfItem;
			//int response;
            ConsoleUtil.DisplayPromptMessage($"Enter the number of {itemType} currently in stock: ");

			bool validResponse = int.TryParse(Console.ReadLine(), out numberOfItem);            
			while (!validResponse) {
				ConsoleUtil.DisplayMessage ("You didn't enter a number. Please try again.");
				Console.WriteLine ();

				ConsoleUtil.DisplayPromptMessage($"Enter the number of {itemType} currently in stock: ");
				validResponse = int.TryParse(Console.ReadLine(), out numberOfItem);
			}

			_salesperson.Item.AddWidgets(numberOfItem);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a closing screen when the user quits the application
        /// </summary>
        public void DisplayClosingScreen()
        {
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Thank you for using The Traveling Salesperson!");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// get the menu choice from the user
        /// </summary>
        public MenuOption DisplayGetUserMenuChoice()
        {
            MenuOption userMenuChoice = MenuOption.None;
            bool usingMenu = true;

            //
            // TODO enable each application function separately and test
            //
            while (usingMenu)
            {
                //
                // set up display area
                //
                ConsoleUtil.DisplayReset();
                Console.CursorVisible = false;

                //
                // display the menu
                //
                ConsoleUtil.DisplayMessage("Please type the number of your menu choice.");
                Console.WriteLine();
                Console.WriteLine(
                    "\t" + "1. Travel" + Environment.NewLine +
                    "\t" + "2. Buy" + Environment.NewLine +
                    "\t" + "3. Sell" + Environment.NewLine +
                    "\t" + "4. Display Inventory" + Environment.NewLine +
                    "\t" + "5. Display Cities" + Environment.NewLine +
                    "\t" + "6. Display Account Info" + Environment.NewLine +
					"\t" + "7. Update Account Info" + Environment.NewLine +
                    "\t" + "E. Exit" + Environment.NewLine);

                //
                // get and process the user's response
                // note: ReadKey argument set to "true" disables the echoing of the key press
                //
                ConsoleKeyInfo userResponse = Console.ReadKey(true);
                switch (userResponse.KeyChar)
                {
                    case '1':
                        userMenuChoice = MenuOption.Travel;
                        usingMenu = false;
                        break;
                    case '2':
                        userMenuChoice = MenuOption.Buy;
                        usingMenu = false;
                        break;
                    case '3':
                        userMenuChoice = MenuOption.Sell;
                        usingMenu = false;
                        break;
                    case '4':
                        userMenuChoice = MenuOption.DisplayInventory;
                        usingMenu = false;
                        break;
                    case '5':
                        userMenuChoice = MenuOption.DisplayCities;
                        usingMenu = false;
                        break;
                    case '6':
                        userMenuChoice = MenuOption.DisplayAccountInfo;
                        usingMenu = false;
                        break;
					case '7':
						userMenuChoice = MenuOption.UpdateAccountInfo;
						usingMenu = false;
						break;
                    case 'E':
                    case 'e':
                        userMenuChoice = MenuOption.Exit;
                        usingMenu = false;
                        break;
					default:
						ConsoleUtil.DisplayMessage ("You didn't choose an apporpirate menu choice. Please try again.");
						DisplayContinuePrompt();
						DisplayGetUserMenuChoice();
						break;
                }
            }
            Console.CursorVisible = true;

            return userMenuChoice;
        }
        /// <summary>
        /// get the next city to travel to from the user
        /// </summary>
        /// <returns>string City</returns>
        public string DisplayGetNextCity()
        {
            string nextCity = "";

            ConsoleUtil.HeaderText = "Next City of Travel";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayPromptMessage("Enter the city you wish to travel to: ");
            nextCity = Console.ReadLine();

            _salesperson.CitiesVisited.Add(nextCity);

            return nextCity;
        }

        /// <summary>
        /// get the number of widget units to buy from the user
        /// </summary>
        /// <returns>int number of units to buy</returns>
        public int DisplayGetNumberOfUnitsToBuy()
        {
            int numberOfUnitsToAdd = 0;       

            ConsoleUtil.HeaderText = "Buy Inventory";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayPromptMessage($"How many {_salesperson.Item.Type} do you want to buy? ");
            bool validResponse = int.TryParse(Console.ReadLine(), out numberOfUnitsToAdd);

            // validation loop, created by Velis
            while (!validResponse)
            {
                ConsoleUtil.DisplayMessage($"You didn't enter how many {_salesperson.Item.Type} you wanted to buy. Please try again.");
                ConsoleUtil.DisplayMessage("");

                ConsoleUtil.DisplayPromptMessage($"How many {_salesperson.Item.Type} do you want to buy? ");
                validResponse = int.TryParse(Console.ReadLine(), out numberOfUnitsToAdd);
            }

            return numberOfUnitsToAdd;
        }

        /// <summary>
        /// get the number of widget units to sell from the user
        /// </summary>
        /// <returns>int number of units to buy</returns>
        public int DisplayGetNumberOfUnitsToSell()
        {
            int numberOfUnitsToSell = 0;

            ConsoleUtil.HeaderText = "Sell Inventory";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayPromptMessage($"How many {_salesperson.Item.Type} do you want to sell? ");
            bool validResponse = int.TryParse(Console.ReadLine(), out numberOfUnitsToSell);

            while (!validResponse)
            {
                ConsoleUtil.DisplayMessage($"You didn't enter how many {_salesperson.Item.Type} you wanted to sell. Please try again.");
                ConsoleUtil.DisplayMessage("");

                ConsoleUtil.DisplayPromptMessage($"How many {_salesperson.Item.Type} do you want to sell? ");
                validResponse = int.TryParse(Console.ReadLine(), out numberOfUnitsToSell);
            }

            if (_salesperson.Item.NumberOfUnits < numberOfUnitsToSell)
            {
				ConsoleUtil.DisplayMessage($"You don't have enough {_salesperson.Item.Type} to sell! Please select fewer {_salesperson.Item.Type} to sell.");
                ConsoleUtil.DisplayMessage("");

                ConsoleUtil.DisplayPromptMessage($"How many {_salesperson.Item.Type} do you want to sell? ");
                validResponse = int.TryParse(Console.ReadLine(), out numberOfUnitsToSell);
            }

            DisplayContinuePrompt();

            return numberOfUnitsToSell;
        }

        /// <summary>
        /// display the current inventory
        /// </summary>
        public void DisplayInventory()
        {
            ConsoleUtil.HeaderText = "Current Inventory";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Item Type: " + _salesperson.Item.Type);
            ConsoleUtil.DisplayMessage("Number of units: " + _salesperson.Item.NumberOfUnits);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a list of the cities traveled
        /// </summary>
        public void DisplayCitiesTraveled()
        {
            ConsoleUtil.HeaderText = "Cities Traveled To";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Cities you have traveled to: ");

			// i don't know _why_ this is happening as i've got two entries of the same city...
            foreach (string cities in _salesperson.CitiesVisited)
            {
                ConsoleUtil.DisplayMessage(cities);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the current account information
        /// </summary>
        public void DisplayAccountInfo()
        {
            ConsoleUtil.HeaderText = "Account Info";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("First Name: " + _salesperson.FirstName);
            ConsoleUtil.DisplayMessage("Last Name: " + _salesperson.LastName);

            ConsoleUtil.DisplayMessage("Item Type: " + _salesperson.Item.Type);
            ConsoleUtil.DisplayMessage("Number of units: " + _salesperson.Item.NumberOfUnits);

            DisplayContinuePrompt();
        }

		/// <summary>
		/// displays updating your account
		/// </summary>
		public void DisplayUpdateAccount()
		{
			ConsoleUtil.HeaderText = "Account Update";
			ConsoleUtil.DisplayReset();

			ConsoleUtil.DisplayMessage("Update your account now.");
			Console.WriteLine();

			ConsoleUtil.DisplayPromptMessage("Enter your first name: ");
			_salesperson.FirstName = Console.ReadLine();
			Console.WriteLine();

			ConsoleUtil.DisplayPromptMessage("Enter your last name: ");
			_salesperson.LastName = Console.ReadLine();
			Console.WriteLine();

			DisplayContinuePrompt();
		}

        #endregion
    }
}
