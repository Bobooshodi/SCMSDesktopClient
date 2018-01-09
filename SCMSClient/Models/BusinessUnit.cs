namespace SCMSClient.Models
{
    public class BusinessUnit : BaseModel
    {
        private string name, description;
        private CardType cardType;

        public string Name
        {
            get { return name; }
            set
            {
                Set(ref name, value);
                RaisePropertyChanged(() => Name);
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                Set(ref description, value);
                RaisePropertyChanged(() => Description);
            }
        }

        public CardType CardType
        {
            get { return cardType; }
            set
            {
                Set(ref cardType, value);
                RaisePropertyChanged(() => CardType);
            }
        }


    }
}
