using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjct4app
{
    interface ReturnListResult<T>
    {
        void AddToResultList(T data);
        Option<Iterator<T>> GetList();
    }

    class ReturnIntResult : ReturnListResult<int>
    {
        public ListIterator<int> datalijst = new ListIterator<int>();
        public void AddToResultList(int data)
        {
            datalijst.Add(data);
        }

        public Option<Iterator<int>> GetList()
        {
            if (datalijst.source.Count > 0)
            {
                return new Some<Iterator<int>>(datalijst);
            }

            else
            {
                return new None<Iterator<int>>();
            }
        }
    }
    class ReturnListApiResult : ReturnListResult<WebServiceDetails.Result>
    {
        int Day;
        int Aankomsttijd;
        int Vertektijd;
        int MaxItems;
        public ListIterator<WebServiceDetails.Result> datalijst = new ListIterator<WebServiceDetails.Result>();
        

        public ReturnListApiResult(int day, int aankomsttijd, int vertrektijd)
        {
            this.Day = day;
            this.Aankomsttijd = aankomsttijd;
            this.Vertektijd = vertrektijd;
            this.MaxItems = 5;
        }

        public void AddToResultList(WebServiceDetails.Result data)
        {
            //if (data.opening_hours.periods[Day].open.day == Day )
            //{
            //    if(Convert.ToInt32(data.opening_hours.periods[Day].open.time) <= Aankomsttijd && Convert.ToInt32(data.opening_hours.periods[Day].close.time) >= Vertektijd)
            //    {
            //        datalijst.Add(data);
            //    }
            //}  
            datalijst.Add(data);
        }

        public Option<Iterator<WebServiceDetails.Result>> GetList()
        {
            if(this.datalijst.source.Count >= MaxItems)
            {
                return new Some<Iterator<WebServiceDetails.Result>>(datalijst);
            }

            else
            {
                return new None<Iterator<WebServiceDetails.Result>>();
            }
        }

    }    

}
