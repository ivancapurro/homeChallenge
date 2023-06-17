namespace WebAPI.Exception
{
    public class NotFoundException : SystemException{


        public string CustomeMessage { get; set; }



        public NotFoundException(string message): base(message)
        {
            CustomeMessage = message;
        }

    }
}
