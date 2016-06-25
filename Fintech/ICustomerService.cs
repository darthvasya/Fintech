using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

using System.Text;

namespace Fintech
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICustomerService" in both code and config file together.
    [ServiceContract]
    public interface ICustomerService
    {
        //test request
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
                                   ResponseFormat = WebMessageFormat.Json,
                                   UriTemplate = "do/")]
        string DoWork();



        //AJAX test
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
                                    RequestFormat = WebMessageFormat.Json,
                                    ResponseFormat = WebMessageFormat.Json,
                                    UriTemplate = "Register/")]
        int Register(Users newUser);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
                                    RequestFormat = WebMessageFormat.Json,
                                    ResponseFormat = WebMessageFormat.Json,
                                    UriTemplate = "Login/")]
        bool Login(LogInfo logInfo);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
                                    RequestFormat = WebMessageFormat.Json,
                                    ResponseFormat = WebMessageFormat.Json,
                                    UriTemplate = "AddCard/")]
        bool AddCard(Cards newCard);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
                                    ResponseFormat = WebMessageFormat.Json,
                                    UriTemplate = "Shops/{id}/")]
        ShopInfo GetShopInfo(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
                                    ResponseFormat = WebMessageFormat.Json,
                                    UriTemplate = "Cards/{id}/")]
        List<CardInfo> GetUserCards(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
                                    ResponseFormat = WebMessageFormat.Json,
                                    UriTemplate = "Shops/{shopId}/Categories/")]
        List<Category> GetShopCats(string shopId);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
                                    ResponseFormat = WebMessageFormat.Json,
                                    UriTemplate = "Shops/{shopId}/Categories/{catId}/")]
        List<Good> GetCatGoods(string shopId, string catId);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Shops/{shopId}/Categories/{catId}/Goods/{goodId}/")]
        Good GetGood(string shopId, string catId, string goodId);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
                                    RequestFormat = WebMessageFormat.Json,
                                    ResponseFormat = WebMessageFormat.Json,
                                    UriTemplate = "MakeOrder/")]
        string MakeOrder(Order order);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
                                    RequestFormat = WebMessageFormat.Json,
                                    ResponseFormat = WebMessageFormat.Json,
                                    UriTemplate = "SayAboutOrderAcept/{orderId}/")]
        void SayAboutOrderAccept(string orderId);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
                                    ResponseFormat = WebMessageFormat.Json,
                                    UriTemplate = "GetCatNameById/{id}/")]
        string GetCatNameById(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
                                    ResponseFormat = WebMessageFormat.Json,
                                    UriTemplate = "GetShopList/")]
        List<Shop> GetShopList();

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
                                    RequestFormat = WebMessageFormat.Json,
                                    ResponseFormat = WebMessageFormat.Json,
                                    UriTemplate = "SayAboutGoodDelivery/{id}/")]
        void SayAboutGoodDelivery(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
                                    ResponseFormat = WebMessageFormat.Json,
                                    UriTemplate = "GetOrderStatus/{orderId}/{operationId}/")]
        string GetOrderStatus(string orderId, string operationId);
    }
}
