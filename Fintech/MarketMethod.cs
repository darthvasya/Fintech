using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fintech
{
    static public class MarketMethod
    {
        static public bool CheckMail(string Mail)
        {
            bool result = false;
            if (Mail.Length > 7 && Mail.Length <= 44)
                if (Mail.IndexOf('@') > 1)
                {
                    int temp = Mail.LastIndexOf('.');
                    if (temp > 0 && temp < Mail.Length - 2)
                        result = true;
                }
            return result;
        }
        static public bool CheckPassword(string Pas)
        {
            bool result = false;
            if (Pas.Length >= 4 && Pas.Length <= 16)
            {
                result = true;
                for (int i = 0; i < Pas.Length; i++)
                {
                    int code = (int)Pas[i];
                    if (code < 33 || code > 122) { result = false; break; }
                }
            }
            return result;
        }
        // ИНФОРМИРОВАНИЕ
        static private List<int> OrderNewList = new List<int>();
        static public void OrderNewAdd(int OrderId)
        {
            OrderNewList.Add(OrderId);
        }
        static public void OrderNewRemove(int OrderId)
        {
            for (int i = 0; i < OrderNewList.Count; i++)
                if (OrderNewList[i] == OrderId)
                {
                    OrderNewList.RemoveAt(i);
                    break;
                }
        }

    }
}