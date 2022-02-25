namespace ShopAPI
{

    public class ResponseModel<T>
     {
         public T Data { get; set; }
         public string message { get; set; }
         public int  code { get; set; }
     }
}