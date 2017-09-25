using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingActivity_TheTravelingSalesperson
{
    public class Salesperson
    {
        #region FIELDS

        private string _firstName;

        private string _lastName;

        private List<string> _citesVisited;

        private WidgetItemStock _item;



        #endregion

        #region PROPERTIES

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public List<string> CitiesVisited
        {
            get { return _citesVisited; }
            set { _citesVisited = value; }
        }

        public WidgetItemStock Item
        {
            get { return _item; }
            set { _item = value; }
        }


        #endregion


        #region CONSTRUCTORS

        public Salesperson()
        {
            _citesVisited = new List<string>();
            _item = new WidgetItemStock();
        }

        public Salesperson(string firstName)
        {
            _firstName = firstName;
            _citesVisited = new List<string>();
            _item = new WidgetItemStock();
        }



        #endregion


        #region METHODS



        #endregion
    }
}
