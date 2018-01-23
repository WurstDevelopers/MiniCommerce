using MiniCommerce.Domain.Interfaces;

namespace MiniCommerce.Data
{
    public class DataContract : IDataContract
    {
        public string DoSomethingDatay()
        {
            return "This is from the data layer!";
        }

        public void Save(string thingToSave)
        {
            //
        }
    }
}
