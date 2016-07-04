using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Fintech
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMarketService" in both code and config file together.
    [ServiceContract]
    public interface IMarketService
    {
        [OperationContract]
        string CreateObject(string Card, string Password, string Name);

        [OperationContract]
        string CheckObject(string Card, string Password);

        [OperationContract]
        string ChangeObject(string Card, string Password, string PasswordNew, string Name, string Address, string Description, int IdImage);

        [OperationContract]
        ObjectInfo GetObject(string Card, string Password);

        [OperationContract]
        string AddCategory(int IdObject, string Name, int IdImage, string Description);

        [OperationContract]
        string ChangeCategory(int Id, int IdObject, string Name, int IdImage, string Description);

        [OperationContract]
        string RemoveCategory(int Id, int IdObject);

        [OperationContract]
        List<CategoryInfo> GetAllCategory(int IdObject);

        [OperationContract]
        string AddProduct(int IdObject, int IdCategory, string Name, decimal Price, string Currency, int IdImage, string Description);

        [OperationContract]
        string ChangeProduct(int Id, int IdObject, int IdCategory, string Name, decimal Price, string Currency, int IdImage, string Description);

        [OperationContract]
        string RemoveProduct(int Id, int IdObject);

        [OperationContract]
        List<ProductInfo> GetAllProduct(int IdObject);


        [OperationContract]
        Image LoadImage(int Id, byte[] Buffer, string Extension);

        [OperationContract]
        Image GetImage(int Id);

        [OperationContract]
        string RemoveImage(int Id);


        [OperationContract]
        List<OrderInfo> GetAllOrder(int IdObject);

        [OperationContract]
        string ChangeOrder(int Id, int IdObject, byte AsseptStatus, byte DeliveryStatus, string DeliveryTime);

    }
}
