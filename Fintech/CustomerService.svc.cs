using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace Fintech
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CustomerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CustomerService.svc or CustomerService.svc.cs at the Solution Explorer and start debugging.
    public class CustomerService : ICustomerService
    {
        DB_9FFBF4_FintechEntities context = new DB_9FFBF4_FintechEntities();

        public string DoWork()
        {
            //string da = context.Users.Where(p => p.uid == 1).FirstOrDefault().login.ToString();
            return "da";
        }

        public int Register(Users newUser)
        {
            List<Users> users = context.Users.Where(p => p.login == newUser.login).ToList();
            if (users.Count > 0)
            {
                return -1;
            }
            else
            {
                context.Users.Add(newUser);
                context.SaveChanges();
                return newUser.uid;
            }

        }

        public bool Login(LogInfo logInfo)
        {
            Users tempUser;
            if (((tempUser = context.Users.Where(p => p.uid == logInfo.id).FirstOrDefault()) != null)
                && (tempUser.password == logInfo.password))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool AddCard(Cards newCard)
        {
            if ((newCard.cardKey.Length == 3) && (newCard.cardNumber.Length == 16) && (newCard.uid != -1))
            {
                context.Cards.Add(newCard);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ShopInfo GetShopInfo(string id)
        {
            ShopInfo result = null;
            try
            {
                int int_id = Int32.Parse(id);
                ObjectInfo objectInfo = context.ObjectInfo.Where(p => p.Id == int_id).FirstOrDefault();
                Image objectImage = context.Image.Where(p => p.Id == objectInfo.IdImage).FirstOrDefault();
                if (objectInfo == null)
                    return result;
                result = new ShopInfo();
                result.Name = objectInfo.Name;
                result.Address = objectInfo.Address;
                result.Description = objectInfo.Description;
                if (objectImage != null)
                    result.ImageUrl = objectImage.ImageString;
                else
                    result.ImageUrl = "";
                return result;
            }
            catch (Exception exc)
            {
                return result;
            }
        }

        public List<CardInfo> GetUserCards(string id)
        {
            try
            {
                int int_id = Int32.Parse(id);
                List<Cards> cards = context.Cards.Where(p => p.uid == int_id).ToList();
                List<CardInfo> result = new List<CardInfo>();
                foreach (Cards card in cards)
                {
                    CardInfo cardInfo = new CardInfo();
                    cardInfo.cardNumber = card.cardNumber;
                    cardInfo.id = card.id;
                    result.Add(cardInfo);
                }
                return result;
            }
            catch (Exception exc)
            {
                return null;
            }
        }
    }
}
