namespace SCMSClient.Models
{
    public class Tenant : BaseModel
    {
        private string company, agreement;

        public string Company
        {
            get
            {
                return company;
            }
            set
            {
                Set(ref company, value);
                RaisePropertyChanged(() => Company);
            }
        }

        public string Agreement
        {
            get
            {
                return agreement;
            }
            set
            {
                Set(ref agreement, value);
                RaisePropertyChanged(() => Agreement);
            }
        }
    }
}
