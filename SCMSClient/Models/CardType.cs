namespace SCMSClient.Models
{
    public class CardType : BaseModel
    {
        private string name;
        private BaseCardTypes baseCardType;

        public string Name
        {
            get { return name; }
            set
            {
                Set(ref name, value);
                RaisePropertyChanged(() => name);
            }
        }

        public BaseCardTypes BaseCardType
        {
            get { return baseCardType; }
            set
            {
                Set(ref baseCardType, value);
                RaisePropertyChanged(() => BaseCardType);
            }
        }


    }

    public enum BaseCardTypes
    {
        INDIVIDUAL,
        TENANT,
        EMPLOYEE
    }
}
