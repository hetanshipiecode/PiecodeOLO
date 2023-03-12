namespace DishoutOLO.ViewModel
{
    public class DishoutOLOResponseModel
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
        public int ListCount { get; set; }
        public List<ErrorDet> Errors { get; set; }

        public int Status { get; set; }

      
    }
    public class ErrorDet
    {
        public string ErrorField { get; set; }
        public string ErrorDescription { get; set; }
    }

}
